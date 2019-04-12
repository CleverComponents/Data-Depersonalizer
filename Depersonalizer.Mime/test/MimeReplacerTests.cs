using System;
using System.Text;
using Xunit;
using Moq;
using Depersonalizer.Common;

namespace Depersonalizer.Mime.Tests
{
    public class MimeReplacerTests
    {
		private readonly Mock<IDataContext> mockDataContext;
		private readonly Mock<IDataDictionary> mockDataDictionary;
		private readonly Mock<IDataReplacer> mockDataReplacer;

		public MimeReplacerTests()
		{
			mockDataContext = new Mock<IDataContext>();
			mockDataDictionary = new Mock<IDataDictionary>();
			mockDataReplacer = new Mock<IDataReplacer>();

			mockDataContext.Setup(x => x.StartFrom)
				.Returns(10);
			mockDataContext.Setup(x => x.DataDictionary)
				.Returns(mockDataDictionary.Object);

			string source = "";
			mockDataReplacer.Setup(x => x.Replace(It.IsAny<string>(), mockDataContext.Object))
				.Callback<string, IDataContext>((x, y) =>
				{
					source = x;
				})
				.Returns(() =>
				{
					return source.Replace("original-value", "replaced-value");
				});
		}

		[Fact]
		public void TestReplaceTextBodies()
		{
			var source =
"Subject: subj original-value\r\n" +
"Content-Type: multipart/alternative; boundary=\"----123\"\r\n" +
"\r\n" +
"This is a multi-part message in MIME format.\r\n" +
"\r\n" +
"------123\r\n" +
"Content-Type: text/plain\r\n" +
"\r\n" +
"text original-value\r\n" +
"\r\n" +
"------123\r\n" +
"Content-Type: text/html\r\n" +
"Content-Transfer-Encoding: base64\r\n" +
"\r\n" +
Convert.ToBase64String(Encoding.ASCII.GetBytes("html original-value")) + "\r\n" +
"\r\n" +
"------123--\r\n";

			var replacer = new MimeReplacer(mockDataReplacer.Object, mockDataReplacer.Object, mockDataReplacer.Object, mockDataReplacer.Object);

			source = replacer.Replace(source, mockDataContext.Object);

			Assert.True(source.IndexOf("subj replaced-value") > -1);

			Assert.True(source.IndexOf("text replaced-value") > -1);

			var encodedHtmlValue = Convert.ToBase64String(Encoding.ASCII.GetBytes("html replaced-value"));
			Assert.True(source.IndexOf(encodedHtmlValue) > -1);
		}

		[Fact]
		public void TestHandleAttachments()
		{
			var source =
"Subject: subj line\r\n" +
"Content-Type: multipart/mixed; boundary=\"----123\"\r\n" +
"\r\n" +
"This is a multi-part message in MIME format.\r\n" +
"\r\n" +
"------123\r\n" +
"Content-Type: text/plain\r\n" +
"\r\n" +
"text line\r\n" +
"\r\n" +
"------123\r\n" +
"Content-Type: application/octet-stream; name=\"file.dat\"\r\n" +
"Content-Disposition: attachment; filename=\"file.dat\"\r\n" +
"Content-Transfer-Encoding: base64\r\n" +
"\r\n" +
Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5 }) + "\r\n" +
"\r\n" +
"------123--\r\n";

			var replacer = new MimeReplacer(mockDataReplacer.Object, mockDataReplacer.Object, mockDataReplacer.Object, mockDataReplacer.Object);

			source = replacer.Replace(source, mockDataContext.Object);

			var encodedAttachment = Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5 });
			Assert.True(source.IndexOf(encodedAttachment) > -1);
		}

		[Fact]
		public void TestReplaceTextAttachments()
		{
			var source =
"Subject: subj line\r\n" +
"Content-Type: multipart/mixed; boundary=\"----123\"\r\n" +
"\r\n" +
"This is a multi-part message in MIME format.\r\n" +
"\r\n" +
"------123\r\n" +
"Content-Type: text/plain\r\n" +
"\r\n" +
"text line\r\n" +
"\r\n" +
"------123\r\n" +
"Content-Type: text/plain; charset=utf-8; name=\"file.txt\"\r\n" +
"Content-Disposition: attachment; filename=\"file.txt\"\r\n" +
"Content-Transfer-Encoding: base64\r\n" +
"\r\n" +
Convert.ToBase64String(Encoding.UTF8.GetBytes("attach original-value")) + "\r\n" +
"\r\n" +
"------123--\r\n";

			var replacer = new MimeReplacer(mockDataReplacer.Object, mockDataReplacer.Object, mockDataReplacer.Object, mockDataReplacer.Object);

			source = replacer.Replace(source, mockDataContext.Object);

			var encodedAttachment = Convert.ToBase64String(Encoding.UTF8.GetBytes("attach replaced-value"));
			Assert.True(source.IndexOf(encodedAttachment) > -1);
		}
	}
}
