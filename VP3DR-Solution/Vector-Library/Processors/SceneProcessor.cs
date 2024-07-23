using Raylib_cs;
using System.ComponentModel;
using System.Reflection;
using Vector_Library.Interfaces;
using Vector_Library.Managers;

namespace Vector_Library.Processors
{
	public class SceneProcessor
	{
		private bool fullscreen = false;
		private (int width, int height) defaultWindowSize;
		// objects
		private Logger log;
		public SceneProcessor(int width, int height, Logger log)
		{
			this.log = log;

			defaultWindowSize = (width, height);
		}
		public void Initialize()
		{
			Raylib.InitWindow(defaultWindowSize.width, defaultWindowSize.height, "Vector Player 3D Remastered");
			log.Log("Drawer Initialized...");
		}
		public void Update()
		{
			
		}
		public void Draw(IScene scene)
		{
			Raylib.BeginDrawing();
			scene.Draw();
			Raylib.EndDrawing();
		}
	}
}
