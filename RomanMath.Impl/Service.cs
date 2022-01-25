using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace RomanMath.Impl
{
	public static class Service
	{
		/// <summary>
		/// See TODO.txt file for task details.
		/// Do not change contracts: input and output arguments, method name and access modifiers
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		private static int ConvertRomanToArab(string roman)
		{
			var dic = new Dictionary<char, int>()
			{
				['M'] = 1000,
				['D'] = 500,
				['C'] = 100,
				['L'] = 50,
				['X'] = 10,
				['V'] = 5,
				['I'] = 1
			};
			int result = 0;
			var arrayRomans = roman.ToArray();
			var number = arrayRomans.Select(x => dic.FirstOrDefault(y => x == y.Key).Value).ToArray();
			for (int i = 0; i < number.Length; i++)
			{
				if (i != number.Length - 1)
					result += number[i] >= number[i + 1] ? number[i] : -number[i];
				else
					result += number[i];
			}
			return result;
		}
		public static int Evaluate(string expression)
		{
			var numbers = new List<int>();
			var arithmeticSigns = new char[] { '+', '-', '*', '/' };
			var romNumbres = expression.Split(arithmeticSigns);
			var signsInExpression = expression.Where(x => x == '+' || x == '-' || x == '*' || x == '/').ToList();
			foreach (var romNum in romNumbres)
			{
				numbers.Add(ConvertRomanToArab(romNum));
			}
			var updatedExpression = "";
			for (int i = 0; i < numbers.Count; i++)
			{
				updatedExpression += $"{numbers[i]}";
				if (i < signsInExpression.Count)
				{
					updatedExpression += signsInExpression[i];
				}
			}
			var result = new DataTable().Compute(updatedExpression, null).ToString();
			return (int)double.Parse(result);
		}
	}
}
