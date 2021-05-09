namespace Proffer.Templating.Handlebars.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Proffer.Templating.Handlerbars.Tests.Stubs;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Templating))]
    [Feature(nameof(Handlebars))]
    [Feature(nameof(HandlebarsTemplate))]
    [Collection(nameof(HandlebarsTestCollection))]
    public class HandlebarsTemplateTests
    {
        private readonly HandlebarsFixture fixture;

        public HandlebarsTemplateTests(HandlebarsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_Poco()
        {
            string result = await this.fixture.Templates.TitleBody(new TitleBodyContext
            {
                Title = "This is a simple HTML fragment",
                Body = "With escaped <b>markup</b>!"
            });

            Assert.NotNull(result);
            Assert.Collection(
                result.Split(Environment.NewLine),
                (line) => Assert.Equal("<div class=\"entry\">", line),
                (line) => Assert.Equal("    <h1>This is a simple HTML fragment</h1>", line),
                (line) => Assert.Equal("    <div class=\"body\">", line),
                (line) => Assert.Equal("        With escaped &lt;b&gt;markup&lt;/b&gt;!", line),
                (line) => Assert.Equal("    </div>", line),
                (line) => Assert.Equal("</div>", line));
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_Dictionary()
        {
            string result = await this.fixture.Templates.TitleBody(new Dictionary<string, object>
            {
                { "Title", "This is a simple HTML fragment" },
                { "Body", "With escaped <b>markup</b>!" }
            });

            Assert.NotNull(result);
            Assert.Collection(
                result.Split(Environment.NewLine),
                (line) => Assert.Equal("<div class=\"entry\">", line),
                (line) => Assert.Equal("    <h1>This is a simple HTML fragment</h1>", line),
                (line) => Assert.Equal("    <div class=\"body\">", line),
                (line) => Assert.Equal("        With escaped &lt;b&gt;markup&lt;/b&gt;!", line),
                (line) => Assert.Equal("    </div>", line),
                (line) => Assert.Equal("</div>", line));
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_AnonymousType()
        {
            string result = await this.fixture.Templates.TitleBody(new
            {
                Title = "This is a simple HTML fragment",
                Body = "With escaped <b>markup</b>!"
            });

            Assert.NotNull(result);
            Assert.Collection(
                result.Split(Environment.NewLine),
                (line) => Assert.Equal("<div class=\"entry\">", line),
                (line) => Assert.Equal("    <h1>This is a simple HTML fragment</h1>", line),
                (line) => Assert.Equal("    <div class=\"body\">", line),
                (line) => Assert.Equal("        With escaped &lt;b&gt;markup&lt;/b&gt;!", line),
                (line) => Assert.Equal("    </div>", line),
                (line) => Assert.Equal("</div>", line));
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_JObject()
        {
            string result = await this.fixture.Templates.TitleBody(JObject.Parse(@"{
                ""Title"": ""This is a simple HTML fragment"",
                ""Body"": ""With escaped <b>markup</b>!""
            }"));

            Assert.NotNull(result);
            Assert.Collection(
                result.Split(Environment.NewLine),
                (line) => Assert.Equal("<div class=\"entry\">", line),
                (line) => Assert.Equal("    <h1>This is a simple HTML fragment</h1>", line),
                (line) => Assert.Equal("    <div class=\"body\">", line),
                (line) => Assert.Equal("        With escaped &lt;b&gt;markup&lt;/b&gt;!", line),
                (line) => Assert.Equal("    </div>", line),
                (line) => Assert.Equal("</div>", line));
        }

        [Fact]
        public async Task Should_ApplyTemplate_With_Partials()
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
        public async Task Should_ApplyTemplate_With_NullContext()
        {
            string result = await this.fixture.Templates.BadContext(null);

            Assert.NotNull(result);
            Assert.Equal("Bad context should still work: ", result);
        }
    }
}
