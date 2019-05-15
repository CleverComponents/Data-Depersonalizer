using Xunit;

namespace Depersonalizer.Text.Tests
{
    public class EmailAddressReplacerTests
    {
		[Fact]
		public void TestReplace()
		{
			var replacer = new EmailAddressReplacer();
			var context = new DataContext();

			replacer.ReplaceMask = "zxc{0}@domain.com";
			context.StartFrom = 10;

			var source = "text qwe@example.com text \r\n next line qwe@example.com asd@example.com next line\r\n";

			source = replacer.Replace(source, context);

			Assert.Equal("text zxc10@domain.com text \r\n next line zxc10@domain.com zxc11@domain.com next line\r\n", source);
			Assert.Equal(12, context.StartFrom);
		}
    }
}
