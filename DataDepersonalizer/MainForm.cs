#region license
//Copyright(C) 2019 by Clever Components

//Author: Sergey Shirokov<admin@clevercomponents.com>
//Website: www.CleverComponents.com

//This file is part of the Data Depersonalizer application.
//The Data Depersonalizer application is free software:
//you can redistribute it and/or modify it under the terms of
//the GNU Lesser General Public License version 3
//as published by the Free Software Foundation and appearing in the
//included file COPYING.LESSER.

//The Data Depersonalizer application is distributed in the hope
//that it will be useful, but WITHOUT ANY WARRANTY; without even the
//implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//See the GNU Lesser General Public License for more details.

//You should have received a copy of the GNU Lesser General Public License
//along with the Data Depersonalizer application. If not, see<http://www.gnu.org/licenses/>.
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Depersonalizer.Common;
using Depersonalizer.Text;
using Depersonalizer.Mime;
using Depersonalizer.Profile;

namespace DataDepersonalizer
{
	public enum FileType { Text, TextEmail, MultipartEmail }

	public partial class MainForm : Form
	{
		bool isInProgress;
		DepersonalizerProfile profile;

		public MainForm()
		{
			InitializeComponent();
		}

		private string AddTrailingBackSlash(string path)
		{
			if (!string.IsNullOrEmpty(path) && (path[path.Length - 1] != Path.DirectorySeparatorChar) && (path[path.Length - 1] != Path.AltDirectorySeparatorChar))
			{
				return path + Path.DirectorySeparatorChar;
			}
			return path;
		}

		private void PutLogMessage(string message)
		{
			txtLog.Text += message + "\r\n";
			txtLog.Select(txtLog.Text.Length, 0);
			txtLog.ScrollToCaret();
		}

		private void LoadFileTypes()
		{
			Dictionary<FileType, string> fileTypes = new Dictionary<FileType, string>();
			fileTypes.Add(FileType.Text, "Text");
			fileTypes.Add(FileType.TextEmail, "Simple text E-mail");
			fileTypes.Add(FileType.MultipartEmail, "Multipart MIME E-mail");

			cbFileType.DataSource = new BindingSource(fileTypes, null);
			cbFileType.DisplayMember = "Value";
			cbFileType.ValueMember = "Key";
		}

		private FileType GetSelectedFileType()
		{
			return ((KeyValuePair<FileType, string>)cbFileType.SelectedItem).Key;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			profile = new DepersonalizerProfile();
			LoadFileTypes();
		}

		private void DefineMimeReplacerChain()
		{
			var attachHtmlTextReplacer = new HtmlTextReplacer() { };
			var attachXmlReplacer = new XmlDocumentReplacer()
			{
				NextReplacer = attachHtmlTextReplacer,
				XmlNodes = new string[] { "CUSTOMERNAME", "CUSTOMERADDRESS1" },
				XmlReplaceWith = new string[] { "John Smith{0}", "Lake City {0}" }
			};
			var attachReplacer = new TextAttachmentReplacer() { NextReplacer = attachXmlReplacer };

			var emailReplacer = new EmailAddressReplacer() { ReplaceMask = "recipient{0}@example.com" };
			var ipReplacer = new IpAddressReplacer() { ReplaceIpAddr = "127.0.0.{0}", NextReplacer = emailReplacer };
			var headerReplacer = new MimeHeaderReplacer() { NextReplacer = ipReplacer };

			var textReplacer = new TextReplacer() { };
			var textBodyReplacer = new TextBodyReplacer() { NextReplacer = textReplacer };

			var mimeReplacer = new MimeReplacer();

			mimeReplacer.MimePartReplacers.Add(attachReplacer);
			mimeReplacer.MimePartReplacers.Add(headerReplacer);
			mimeReplacer.MimePartReplacers.Add(textBodyReplacer);

			profile.ReplacerChain = mimeReplacer;
		}

