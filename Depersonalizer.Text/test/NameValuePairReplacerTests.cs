using Xunit;

namespace Depersonalizer.Text.Tests
{
	public class NameValuePairReplacerTests
	{
		[Fact]
		public void TestReplace()
		{
			var replacer = new NameValuePairReplacer();
			var context = new DataContext();

			replacer.PairNames = new string[] { "tracking id", "order name" };
			replacer.PairReplaceWith = new string[] { "1{0:D6}", "name {0}" };
			context.StartFrom = 10;

			var source = "qwe : asd \r\ntracking id : 1234567 \r\n order name : john smith \r\n tracking id : 1234567\r\n";

			source = replacer.Replace(source, context);

			Assert.Equal("qwe : asd \r\ntracking id : 1000010\r\norder name : name 11\r\ntracking id : 1000010\r\n", source);
			Assert.Equal(12, context.StartFrom);
		}
	}
}
