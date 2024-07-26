using CSCore.CoreAudioAPI;
using Raylib_cs;
using System.Numerics;

namespace Vector_Library.Interfaces
{
	public abstract class Scene
	{
		public struct SceneInfo
		{
			public int index;
			public string Name;
			public string Description;

			public SceneInfo(int index, string name, string description)
			{
				this.index = index;
				Name = name;
				Description = description;
			}
			public SceneInfo(int index, SceneInfo info)
			{
				this.index = index;
				Name = info.Name;
				Description = info.Description;
			}
		}
		public (int width, int height) windowSize = (0, 0);
		public bool drawSceneInformation = true;
		protected Logger logger;
		protected Core core;
		public Scene()
		{
			core = Core.Instance;
			logger = core.logger;
		}
		public SceneInfo Info { get; set; }
		/// <summary>
		/// Used as an initializer.
		/// </summary>
		public abstract void Load();
		/// <summary>
		/// Runs in a loop before the draw method.
		/// </summary>
		public abstract void Update();
		/// <summary>
		/// Runs in a loop to draw onto the screen.
		/// </summary>
		public abstract void Draw();
		/// <summary>
		/// Used to draw information for the scene. Typically used for debugging, not for viewing pleasure.
		/// </summary>
		public virtual void DrawInfo()
		{
			// Scene info and description text drawing
			int currentMonitorIndex = Raylib.GetCurrentMonitor();
			// Checks if we are using the full screen or not
			(int width, int height) windowSizeToUse = (
				(windowSize.width <= 0 ? Raylib.GetMonitorWidth(currentMonitorIndex) : windowSize.width),
				(windowSize.height <= 0 ? Raylib.GetMonitorHeight(currentMonitorIndex) : windowSize.height));
			// Draw a transparent box over the screne as a tint
			Raylib.DrawRectangle(0, 0, windowSizeToUse.width, windowSizeToUse.height, new Color(0, 0, 0, 128));
			Vector2 center = new Vector2(windowSizeToUse.width / 2, windowSizeToUse.height / 2);
			string defaultSceneText = $"{Info.Name}\tFPS: {Raylib.GetFPS()}\n{Info.Description}";
			int textHeight = 20;
			int defaultSceneTextWidth = Raylib.MeasureText(defaultSceneText, textHeight);
			Raylib.DrawText(defaultSceneText, (int)(center.X - (defaultSceneTextWidth / 2)), (int)(center.Y - (textHeight / 2)), textHeight, Color.Red);
			// Audio device description
			MMDevice defaultAudioDevice = core.audioProcessor.GetDefaultRenderDevice();
			string audioDeviceText = $"Default Audio Device: {defaultAudioDevice.FriendlyName} | Is it active? {core.audioProcessor.IsDeviceActive(defaultAudioDevice)}";
			int audioDeviceTextWidth = Raylib.MeasureText(audioDeviceText, textHeight);
			Raylib.DrawText(audioDeviceText, (int)(center.X - (defaultSceneTextWidth / 2)), (int)(center.Y + (textHeight * 2)), textHeight, Color.Red);
		}
		/// <summary>
		/// For cleaning up resources when a scene is discarded.
		/// </summary>
		public virtual void Exit()
		{
			logger.Log($"Exiting scene: {Info.Name}");
			Raylib.CloseWindow();
		}
	}
}
