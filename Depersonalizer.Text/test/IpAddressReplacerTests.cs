using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Depersonalizer.Text.Tests
{
	public class IpAddressReplacerTests
	{
		[Fact]
		public void TestReplace()
		{
			var replacer = new IpAddressReplacer();
			var context = new DataContext();

			replacer.ReplaceIpAddr = "127.0.0.{0}";
			context.StartFrom = 10;

			var source = "text 11.22.333.444 text \r\n next line 11.22.333.444 123.456.789.10 next line\r\n";

			source = replacer.Replace(source, context);

			Assert.Equal("text 127.0.0.10 text \r\n next line 127.0.0.10 127.0.0.11 next line\r\n", source);
			Assert.Equal(12, context.StartFrom);
		}
	}
}
