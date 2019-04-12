using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Depersonalizer.Text.Tests
{
	public class RegexReplacerTests
	{
		[Fact]
		public void TestReplace()
		{
			var replacer = new RegexReplacer();
			var context = new DataContext();

			replacer.RegexPatterns = new string[] { "[0-9]{8}-[0-9]{5}-[0-9]{1,3}", "trackingid=[0-9]{7}&sid=[0-9]{10}" };
			replacer.RegexReplaceWith = new string[] { "12345678-12345-{0}", "trackingid=1{0:D6}&sid=2{0:D9}" };
			context.StartFrom = 10;

			var source = "first line 87654321-54321-321 first line \r\n next line trackingid=1234567&sid=1234567890 next line 87654321-54321-321 \r\n";

			source = replacer.Replace(source, context);

			Assert.Equal("first line 12345678-12345-10 first line \r\n next line trackingid=1000011&sid=2000000011 next line 12345678-12345-10 \r\n", source);
			Assert.Equal(12, context.StartFrom);
		}
	}
}
