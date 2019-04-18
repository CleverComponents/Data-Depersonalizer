using Xunit;

namespace Depersonalizer.Text.Tests
{
	public class HtmlEncoderTests
	{
		[Theory]
		[InlineData("&lt;p&gt;&#21488;&#21271;&#24066;&lt;/p&gt;", "<p>台北市</p>")]
		public void Test_Decode(string source, string expected)
		{
			var dest = HtmlEncoder.DecodeEntities(source);
			Assert.Equal(expected, dest);
		}

		[Theory]
		[InlineData("<p>台北市</p>", "&lt;p&gt;&#21488;&#21271;&#24066;&lt;/p&gt;")]
		public void Test_Encode_Dec(string source, string expected)
		{
			var dest = HtmlEncoder.EncodeEntities(source);
			Assert.Equal(expected, dest);
		}

		[Theory]
		[InlineData("<p>台北市</p>", "&lt;p&gt;&#x53F0;&#x5317;&#x5E02;&lt;/p&gt;")]
		public void Test_Encode_Hex(string source, string expected)
		{
			var dest = HtmlEncoder.EncodeEntities(source, true);
			Assert.Equal(expected, dest);
		}
	}
}
