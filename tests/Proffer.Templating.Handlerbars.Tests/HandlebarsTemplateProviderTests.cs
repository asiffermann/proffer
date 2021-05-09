namespace Proffer.Templating.Handlebars.Tests
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Proffer.Templating.Handlerbars.Tests.Stubs;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Templating))]
    [Feature(nameof(Handlebars))]
    [Feature(nameof(HandlebarsTemplateProvider))]
    [Collection(nameof(HandlebarsTestCollection))]
    public class HandlebarsTemplateProviderTests
    {
        private readonly HandlebarsFixture fixture;

        public HandlebarsTemplateProviderTests(HandlebarsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_GetTemplate()
        {
            ITemplate template = await this.fixture.Templates.GetTemplate("TitleBody");

            Assert.NotNull(template);

            string result = template.Apply(new { Title = "Sample title", Body = "Sample body" });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData("Format without args: ", 0)]
        [InlineData("Format with value only: 42", 1)]
        [InlineData("Format date: 08/05/2021", 2)]
        [InlineData("Format date with culture: 8 mai", 3)]
        [InlineData("Format integer: 00042", 4)]
        [InlineData("Format decimal: 42.00 %", 5)]
        [InlineData("Format float: 42.00 %", 6)]
        [InlineData("Format double: 42.00 %", 7)]
        [InlineData("Format nullable date: ", 8)]
        [InlineData("Format nullable integer: ", 9)]
        [InlineData("Format nullable decimal: ", 10)]
        [InlineData("Format nullable float: ", 11)]
        [InlineData("Format nullable double: 24.00 %", 12)]
        [InlineData("Format unformattable: --Unformattable test value--", 13)]
        public async Task Should_RegisterHelper_Format(string expectedLine, int atPosition)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var context = new FormatContext
            {
                Date = new DateTime(2021, 5, 8),
                Integer = 42,
                Decimal = 0.42M,
                Float = 0.42F,
                Double = 0.42,
                NullableDouble = 0.24
            };

            string result = await this.fixture.Templates.Format(context);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            string[] lines = result.Split(Environment.NewLine);

            Assert.True(lines.Length > atPosition);
            Assert.Equal(expectedLine, lines[atPosition]);
        }

        [Fact]
        public void Should_CompileTemplate_With_StringContent()
        {
            HandlebarsTemplateProvider provider = this.fixture.Services.GetRequiredService<HandlebarsTemplateProvider>();

            ITemplate template = provider.Compile("Hello {{Name}}!");

            Assert.NotNull(template);

            string result = template.Apply(new { Name = "World" });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("Hello World!", result);
        }
    }
}
