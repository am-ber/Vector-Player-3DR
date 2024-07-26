using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_Library.Arithmetic.Audio
{
	public interface ISpectrumProvider
	{
		bool GetFftData(float[] fftBuffer, object context);
		int GetFftBandIndex(float frequency);
	}
}
