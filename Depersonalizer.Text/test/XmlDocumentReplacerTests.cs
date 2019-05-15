using Xunit;

namespace Depersonalizer.Text.Tests
{
	public class XmlDocumentReplacerTests
	{
		[Fact]
		public void TestReplace_NodeByName()
		{
			var replacer = new XmlDocumentReplacer();
			var context = new DataContext();

			replacer.XmlReplaceNodes.Add(new ReplaceParameter("tracking id", "1{0:D6}"));
			replacer.XmlReplaceNodes.Add(new ReplaceParameter("order name", "name {0}"));
			context.StartFrom = 10;

			var source = "<qwe>asd</qwe>\r\n<tracking id>1234567</tracking id>\r\n<order name>john smith</order name>\r\n <tracking id>1234567</tracking id> \r\n";

			source = replacer.Replace(source, context);

			Assert.Equal("<qwe>asd</qwe>\r\n<tracking id>1000010</tracking id>\r\n<order name>name 11</order name>\r\n <tracking id>1000010</tracking id> \r\n", source);
			Assert.Equal(12, context.StartFrom);
		}

		[Fact]
		public void TestReplace_NodeByDictionary()
		{
			var replacer = new XmlDocumentReplacer();
			var context = new DataContext();

			context.DataDictionary.AddValue("qwe", "dictionary");

			replacer.XmlReplaceNodes.Add(new ReplaceParameter("node", "ignore"));

			var source = "<node>qwe</node>";
			var expected = "<node>dictionary</node>";

			source = replacer.Replace(source, context);
			Assert.Equal(expected, source);
		}
	}
}
