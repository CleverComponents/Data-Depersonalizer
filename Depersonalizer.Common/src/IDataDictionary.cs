using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depersonalizer.Common
{
	public interface IDataDictionary
	{
		void Reset();
		string GetValue(string key, Func<string> getNewValue);
	}
}
