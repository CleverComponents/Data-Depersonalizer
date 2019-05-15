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
using System.Windows.Forms;
using Depersonalizer.Text;

namespace DataDepersonalizer.Editors
{
	public abstract class ListReplaceEditor : ReplacerEditor
	{
		private DataGridView gridView;
		private BindingSource bindingSource;

		private void BindControls()
		{
			bindingSource.AddingNew += BindingSource_AddingNew;

			gridView.DataSource = bindingSource;
			gridView.AutoGenerateColumns = false;
			gridView.Columns[0].DataPropertyName = "Parameter";
			gridView.Columns[1].DataPropertyName = "ReplaceWith";
		}

		private void BindingSource_AddingNew(object sender, System.ComponentModel.AddingNewEventArgs e)
		{
			e.NewObject = new ReplaceParameter();
		}

		private void TxtEmailReplaceMask_TextChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected abstract List<ReplaceParameter> GetParameters();

		protected override void LoadData()
		{
			bindingSource.DataSource = GetParameters();
		}

		protected override void SaveData()
		{
		}

		protected override void UpdateControls()
		{
			bool disabled = Controller.State == EditorState.Run;
			gridView.ReadOnly = disabled;
		}

		protected ListReplaceEditor(DataGridView gridView) : base()
		{
			bindingSource = new BindingSource();
			this.gridView = gridView;

			BindControls();
		}
	}
}
