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
using System.Collections.Generic;
using Depersonalizer.Profile;

namespace DataDepersonalizer.Editors
{
	public enum EditorState { Edit, Run }

	public class EditController
	{
		private TabControl tabSteps;
		private DepersonalizerProfile data;
		private Dictionary<TabPage, StepEditor> editors;
		private EditorState state;

		private void TcSteps_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TabChanged();
		}

		private void TabChanged()
		{
			editors[tabSteps.SelectedTab].Edit(data);
		}

		private void BindControls()
		{
			tabSteps.SelectedIndexChanged += TcSteps_SelectedIndexChanged;
		}

		public EditController(TabControl tabSteps)
		{
			State = EditorState.Edit;
			editors = new Dictionary<TabPage, StepEditor>();

			this.tabSteps = tabSteps;

			BindControls();
		}

		public void RegisterEditor(StepEditor editor, TabPage page)
		{
			editors.Add(page, editor);
			editor.Controller = this;
		}

		public void Edit(DepersonalizerProfile data)
		{
			this.data = data;

			tabSteps.SelectedIndex = 0;
			TabChanged();
		}

		public EditorState State
		{
			get { return state; }
			set
			{
				if (state != value)
				{
					state = value;
					editors[tabSteps.SelectedTab].Update();
				}
			}
		}
	}
}
