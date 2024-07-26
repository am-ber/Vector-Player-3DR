using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_Library.Arithmetic
{
	public class MathmaticalExtentions
	{
		/// <summary>
		/// Used to quickly and logically iterate cyclically within a linear fasion non-negatively.
		/// </summary>
		/// <param name="x">(i.e. how much you want to traverse through the array)</param>
		/// <param name="modulo">(i.e. the size of an array)</param>
		/// <returns></returns>
		public static int WheelMod(int x, int modulo)
		{
			int remainder = x % modulo;
			return remainder < 0 ? remainder + modulo : remainder;
		}
	}
}
