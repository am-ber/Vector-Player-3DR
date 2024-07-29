using System;
using System.Drawing;
using Vector_Library.Arithmetic.Audio;
using CSCore;
using CSCore.DSP;
using CSCore.CoreAudioAPI;
using CSCore.Streams;
using CSCore.SoundIn;

namespace Vector_Library.Processors
{
	public class AudioProcessor
	{
		public readonly FftSize fftSize = FftSize.Fft4096;
		public Logger logger;
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
		/// Used to determine if a specific device is active or not.
		/// </summary>
		/// <param name="device"></param>
		/// <returns></returns>
		public bool IsDeviceActive(MMDevice device)
		{
			device.GetStateNative(out DeviceState deviceState);
			return deviceState == DeviceState.Active;
		}
		public void Exit()
		{

		}
	}
}
