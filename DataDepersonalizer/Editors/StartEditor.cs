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
using System.Text;
using System.IO;
using Depersonalizer.Text;
using System.Windows.Forms;

namespace DataDepersonalizer.Editors
{
	public class StartEditor : StepEditor
	{
		private TextBox txtSaveReportTo;
		private Button btnSaveReportTo;
		private Button btnStart;
		private TextBox txtLog;
		private SaveFileDialog saveFileDialog;

		private void BtnStart_Click(object sender, EventArgs e)
		{
			if (Data == null) return;

			if (Data.ReplacerChain.Root == null)
			{
				MessageBox.Show("You need to define at least one replacer to depersonalize your data.");
				return;
			}

			if (Controller.State == EditorState.Run) return;

			Controller.State = EditorState.Run;
			try
			{
				DepersonalizeFiles();
			}
			finally
			{
				Controller.State = EditorState.Edit;
			}

			MessageBox.Show("Done");
		}

		private void BtnSaveReportTo_Click(object sender, EventArgs e)
		{
			if (Controller.State == EditorState.Run) return;

			saveFileDialog.FileName = txtSaveReportTo.Text;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				txtSaveReportTo.Text = saveFileDialog.FileName;
			}
		}

		private void TxtSaveReportTo_TextChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void PutLogMessage(string message)
		{
			message += "\r\n";

			txtLog.Text += message;
			txtLog.Select(txtLog.Text.Length, 0);
			txtLog.ScrollToCaret();

			//TODO save to file
		}

		private string AddTrailingBackSlash(string path)
		{
			if (!string.IsNullOrEmpty(path) && (path[path.Length - 1] != Path.DirectorySeparatorChar) && (path[path.Length - 1] != Path.AltDirectorySeparatorChar))
			{
				return path + Path.DirectorySeparatorChar;
			}
			return path;
		}

		private void DepersonalizeFiles()
		{
			var dataContext = new DataContext() { StartFrom = Data.DataReplaceProfile.StartFromNumber };
			try
			{
				PutLogMessage("Start data depersonalization...");

				var list = Directory.GetFileSystemEntries(AddTrailingBackSlash(Data.FileReplaceProfile.SourceFolder), "*.*");

				Encoding encoding;
				if (string.IsNullOrEmpty(Data.FileReplaceProfile.Encoding))
				{
					encoding = Encoding.Default;
				}
				if (!Data.FileReplaceProfile.WriteBom && (Data.FileReplaceProfile.Encoding.ToLower() == "utf-8"))
				{
					encoding = new UTF8Encoding(false);
				}
				else
				{
					encoding = Encoding.GetEncoding(Data.FileReplaceProfile.Encoding);
				}

				foreach (var fileEntry in list)
				{
					if (!Data.FileReplaceProfile.LinkedDataInFiles)
					{
						dataContext.DataDictionary.Reset();
					}

					var source = File.ReadAllText(fileEntry, encoding);

					source = Data.ReplacerChain.Replace(source, dataContext);

					File.WriteAllText(fileEntry, source, encoding);

					PutLogMessage(String.Format("File \"{0}\" depersonalized.", Path.GetFileName(fileEntry)));
				}

				PutLogMessage("E-mails replaced, IP addresses replaced, sensitive data removed.\r\nDone.");
			}
			finally
			{
				Data.DataReplaceProfile.StartFromNumber = dataContext.StartFrom;
			}
		}

		private void BindControls()
		{
			txtSaveReportTo.TextChanged += TxtSaveReportTo_TextChanged;
			btnSaveReportTo.Click += BtnSaveReportTo_Click;
			btnStart.Click += BtnStart_Click;
		}

		protected override void SaveData()
		{
			Data.DataReplaceProfile.SaveReportTo = txtSaveReportTo.Text;
		}

		protected override void LoadData()
		{
			txtSaveReportTo.Text = Data.DataReplaceProfile.SaveReportTo;
		}

		protected override void UpdateControls()
		{
			bool disabled = Controller.State == EditorState.Run;
			txtSaveReportTo.ReadOnly = disabled;
		}

		public StartEditor(TextBox txtSaveReportTo, Button btnSaveReportTo, Button btnStart, TextBox txtLog,
			SaveFileDialog saveFileDialog) : base()
		{
			this.txtSaveReportTo = txtSaveReportTo;
			this.btnSaveReportTo = btnSaveReportTo;
			this.btnStart = btnStart;
			this.txtLog = txtLog;
			this.saveFileDialog = saveFileDialog;

			BindControls();
		}
	}
}
