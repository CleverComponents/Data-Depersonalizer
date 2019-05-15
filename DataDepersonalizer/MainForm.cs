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
using System.Windows.Forms;
using Depersonalizer.Text;
using Depersonalizer.Mime;
using Depersonalizer.Profile;
using DataDepersonalizer.Editors;

namespace DataDepersonalizer
{
	public partial class MainForm : Form
	{
		EditController controller;
		DepersonalizerProfile profile;

		public MainForm()
		{
			InitializeComponent();
		}

		private void NewProfile()
		{
			profile = new DepersonalizerProfile();
			controller.Edit(profile);
		}

		private void LoadProfile()
		{
			profile = DepersonalizerProfile.Load(openFileDialog1.FileName);
			controller.Edit(profile);
		}

		private void CreateController()
		{
			controller = new EditController(tabSteps);

			controller.RegisterEditor(new SourceEditor(txtSourceFolder, btnOpenSourceFolder, cbLinkedData,
				txtDestinationFolder, btnOpenDestFolder, cbWriteBom, txtEncoding,
				folderBrowserDialog1), pageSource);

			var orderEditor = new OrderEditor(txtStartFrom, gridReplacers, cbAddReplacer,
				btnAddReplacer, btnUp, btnDown, btnDelete, btnCopy, btnPaste,
				pageTextData, pageMimeData, tabEditors, tabReplacers);

			controller.RegisterEditor(orderEditor, pageOrder);

			controller.RegisterEditor(new StartEditor(txtSaveReportTo, btnSaveReportTo, btnStart, txtLog, saveFileDialog1), pageStart);

			orderEditor.RegisterReplacerEditor(new EmailAddressReplacerEditor(txtEmailReplaceMask), typeof(EmailAddressReplacer), pageEmailReplacer);
			orderEditor.RegisterReplacerEditor(new IpAddressReplacerEditor(txtIpAddrReplaceMask), typeof(IpAddressReplacer), pageIpAddrReplacer);
			orderEditor.RegisterReplacerEditor(new XmlDocumentReplacerEditor(gridXmlReplaceMask), typeof(XmlDocumentReplacer), pageXmlDocument);
			orderEditor.RegisterReplacerEditor(new RegexReplacerEditor(gridRegexPatternReplaceMask), typeof(RegexReplacer), pageRegexPatterns);
			orderEditor.RegisterReplacerEditor(new NameValuePairReplacerEditor(gridNameValueReplaceMask), typeof(NameValuePairReplacer), pageNameValuePairs);
			orderEditor.RegisterReplacerEditor(new HtmlDocumentReplacerEditor(gridHtmlReplaceMask), typeof(HtmlDocumentReplacer), pageHtmlDocument);
			orderEditor.RegisterReplacerEditor(new TextReplacerEditor(gridTextReplaceMask), typeof(TextReplacer), pageTextReplacer);
			orderEditor.RegisterReplacerEditor(new TextReplacerEditor(gridTextReplaceMask), typeof(HtmlTextReplacer), pageTextReplacer);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = controller.State == EditorState.Run;
			if (e.Cancel)
			{
				MessageBox.Show("The data depersonalization is in progress.");
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Text = Application.ProductName + " v." + Application.ProductVersion;

			CreateController();
			NewProfile();
		}

		private void btnAbout_Click(object sender, EventArgs e)
		{
			MessageBox.Show(Application.ProductName + " version " + Application.ProductVersion + "\r\nCopyright (C) " + Application.CompanyName);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSaveProfile_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				profile.Save(saveFileDialog1.FileName);
			}
		}

		private void btnLoadProfile_Click(object sender, EventArgs e)
		{
			if (controller.State == EditorState.Run) return;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				LoadProfile();
			}
		}

		private void btnNewProfile_Click(object sender, EventArgs e)
		{
			if (controller.State == EditorState.Run) return;

			if (MessageBox.Show("Do you want to drop changes and make a new profile?", "New profile confirmation",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				NewProfile();
			}
		}
	}
}
