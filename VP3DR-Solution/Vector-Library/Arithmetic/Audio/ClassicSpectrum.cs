using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.DSP;
using CSCore.SoundIn;
using CSCore.Streams;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector_Library.Processors;

namespace Vector_Library.Arithmetic.Audio
{
	public class ClassicSpectrum : SpectrumBase
	{
		private AudioProcessor processor;
		private WasapiCapture soundIn;
		private IWaveSource sampleSource;
		private bool initialized = false;
		/// <summary>
		/// Defaults to doing a classic spectrum result on the default audio device.
		/// </summary>
		/// <param name="processor"></param>
		/// <param name="sampleSource"></param>
		public ClassicSpectrum(AudioProcessor processor, IWaveSource sampleSource = null)
		{
			this.processor = processor;
			processor.logger.Log("Initializing the Classic Spectrum...");
			if (sampleSource == null)
			{
				sampleSource = GetSampleSource();
			}
			this.sampleSource = sampleSource;

			ScalingStrategy = ScalingStrategy.Sqrt;
		}
		public SpectrumPointData[] GetSpectrumPoints()
		{
			if (!initialized)
			{
				UpdateFrequencyMapping();
				initialized = true;
			}

			return CalculateSpectrumPoints(1.0, GetFFT());
		}
		public float[] GetFFT()
		{
			float[] fftResultBuffer = new float[(int)processor.fftSize];
			SpectrumProvider.GetFftData(fftResultBuffer, SpectrumProvider);
			return fftResultBuffer;
		}
		private IWaveSource GetSampleSource(MMDevice device = null)
		{
			if (device == null)
			{
				device = processor.GetDefaultRenderDevice();
			}
			if (soundIn == null)
			{
				soundIn = new WasapiLoopbackCapture();
				soundIn.Initialize();
				soundIn.Start();
			}
			processor.logger.Log($"Sound IN: {soundIn.Device.FriendlyName}");

			// Used for adjusting the source if needed
			SoundInSource sample = new SoundInSource(soundIn);
			
			//create a spectrum provider which provides fft data based on some input
			SpectrumProvider = new BasicSpectrumProvider(soundIn.WaveFormat.Channels,
				soundIn.WaveFormat.SampleRate, processor.fftSize);
			
			var notificationSource = new SingleBlockNotificationStream(sample.ToSampleSource());
			notificationSource.SingleBlockRead += (s, a) => (SpectrumProvider as BasicSpectrumProvider).Add(a.Left, a.Right);

			// Starts the reading process for the provider to be populated
			byte[] buffer = new byte[sample.WaveFormat.BytesPerSecond / 2];
			sample.DataAvailable += (s, a) =>
			{
				int read;
				while ((read = sample.Read(buffer, 0, buffer.Length)) > 0);
			};
			return sample;
		}
		public void Stop()
		{
			if (soundIn != null)
			{
				soundIn.Stop();
				soundIn.Dispose();
			}
		}
	}
}
