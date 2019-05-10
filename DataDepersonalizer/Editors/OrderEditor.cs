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
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Depersonalizer.Common;
using Depersonalizer.Text;
using Depersonalizer.Mime;

namespace DataDepersonalizer.Editors
{
	public class OrderEditor : StepEditor
	{
		public class ReplacerItem
		{
			public IDataReplacer Replacer { get; set; }
			public string Name { get; set; }
		}

		private ReplacerFactory factory;

		private NumericUpDown txtStartFrom;
		private DataGridView gridReplacers;
		private ComboBox cbAddReplacer;
		private Button btnAddReplacer;
		private Button btnUp;
		private Button btnDown;
		private Button btnDelete;
		private Button btnCopy;
		private Button btnPaste;

		private void BtnAddReplacer_Click(object sender, System.EventArgs e)
		{
			if (Controller.State == EditorState.Run) return;

			var replacer = factory.CreateReplacer((Type)cbAddReplacer.SelectedValue);
			Data.ReplacerChain.Add(replacer);
			LoadReplacers();
			SelectReplacer(replacer);
		}

		private void BtnPaste_Click(object sender, System.EventArgs e)
		{
			if (Controller.State == EditorState.Run) return;

			//TODO
		}

		private void BtnCopy_Click(object sender, System.EventArgs e)
		{
			if (Controller.State == EditorState.Run) return;

			//TODO
		}

		private void BtnDelete_Click(object sender, System.EventArgs e)
		{
			if (Controller.State == EditorState.Run) return;

			Data.ReplacerChain.Remove(GetSelectedReplacer());
			LoadReplacers();
		}

		private void BtnDown_Click(object sender, System.EventArgs e)
		{
			if (Controller.State == EditorState.Run) return;

			var replacer = GetSelectedReplacer();
			Data.ReplacerChain.MoveDown(replacer);
			LoadReplacers();
			SelectReplacer(replacer);
		}

		private void BtnUp_Click(object sender, System.EventArgs e)
		{
			if (Controller.State == EditorState.Run) return;

			var replacer = GetSelectedReplacer();
			Data.ReplacerChain.MoveUp(replacer);
			LoadReplacers();
			SelectReplacer(replacer);
		}

		private void TxtStartFrom_ValueChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		private void RegisterTextReplacers(ReplacerFactory factory)
		{
			factory.RegisterReplacer(typeof(EmailAddressReplacer), "E-mail address replacer");
			factory.RegisterReplacer(typeof(IpAddressReplacer), "IP address replacer");
			factory.RegisterReplacer(typeof(TextReplacer), "Text replacer");
			factory.RegisterReplacer(typeof(NameValuePairReplacer), "Name : Value pair replacer");
			factory.RegisterReplacer(typeof(XmlDocumentReplacer), "XML document replacer");
			factory.RegisterReplacer(typeof(HtmlDocumentReplacer), "HTML document replacer");
			factory.RegisterReplacer(typeof(HtmlTextReplacer), "HTML text replacer");
			factory.RegisterReplacer(typeof(RegexReplacer), "Regex replacer");
		}

		private void RegisterMimeReplacer(ReplacerFactory factory)
		{
			factory.RegisterReplacer(typeof(MimeReplacer), "MIME mail message replacer");
		}

		private void SelectReplacer(IDataReplacer replacer)
		{
			var row = gridReplacers.Rows
				.Cast<DataGridViewRow>()
				.Where(x => ((ReplacerItem)x.DataBoundItem).Replacer == replacer)
				.FirstOrDefault();

			gridReplacers.ClearSelection();
			if (row != null)
			{
				row.Selected = true;
				gridReplacers.CurrentCell = row.Cells[0];
			}
		}

		private IDataReplacer GetSelectedReplacer()
		{
			if (gridReplacers.SelectedRows.Count != 1) return null;

			return ((ReplacerItem)gridReplacers.SelectedRows[0].DataBoundItem).Replacer;
		}

		private void LoadReplacers()
		{
			var list = new List<ReplacerItem>();

			var replacers = Data.ReplacerChain.ToList();
			foreach (var replacer in replacers)
			{
				list.Add(new ReplacerItem() { Replacer = replacer, Name = factory.GetReplacerName(replacer.GetType()) });
			}

			gridReplacers.DataSource = null;
			gridReplacers.DataSource = list;
		}

		private void BindControls()
		{
			gridReplacers.AutoGenerateColumns = false;
			gridReplacers.Columns[0].DataPropertyName = "Name";

			txtStartFrom.ValueChanged += TxtStartFrom_ValueChanged;
			btnAddReplacer.Click += BtnAddReplacer_Click;

			btnUp.Click += BtnUp_Click;
			btnDown.Click += BtnDown_Click;
			btnDelete.Click += BtnDelete_Click;
			btnCopy.Click += BtnCopy_Click;
			btnPaste.Click += BtnPaste_Click;
		}

		protected override void SaveData()
		{
			Data.DataReplaceProfile.StartFromNumber = (int)txtStartFrom.Value;
		}

		protected override void LoadData()
		{
			cbAddReplacer.DataSource = new BindingSource(factory.Replacers, null);
			cbAddReplacer.DisplayMember = "Value";
			cbAddReplacer.ValueMember = "Key";

			txtStartFrom.Value = Data.DataReplaceProfile.StartFromNumber;

			LoadReplacers();
		}

		protected override void UpdateControls()
		{
			bool disabled = Controller.State == EditorState.Run;
			txtStartFrom.ReadOnly = disabled;
		}

		public OrderEditor(NumericUpDown txtStartFrom, DataGridView gridReplacers, ComboBox cbAddReplacer,
			Button btnAddReplacer, Button btnUp, Button btnDown, Button btnDelete, Button btnCopy, Button btnPaste) : base()
		{
			factory = new ReplacerFactory();
			RegisterTextReplacers(factory);

			this.txtStartFrom = txtStartFrom;
			this.gridReplacers = gridReplacers;
			this.cbAddReplacer = cbAddReplacer;
			this.btnAddReplacer = btnAddReplacer;
			this.btnUp = btnUp;
			this.btnDown = btnDown;
			this.btnDelete = btnDelete;
			this.btnCopy = btnCopy;
			this.btnPaste = btnPaste;

			BindControls();
		}
	}
}
