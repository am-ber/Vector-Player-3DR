using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_Library.Processors
{
	public class AudioProcessor
	{
		private Logger logger;
		public AudioProcessor(Logger logger)
		{
			this.logger = logger;
			logger.Log("AudioProcessor initialized...");
		}
	}
}
