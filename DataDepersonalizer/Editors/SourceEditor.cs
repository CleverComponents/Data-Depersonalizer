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

using System.Windows.Forms;
using Depersonalizer.Profile;

namespace DataDepersonalizer.Editors
{
	public class SourceEditor : StepEditor
	{
		private TextBox txtSourceFolder;
		private Button btnOpenSourceFolder;
		private CheckBox cbLinkedData;
		private TextBox txtDestinationFolder;
		private Button btnOpenDestFolder;
		private CheckBox cbWriteBom;
		private TextBox txtEncoding;
		private FolderBrowserDialog folderBrowserDialog;

		private void BtnOpenSourceFolder_Click(object sender, System.EventArgs e)
		{
			if (Controller.State == EditorState.Run) return;

			folderBrowserDialog.SelectedPath = txtSourceFolder.Text;

			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				txtSourceFolder.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void BtnOpenDestFolder_Click(object sender, System.EventArgs e)
		{
			if (Controller.State == EditorState.Run) return;

			folderBrowserDialog.SelectedPath = txtDestinationFolder.Text;

			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				txtDestinationFolder.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void CheckedChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		private void TextChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		private void BindControls()
		{
			txtSourceFolder.TextChanged += TextChanged;
			txtDestinationFolder.TextChanged += TextChanged;
			txtEncoding.TextChanged += TextChanged;
			cbLinkedData.CheckedChanged += CheckedChanged;
			cbWriteBom.CheckedChanged += CheckedChanged;

			btnOpenDestFolder.Click += BtnOpenDestFolder_Click;
			btnOpenSourceFolder.Click += BtnOpenSourceFolder_Click;
		}

		protected override void SaveData()
		{
			Data.FileReplaceProfile.SourceFolder = txtSourceFolder.Text;
			Data.FileReplaceProfile.DestinationFolder = txtDestinationFolder.Text;
			Data.FileReplaceProfile.Encoding = txtEncoding.Text;
			Data.FileReplaceProfile.LinkedDataInFiles = cbLinkedData.Checked;
			Data.FileReplaceProfile.WriteBom = cbWriteBom.Checked;
		}

		protected override void LoadData()
		{
			txtSourceFolder.Text = Data.FileReplaceProfile.SourceFolder;
			txtDestinationFolder.Text = Data.FileReplaceProfile.DestinationFolder;
			txtEncoding.Text = Data.FileReplaceProfile.Encoding;
			cbLinkedData.Checked = Data.FileReplaceProfile.LinkedDataInFiles;
			cbWriteBom.Checked = Data.FileReplaceProfile.WriteBom;
		}

		protected override void UpdateControls()
		{
			bool disabled = Controller.State == EditorState.Run;

			txtSourceFolder.ReadOnly = disabled;
			txtDestinationFolder.ReadOnly = disabled;
			txtEncoding.ReadOnly = disabled;
			cbLinkedData.Enabled = !disabled;
			cbWriteBom.Enabled = !disabled;
		}

		public SourceEditor(TextBox txtSourceFolder, Button btnOpenSourceFolder, CheckBox cbLinkedData,
			TextBox txtDestinationFolder, Button btnOpenDestFolder, CheckBox cbWriteBom, TextBox txtEncoding,
			FolderBrowserDialog folderBrowserDialog) : base()
		{
			this.txtSourceFolder = txtSourceFolder;
			this.btnOpenSourceFolder = btnOpenSourceFolder;
			this.cbLinkedData = cbLinkedData;
			this.txtDestinationFolder = txtDestinationFolder;
			this.btnOpenDestFolder = btnOpenDestFolder;
			this.cbWriteBom = cbWriteBom;
			this.txtEncoding = txtEncoding;
			this.folderBrowserDialog = folderBrowserDialog;

			BindControls();
		}
	}
}
