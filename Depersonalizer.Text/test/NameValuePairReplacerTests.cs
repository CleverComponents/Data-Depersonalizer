using Xunit;

namespace Depersonalizer.Text.Tests
{
	public class NameValuePairReplacerTests
	{
		[Fact]
		public void TestReplace_PairByName()
		{
			var replacer = new NameValuePairReplacer();
			var context = new DataContext();

			replacer.PairNames = new string[] { "tracking id", "order name" };
			replacer.PairReplaceWith = new string[] { "1{0:D6}", "name {0}" };
			context.StartFrom = 10;

			var source = "qwe : asd \r\ntracking id : 1234567 \r\n order name: john smith \r\n tracking id : 1234567\r\n";

			source = replacer.Replace(source, context);

			Assert.Equal("qwe : asd \r\ntracking id : 1000010 \r\n order name: name 11 \r\n tracking id : 1000010\r\n", source);
			Assert.Equal(12, context.StartFrom);
		}

		[Fact]
		public void TestReplace_PairByDictionary()
		{
			var replacer = new NameValuePairReplacer();
			var context = new DataContext();

			context.DataDictionary.AddValue("qwe", "dictionary");

			replacer.PairNames = new string[] { "pair" };
			replacer.PairReplaceWith = new string[] { "ignore" };

			var source = "pair : qwe\r\n";
			var expected = "pair : dictionary\r\n";

			source = replacer.Replace(source, context);
			Assert.Equal(expected, source);
		}
	}
}
