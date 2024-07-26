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

				if (currentScene.drawSceneInformation)
				{
					Raylib.ClearBackground(Color.Black);
					// Scene info and description text drawing
					int currentMonitorIndex = Raylib.GetCurrentMonitor();
					// Checks if we are using the full screen or not.
					Vector2 center = new Vector2(
						(currentScene.windowSize.width <= 0 ? Raylib.GetMonitorWidth(currentMonitorIndex) : currentScene.windowSize.width) / 2,
						(currentScene.windowSize.height <= 0 ? Raylib.GetMonitorHeight(currentMonitorIndex) : currentScene.windowSize.height) / 2);
					string defaultSceneText = $"{currentScene.Info.Name}\tFPS: {Raylib.GetFPS()}\n{currentScene.Info.Description}";
					int textHeight = 20;
					int defaultSceneTextWidth = Raylib.MeasureText(defaultSceneText, textHeight);
					Raylib.DrawText(defaultSceneText, (int)(center.X - (defaultSceneTextWidth / 2)), (int)(center.Y - (textHeight / 2)), textHeight, Color.Red);
					// Audio device description
					MMDevice defaultAudioDevice = core.audioProcessor.GetDefaultRenderDevice();
					string audioDeviceText = $"Default Audio Device: {defaultAudioDevice.FriendlyName} | Is it active? {core.audioProcessor.IsDeviceActive(defaultAudioDevice)}";
					int audioDeviceTextWidth = Raylib.MeasureText(audioDeviceText, textHeight);
					Raylib.DrawText(audioDeviceText, (int)(center.X - (defaultSceneTextWidth / 2)), (int)(center.Y + (textHeight * 2)), textHeight, Color.Red);
				}
				// Call scene drawing
				currentScene.Draw();
				Raylib.EndDrawing();
			}
		}
	}
}
