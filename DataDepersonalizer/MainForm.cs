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

namespace DataDepersonalizer
{
	public enum FileType { Text, TextEmail, MultipartEmail }

	public partial class MainForm : Form
	{
		bool isInProgress;

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
			LoadFileTypes();
		}

		private void DepersonalizeData(IDataReplacer dataReplacer)
		{
			if (isInProgress) return;


			dataReplacer = new EmailAddressReplacer(
				new IpAddressReplacer(dataReplacer) { ReplaceIpAddr = txtReplaceIpAddr.Text })
			{ ReplaceMask = txtReplaceMask.Text };

			var fileType = GetSelectedFileType();
			if (fileType == FileType.MultipartEmail)
			{
				dataReplacer = new MimeReplacer(dataReplacer, dataReplacer, dataReplacer, dataReplacer);
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
