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
		public override void Load()
		{
		}
		public override void Update()
		{
		}
		public override void Draw()
		{
			Raylib.ClearBackground(Color.Black);
			// Scene info and description text drawing
			Vector2 center = new Vector2(windowSize.width / 2, windowSize.height / 2);
			string defaultSceneText = $"{Info.Name}\tFPS: {Raylib.GetFPS()}\n{Info.Description}";
			int textHeight = 20;
			int defaultSceneTextWidth = Raylib.MeasureText(defaultSceneText, textHeight);
			Raylib.DrawText(defaultSceneText, (int)(center.X - (defaultSceneTextWidth / 2)), (int)(center.Y - (textHeight / 2)), textHeight, Color.Red);
			// Audio device description
			MMDevice defaultAudioDevice = core.audioProcessor.GetDefaultRenderDevice();
			string audioDeviceText = $"Default Audio Device: {defaultAudioDevice.FriendlyName} | Is it playing audio? {core.audioProcessor.IsAudioPlaying(defaultAudioDevice)}";
			int audioDeviceTextWidth = Raylib.MeasureText(audioDeviceText, textHeight);
			Raylib.DrawText(audioDeviceText, (int)(center.X - (defaultSceneTextWidth / 2)), (int)(center.Y + (textHeight * 2)), textHeight, Color.Red);
		}
	}
}
