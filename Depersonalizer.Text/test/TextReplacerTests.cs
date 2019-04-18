using Xunit;

namespace Depersonalizer.Text.Tests
{
	public class TextReplacerTests
	{
		[Fact]
		public void TestReplace_ByParameters()
		{
			var replacer = new TextReplacer();
			replacer.Keys = new string[] { "key" };
			replacer.ReplaceWith = new string[] { "val" };

			var context = new DataContext();
			context.DataDictionary.AddValue("key", "dictionary");

			var source = "Text line with key.\r\nAnother key line.\r\n";
			var expected = "Text line with val.\r\nAnother val line.\r\n";

			source = replacer.Replace(source, context);

			Assert.Equal(expected, source);
		}

		[Fact]
		public void TestReplace_ByContext()
		{
			var replacer = new TextReplacer();

			var context = new DataContext();
			context.DataDictionary.AddValue("key", "dictionary");

			var source = "Text line with key.\r\nAnother key line.\r\n";
			var expected = "Text line with dictionary.\r\nAnother dictionary line.\r\n";

			source = replacer.Replace(source, context);

			Assert.Equal(expected, source);
		}
	}
}
