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
		private Logger log;
		private IScene currentScene;
		private Core core;
		public SceneProcessor(Core core, Logger log)
		{
			this.log = log;
			this.core = core;
		}
		public void Initialize(IScene scene)
		{
			currentScene = scene;
			Raylib.InitWindow(defaultWindowSize.width, defaultWindowSize.height, currentScene.Info.Name);
			log.Log("Drawer Initialized...");
		}
		public void Update()
		{
			
		}
		public void Draw()
		{
			Raylib.BeginDrawing();
			currentScene.Draw();
			Raylib.EndDrawing();
		}
	}
}
