using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Depersonalizer.Common;

namespace Depersonalizer.Text
{
	public class TextReplacer : DataReplacer
	{
		private string ReplaceByDictionary(string source, IDataDictionary dictionary)
		{
			foreach (var item in dictionary)
			{
				source = ReplaceValue(source, item.Key, item.Value);
			}
			return source;
		}

		protected virtual string ReplaceValue(string source, string key, string value)
		{
			return source.Replace(key, value);
		}

		public TextReplacer() : base() { }

		public override string Replace(string source, IDataContext context)
		{
			if (Keys == null || ReplaceWith == null)
			{
				source = ReplaceByDictionary(source, context.DataDictionary);
			}
			else
			{
				if (Keys.Length != ReplaceWith.Length)
				{
					throw new Exception("The number of keys must be the same as Replace With values");
				}

				var dictionary = new DataDictionary();

				for (int i = 0; i < Keys.Length; i++)
				{
					dictionary.AddValue(Keys[i], ReplaceWith[i]);
				}

				source = ReplaceByDictionary(source, dictionary);
			}

			return base.Replace(source, context);
		}

		public string[] Keys { get; set; }
		public string[] ReplaceWith { get; set; }
	}
}
