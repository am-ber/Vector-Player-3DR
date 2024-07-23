using System.Numerics;
using Raylib_cs;
using Vector_Library.Interfaces;
using static Vector_Library.Interfaces.IScene;

namespace Vector_Library
{
    public class DefaultScene : IScene
	{
		public SceneInfo Info { get; }
		private Logger logger;
		private Core core;
		public DefaultScene(Core core)
		{
			this.core = core;
			logger = core.logger;
			Info = new SceneInfo()
			{
				Name = "Default Scene",
				Description = "Enjoy the generic equalizer scene without needing the fancy business of hard GPU usage."
			};
		}
		public void Load()
		{
		}
		public void Update()
		{
		}
		public void Draw()
		{
			Raylib.ClearBackground(Color.Black);
			Raylib.DrawText($"{Info.Name}\tFPS: {Raylib.GetFPS()}\n{Info.Description}", core.defaultWindowSize.width / 2, core.defaultWindowSize.height / 2, 20, Color.Red);
		}
		public void Dispose()
		{
		}
	}
}
