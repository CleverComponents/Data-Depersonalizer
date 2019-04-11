using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Depersonalizer.Common;

namespace Depersonalizer.Text
{
	public class DataDictionary : Dictionary<string, string>, IDataDictionary
	{
		public void Reset()
		{
			Clear();
		}

		public string GetValue(string key, Func<string> getNewValue)
		{
			System.Diagnostics.Debug.Assert(key != null);

			string result;

			key = key.Trim();

			if (TryGetValue(key, out result))
			{
				return result;
			}

			var newValue = getNewValue();
			Add(key, newValue);
			return newValue;
		}
	}
}
