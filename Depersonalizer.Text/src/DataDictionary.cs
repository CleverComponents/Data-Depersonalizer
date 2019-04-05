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
			string result;

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
