using FftSharp;
using FftSharp.Windows;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Drawing;
using System.Numerics;
using Vector_Library.Arithmetic.Audio;

namespace Vector_Library.Processors
{
	public class AudioProcessor
	{
		public Logger logger;
		public AudioInfo AudioInfo;

		private Core core;
		private WasapiLoopbackCapture wasapiCapture;
		private MMDevice audioDevice;
		private double[] samples;
		private int bitsPerSample;
		private int channels;
		private int sampleRate;
		private int BufferMilliseconds = 20;
		public AudioProcessor(Core core)
		{
			this.core = core;
			logger = core.logger;
			AudioInfo = new AudioInfo();

			logger.Log("AudioProcessor initialized...");
		}
		public void Initialize(MMDevice? mDevice = null)
		{
			// Sets the device to the default Loopback capturing if you don't specify a device
			if (mDevice == null)
			{
				mDevice = WasapiLoopbackCapture.GetDefaultLoopbackCaptureDevice();
			}
			audioDevice = mDevice;
			// Destroy existing instances of the capture and set it to the device given
			wasapiCapture?.Dispose();
			wasapiCapture = new WasapiLoopbackCapture(audioDevice);
			wasapiCapture.DataAvailable += SaveSamples;

			bitsPerSample = wasapiCapture.WaveFormat.BitsPerSample;
			sampleRate = wasapiCapture.WaveFormat.SampleRate;
			channels = wasapiCapture.WaveFormat.Channels;
			samples = new double[sampleRate * BufferMilliseconds / 1000];

			if (AudioInfo.IsRecording)
			{
				wasapiCapture.StartRecording();
			}
		}
		public void ToggleCapture()
		{
			ToggleCapture(!AudioInfo.IsRecording);
		}
		public void ToggleCapture(bool value)
		{
			if (value)
			{
				wasapiCapture.StartRecording();
			} else
			{
				wasapiCapture.StopRecording();
			}
			AudioInfo.IsRecording = value;
		}
		private void SaveSamples(object sender, WaveInEventArgs e)
		{
			if (e.BytesRecorded == 0)
				return;
			samples = Enumerable.Range(0, e.BytesRecorded / 4).Select(i => BitConverter.ToDouble(e.Buffer, i * 4)).ToArray();
		}
		public double[] GetSamples()
		{
			return samples;
		}
		public MMDevice GetMediaDevice()
		{
			return audioDevice;
		}
		public double[] GetFFT(double[]? samples = null, FFTStyle style = FFTStyle.Magnitude)
		{
			if (samples == null || samples.Length == 0)
			{
				samples = this.samples;
			}
			try
			{
				double[] paddedAudio = Pad.ZeroPad(samples);
				System.Numerics.Complex[] sampleComplexes = FFT.Forward(paddedAudio);
				// Just default to the magnitude style because sure why not
				switch (style)
				{
					default:
					case FFTStyle.Magnitude:
						return FFT.Magnitude(sampleComplexes);
					case FFTStyle.InverseReal:
						return FFT.InverseReal(sampleComplexes);
					case FFTStyle.Power:
						return FFT.Power(sampleComplexes);
					case FFTStyle.Phase:
						return FFT.Phase(sampleComplexes);
				}
			} catch
			{
				return new double[samples.Length];
			}
		}
	}
}
