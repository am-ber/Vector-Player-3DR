using CSCore.CoreAudioAPI;
using Raylib_cs;
using System.ComponentModel;
using System.Numerics;
using System.Reflection;
using Vector_Library.Interfaces;
using Vector_Library.Managers;

namespace Vector_Library.Processors
{
	public class SceneProcessor
	{
		private bool fullscreen = false;
		private Logger logger;
		private Scene currentScene;
		private Core core;
		public SceneProcessor(Core core)
		{
			logger = core.logger;
			this.core = core;
		}
		public void Initialize(Scene scene)
		{
			currentScene = scene;
			Raylib.InitWindow(scene.windowSize.width, scene.windowSize.height, currentScene.Info.Name);
			logger.Log("Drawer Initialized...");
		}
		public void Update()
		{
			if (currentScene != null)
			{
				core.inputManager.CheckInput(currentScene);
				currentScene.Update();
			}
		}
		public void Draw()
		{
			if (currentScene != null)
			{
				Raylib.BeginDrawing();
				// Call scene drawing
				currentScene.Draw();
				if (currentScene.drawSceneInformation)
					currentScene.DrawInfo();
				Raylib.EndDrawing();
			}
		}
	}
}
