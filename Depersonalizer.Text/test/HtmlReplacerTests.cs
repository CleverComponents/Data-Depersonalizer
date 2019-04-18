using Xunit;

namespace Depersonalizer.Text.Tests
{
	public class HtmlReplacerTests
	{
		[Theory]
		[InlineData("<p>value</p>\r\n", "<p>value</p>\r\n")]
		[InlineData("<p id=\"a1\">value</p><div id=\"a1\">value</div>\r\n", "<p id=\"a1\">1000010</p><div id=\"a1\">1000010</div>\r\n")]
		[InlineData("<p class='x' id=\"a1\" param=\"b\">value</p>\r\n", "<p class='x' id=\"a1\" param=\"b\">1000010</p>\r\n")]
		[InlineData("<p class='x' id=\"a1\"\r\n param=\"b\">value</p>\r\n", "<p class='x' id=\"a1\"\r\n param=\"b\">1000010</p>\r\n")]
		public void TestReplace_TagById(string source, string expected)
		{
			var replacer = new HtmlReplacer();
			var context = new DataContext();

			replacer.TagIds = new string[] { "a1" };
			replacer.TagReplaceWith = new string[] { "1{0:D6}" };
			context.StartFrom = 10;

			source = replacer.Replace(source, context);
			Assert.Equal(expected, source);
		}

		[Fact]
		public void TestReplace_TagByDictionary()
		{
			var replacer = new HtmlReplacer();
			var context = new DataContext();

			context.DataDictionary.AddValue("台北市", "dictionary");

			replacer.TagIds = new string[] { "a1" };
			replacer.TagReplaceWith = new string[] { "ignore" };

			var source = "<p id=\"a1\">&#21488;&#21271;&#24066;</p>";
			var expected = "<p id=\"a1\">dictionary</p>";

			source = replacer.Replace(source, context);
			Assert.Equal(expected, source);
		}
	}
}
