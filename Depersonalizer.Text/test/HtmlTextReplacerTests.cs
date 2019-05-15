using Xunit;

namespace Depersonalizer.Text.Tests
{
	public class HtmlTextReplacerTests
	{
		[Fact]
		public void TestReplace_ByContext()
		{
			var replacer = new HtmlTextReplacer();

			var context = new DataContext();
			context.DataDictionary.AddValue("台北市", "北京");

			var source = "<p>&#21488;&#21271;&#24066;</p> <p>&#x53F0;&#x5317;&#x5E02;</p> <p>台北市</p>";
			var expected = "<p>&#21271;&#20140;</p> <p>&#21271;&#20140;</p> <p>北京</p>";

			source = replacer.Replace(source, context);

			Assert.Equal(expected, source);
		}
	}
}
