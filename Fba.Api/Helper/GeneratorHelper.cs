using System;
namespace Fba.Api.Helper
{
	public class GeneratorHelper
	{
		public static string DigitGenerator(int digit)
		{
            var result = new char[digit];
            for (var j = 0; j < digit; j++) result[j] = (char)(new Random().NextDouble() * 10 + 48);
			return new string(result);
        }
	}
}

