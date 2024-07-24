using CSCore.CoreAudioAPI;
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
		private Core core;
		public AudioProcessor(Core core)
		{
			this.core = core;
			logger = core.logger;
			logger.Log("AudioProcessor initialized...");
		}
		/// <summary>
		/// Used for grabbing the default device for the system.
		/// </summary>
		/// <returns></returns>
		public MMDevice GetDefaultRenderDevice()
		{
			using (MMDeviceEnumerator enumerator = new MMDeviceEnumerator())
			{
				return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
			}
		}
		/// <summary>
		/// Used to determine if a specific device is playing audio or not.
		/// </summary>
		/// <param name="device"></param>
		/// <returns></returns>
		public bool IsAudioPlaying(MMDevice device)
		{
			using (AudioMeterInformation meter = AudioMeterInformation.FromDevice(device))
			{
				return meter.PeakValue > 0;
			}
		}
	}
}
