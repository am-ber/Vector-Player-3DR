using System.Numerics;
using Raylib_cs;
using Vector_Library.Interfaces;
using static Vector_Library.Interfaces.IScene;

namespace Vector_Library
{
    public class DefaultScene : IScene
	{
		private SceneInfo info;
		SceneInfo IScene.Info
		{
			get => info;
			set => info = value;
		}
		public DefaultScene()
		{
			info = new SceneInfo()
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
			Raylib.ClearBackground(Color.RayWhite);
			Vector2 windowScale = Raylib.GetWindowScaleDPI();
			Raylib.DrawText($"{info.Name}\n{info.Description}", (int)windowScale.X, (int)windowScale.Y, 20, Color.Red);
		}
		public void Dispose()
		{
		}
	}
}
