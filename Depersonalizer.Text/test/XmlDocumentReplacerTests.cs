using Xunit;

namespace Depersonalizer.Text.Tests
{
	public class XmlDocumentReplacerTests
	{
		[Fact]
		public void TestReplace()
		{
			var replacer = new XmlDocumentReplacer();
			var context = new DataContext();

			replacer.XmlNodes = new string[] { "tracking id", "order name" };
			replacer.XmlReplaceWith = new string[] { "1{0:D6}", "name {0}" };
			context.StartFrom = 10;

			var source = "<qwe>asd</qwe>\r\n<tracking id>1234567</tracking id>\r\n<order name>john smith</order name>\r\n <tracking id>1234567</tracking id> \r\n";

			source = replacer.Replace(source, context);

			Assert.Equal("<qwe>asd</qwe>\r\n<tracking id>1000010</tracking id>\r\n<order name>name 11</order name>\r\n <tracking id>1000010</tracking id> \r\n", source);
			Assert.Equal(12, context.StartFrom);
		}
	}
}