		private void DepersonalizeChain()
		{
			if (isInProgress) return;

			var dataContext = new DataContext() { StartFrom = Convert.ToInt32(txtStartFrom.Text) };

			isInProgress = true;
			try
			{
				PutLogMessage("Start data depersonalization...");

				var list = Directory.GetFileSystemEntries(AddTrailingBackSlash(txtEmailFolder.Text), "*.*");

				Encoding encoding;
				if (string.IsNullOrEmpty(txtEncoding.Text))
				{
					encoding = Encoding.Default;
				}
				if (!cbWriteBom.Checked && (txtEncoding.Text.ToLower() == "utf-8"))
				{
					encoding = new UTF8Encoding(false);
				}
				else
				{
					encoding = Encoding.GetEncoding(txtEncoding.Text);
				}

				foreach (var fileEntry in list)
				{
					if (!cbLinkedData.Checked)
					{
						dataContext.DataDictionary.Reset();
					}

					var source = File.ReadAllText(fileEntry, encoding);

					source = profile.ReplacerChain.Replace(source, dataContext);

					File.WriteAllText(fileEntry, source, encoding);

					PutLogMessage(String.Format("File \"{0}\" depersonalized.", Path.GetFileName(fileEntry)));
				}

				PutLogMessage("E-mails replaced, IP addresses replaced, sensitive data removed.\r\nDone.");
			}
			finally
			{
				isInProgress = false;
				txtStartFrom.Text = dataContext.StartFrom.ToString();
			}
		}

		private void DepersonalizeData(IDataReplacer dataReplacer)
		{
			if (isInProgress) return;

			var ipAddressReplacer = new IpAddressReplacer() { NextReplacer = dataReplacer, ReplaceIpAddr = txtReplaceIpAddr.Text };
			dataReplacer = new EmailAddressReplacer() { NextReplacer = ipAddressReplacer, ReplaceMask = txtReplaceMask.Text };

			var fileType = GetSelectedFileType();
			if (fileType == FileType.MultipartEmail)
			{
				var mimeReplacer = new MimeReplacer();
				mimeReplacer.MimePartReplacers.Add(new MimeHeaderReplacer() { NextReplacer = dataReplacer });
				mimeReplacer.MimePartReplacers.Add(new TextBodyReplacer() { NextReplacer = dataReplacer });
				mimeReplacer.MimePartReplacers.Add(new HtmlBodyReplacer() { NextReplacer = dataReplacer });
				mimeReplacer.MimePartReplacers.Add(new TextAttachmentReplacer() { NextReplacer = dataReplacer });

				dataReplacer = mimeReplacer;
			}

			var dataContext = new DataContext() { StartFrom = Convert.ToInt32(txtStartFrom.Text) };

			isInProgress = true;
			try
			{
				PutLogMessage("Start data depersonalization...");

				var list = Directory.GetFileSystemEntries(AddTrailingBackSlash(txtEmailFolder.Text), "*.*");

				Encoding encoding;
				if (string.IsNullOrEmpty(txtEncoding.Text))
				{
					encoding = Encoding.Default;
				}
				if (!cbWriteBom.Checked && (txtEncoding.Text.ToLower() == "utf-8"))
				{
					encoding = new UTF8Encoding(false);
				}
				else
				{
					encoding = Encoding.GetEncoding(txtEncoding.Text);
				}

				foreach (var fileEntry in list)
				{
					if (!cbLinkedData.Checked)
					{
						dataContext.DataDictionary.Reset();
					}

					var source = File.ReadAllText(fileEntry, encoding);

					source = dataReplacer.Replace(source, dataContext);

					File.WriteAllText(fileEntry, source, encoding);

					PutLogMessage(String.Format("File \"{0}\" depersonalized.", Path.GetFileName(fileEntry)));
				}

				PutLogMessage("E-mails replaced, IP addresses replaced, sensitive data removed.\r\nDone.");
			}
			finally
			{
				isInProgress = false;
				txtStartFrom.Text = dataContext.StartFrom.ToString();
			}
		}

		private void btnOpenEmailFolder_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				txtEmailFolder.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void btnDepersonalizeText_Click(object sender, EventArgs e)
		{
			//DefineMimeReplacerChain();
			//profile.Save("DefineMimeReplacerChain.xml");
			//profile.Load("DefineMimeReplacerChain.xml");

			//DepersonalizeChain();

			//MessageBox.Show("Done");

			DepersonalizeData(null);
		}

		private void btnDepersonalizeXml_Click(object sender, EventArgs e)
		{
			DepersonalizeData(new XmlDocumentReplacer() { XmlNodes = txtXmlNodes.Lines, XmlReplaceWith = txtXmlReplaceWith.Lines });
		}

		private void btnDepersonalizeNameValues_Click(object sender, EventArgs e)
		{
			DepersonalizeData(new NameValuePairReplacer() { PairNames = txtPairNames.Lines, PairReplaceWith = txtPairReplaceWith.Lines });
		}

		private void btnDepersonalizeRegex_Click(object sender, EventArgs e)
		{
			DepersonalizeData(new RegexReplacer() { RegexPatterns = txtRegexPatterns.Lines, RegexReplaceWith = txtRegexReplaceWith.Lines });
		}
	}
}
