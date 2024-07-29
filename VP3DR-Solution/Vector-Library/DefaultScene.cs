using System.Numerics;
using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using Raylib_cs;
using Vector_Library.Arithmetic;
using Vector_Library.Arithmetic.Audio;
using Vector_Library.Interfaces;
using static Vector_Library.Arithmetic.Audio.SpectrumBase;

namespace Vector_Library
{
    public class DefaultScene : Scene
	{
		ClassicSpectrum spectrum;
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
			spectrum = new ClassicSpectrum(core.audioProcessor);
		}
		public override void Update()
		{
			
		}
		public override void Draw()
		{
			Raylib.ClearBackground(Color.Black);
			SpectrumPointData[] spectrumPoints = spectrum.GetSpectrumPoints();
			int spectrumSpacing = (int)MathF.Round((windowSize.width / (spectrumPoints.Length > 0 ? spectrumPoints.Length : windowSize.width)), MidpointRounding.ToPositiveInfinity);
			for (int i = 0; i < spectrumPoints.Length; i++)
			{
				SpectrumPointData spectrumPoint = spectrumPoints[i];
				int mappedY = (int)MathmaticalExtentions.Map(spectrumPoint.Value, 0, 1.0, windowSize.height, 0);
				Raylib.DrawRectangle(spectrumPoint.SpectrumPointIndex, windowSize.height, spectrumSpacing, mappedY, Color.Red);
			}
		}
		public override void Exit()
		{
			spectrum.Stop();
			base.Exit();
		}
	}
}
