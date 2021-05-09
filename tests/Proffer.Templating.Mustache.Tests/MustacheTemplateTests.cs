namespace Proffer.Templating.Mustache.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Proffer.Templating.Mustache.Tests.Stubs;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Templating))]
    [Feature(nameof(Mustache))]
    [Feature(nameof(MustacheTemplate))]
    [Collection(nameof(MustacheTestCollection))]
    public class MustacheTemplateTests
    {
        private readonly MustacheFixture fixture;

        public MustacheTemplateTests(MustacheFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_Poco()
        {
            string result = await this.fixture.Templates.TitleBody(new TitleBodyContext
            {
                Title = "This is a simple HTML fragment",
                Body = "With unescaped <b>markup</b>!"
            });

            Assert.NotNull(result);
            Assert.Collection(
                result.Split(Environment.NewLine),
                (line) => Assert.Equal("<div class=\"entry\">", line),
                (line) => Assert.Equal("    <h1>This is a simple HTML fragment</h1>", line),
                (line) => Assert.Equal("    <div class=\"body\">", line),
                (line) => Assert.Equal("        With unescaped <b>markup</b>!", line),
                (line) => Assert.Equal("    </div>", line),
                (line) => Assert.Equal("</div>", line));
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_Dictionary()
        {
            string result = await this.fixture.Templates.TitleBody(new Dictionary<string, object>
            {
                { "Title", "This is a simple HTML fragment" },
                { "Body", "With unescaped <b>markup</b>!" }
            });

            Assert.NotNull(result);
            Assert.Collection(
                result.Split(Environment.NewLine),
                (line) => Assert.Equal("<div class=\"entry\">", line),
                (line) => Assert.Equal("    <h1>This is a simple HTML fragment</h1>", line),
                (line) => Assert.Equal("    <div class=\"body\">", line),
                (line) => Assert.Equal("        With unescaped <b>markup</b>!", line),
                (line) => Assert.Equal("    </div>", line),
                (line) => Assert.Equal("</div>", line));
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_AnonymousType()
        {
            string result = await this.fixture.Templates.TitleBody(new
            {
                Title = "This is a simple HTML fragment",
                Body = "With unescaped <b>markup</b>!"
            });

            Assert.NotNull(result);
            Assert.Collection(
                result.Split(Environment.NewLine),
                (line) => Assert.Equal("<div class=\"entry\">", line),
                (line) => Assert.Equal("    <h1>This is a simple HTML fragment</h1>", line),
                (line) => Assert.Equal("    <div class=\"body\">", line),
                (line) => Assert.Equal("        With unescaped <b>markup</b>!", line),
                (line) => Assert.Equal("    </div>", line),
                (line) => Assert.Equal("</div>", line));
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_JObject()
        {
            string result = await this.fixture.Templates.TitleBody(JObject.Parse(@"{
                ""Title"": ""This is a simple HTML fragment"",
                ""Body"": ""With unescaped <b>markup</b>!""
            }"));

            Assert.NotNull(result);
            Assert.Collection(
                result.Split(Environment.NewLine),
                (line) => Assert.Equal("<div class=\"entry\">", line),
                (line) => Assert.Equal("    <h1>This is a simple HTML fragment</h1>", line),
                (line) => Assert.Equal("    <div class=\"body\">", line),
                (line) => Assert.Equal("        With unescaped <b>markup</b>!", line),
                (line) => Assert.Equal("    </div>", line),
                (line) => Assert.Equal("</div>", line));
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_Each()
        {
            string result = await this.fixture.Templates.Contacts(new()
            {
                Contacts = new()
                {
                    new() { FirstName = "David", LastName = "Bechard" },
                    new() { FirstName = "Médard", LastName = "Gainsbourg" },
                    new() { FirstName = "Gwenaëlle", LastName = "Carpentier" },
                }
            });

            Assert.NotNull(result);

            Assert.Collection(
                result.Split(Environment.NewLine),
                (line) => Assert.Equal("<h2>Your contact list</h2>", line),
                (line) => Assert.Equal("0. <strong>David Bechard</strong>", line),
                (line) => Assert.Equal("1. <strong>Médard Gainsbourg</strong>", line),
                (line) => Assert.Equal("2. <strong>Gwenaëlle Carpentier</strong>", line),
                (line) => Assert.Equal(string.Empty, line));
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_FormatProvider()
        {
            DateTime today = DateTime.Now.Date;

            string result = await this.fixture.Templates.TitleBody(new TitleBodyContext
            {
                Title = "This is a simple HTML fragment",
                Body = today
            },
            new CultureInfo("fr-FR"));

            Assert.NotNull(result);
            Assert.Collection(
                result.Split(Environment.NewLine),
                (line) => Assert.Equal("<div class=\"entry\">", line),
                (line) => Assert.Equal("    <h1>This is a simple HTML fragment</h1>", line),
                (line) => Assert.Equal("    <div class=\"body\">", line),
                (line) => Assert.Equal($"        {today:dd/MM/yyyy HH:mm:ss}", line),
                (line) => Assert.Equal("    </div>", line),
                (line) => Assert.Equal("</div>", line));
        }

        [Fact]
        public async Task Should_Throws_With_NullContext()
        {
            await Assert.ThrowsAsync<InvalidContextException>(() => this.fixture.Templates.BadContext(null));
        }

        [Fact]
        public async Task Should_Throws_With_IncompleteContext()
        {
            await Assert.ThrowsAsync<InvalidContextException>(() => this.fixture.Templates.BadContext(new { Property = "Not used" }));
        }

        [Fact]
        public async Task Should_Throws_With_NullContext_And_FormatProvider()
        {
            await Assert.ThrowsAsync<InvalidContextException>(() => this.fixture.Templates.TitleBody(null, new CultureInfo("fr-FR")));
        }
    }
}
