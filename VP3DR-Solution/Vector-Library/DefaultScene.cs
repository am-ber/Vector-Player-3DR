using System.Numerics;
using System.Text;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using Raylib_cs;
using Vector_Library.Arithmetic;
using Vector_Library.Arithmetic.Audio;
using Vector_Library.Interfaces;
using static Vector_Library.Arithmetic.Audio.SpectrumBase;

namespace Vector_Library
{
    public class DefaultScene : Scene
	{
		public DefaultScene() : this(Core.Instance) { }
		public DefaultScene(Core core)
		{
			this.core = core;
			logger = core.logger;
			Info = new SceneInfo()
			{
				Name = "Default Scene",
				Description = "Enjoy the generic equalizer scene without needing the fancy business of hard GPU usage."
			};
			windowSize = (1280, 720);
		}
		public override void Load()
		{
			core.audioProcessor.ToggleCapture(true);
		}
		public override void Update()
		{
			
		}
		public override void Draw()
		{
			Raylib.ClearBackground(Color.Black);
			
			StringBuilder waveData = new StringBuilder("Wave Data: ");
			double[] frequencies = core.audioProcessor.GetFFT();
			if (frequencies != null)
			{
				for (int i = 0; i < frequencies.Length; i++)
				{
					waveData.Append($"{Math.Round(frequencies[i], 3)}, ");
					if (i % 128 == 0)
					{
						waveData.Append("\n");
					}
				}
			}
			Raylib.DrawText(waveData.ToString(), 0, windowSize.height / 2, 4, Color.White);
		}
	}
}
