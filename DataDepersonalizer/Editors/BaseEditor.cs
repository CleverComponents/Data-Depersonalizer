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

namespace DataDepersonalizer.Editors
{
	public abstract class BaseEditor<T>
	{
		private bool isLoading;

		protected abstract void SaveData();
		protected abstract void LoadData();
		protected abstract void UpdateControls();

		public void Save()
		{
			if (Data == null) return;

			if (isLoading) return;

			SaveData();
		}

		public void Load()
		{
			if (Data == null) return;

			if (isLoading) return;

			isLoading = true;
			try
			{
				LoadData();
			}
			finally
			{
				isLoading = false;
			}
		}

		public void Update()
		{
			UpdateControls();
		}

		public void Edit(T data)
		{
			Data = data;
			Load();
			UpdateControls();
		}

		public T Data { get; private set; }

		public EditController Controller { get; set; }
	}
}
