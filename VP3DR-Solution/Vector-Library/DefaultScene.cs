using System.Numerics;
using CSCore.CoreAudioAPI;
using Raylib_cs;
using Vector_Library.Interfaces;

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
		public override void Update()
		{
			
		}
		public override void Draw()
		{
			Raylib.ClearBackground(Color.Black);
			
		}
	}
}
