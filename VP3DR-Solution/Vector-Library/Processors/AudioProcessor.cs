using System;
using System.Drawing;
using Vector_Library.Arithmetic.Audio;
using CSCore;
using CSCore.DSP;
using CSCore.CoreAudioAPI;
using CSCore.Streams;

namespace Vector_Library.Processors
{
	public class AudioProcessor
	{
		private Logger logger;
		private Core core;
		private IWaveSource source;
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
		public void GetFFT(ISampleSource sampleSource)
		{
			const FftSize fftSize = FftSize.Fft4096;
			//create a spectrum provider which provides fft data based on some input
			var spectrumProvider = new BasicSpectrumProvider(sampleSource.WaveFormat.Channels,
				sampleSource.WaveFormat.SampleRate, fftSize);
			//the SingleBlockNotificationStream is used to intercept the played samples
			var notificationSource = new SingleBlockNotificationStream(sampleSource);
			//pass the intercepted samples as input data to the spectrumprovider (which will calculate a fft based on them)
			notificationSource.SingleBlockRead += (s, a) => spectrumProvider.Add(a.Left, a.Right);

			source = notificationSource.ToWaveSource(16);
		}
	}
}
