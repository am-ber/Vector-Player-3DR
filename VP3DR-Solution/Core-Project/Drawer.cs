using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.ComponentModel;
using Managers;
using System.Reflection;

namespace Core_Project
{
	public class Drawer : GameWindow, IDrawer
	{
		private Action exitCallBack;
		private Logger log;
		private SceneManager sceneManager;
		public Drawer(int width, int height, Action onExit, Logger log) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
		{
			this.log = log;
			CenterWindow(new Vector2i(width, height));
			exitCallBack = onExit;
			sceneManager = new SceneManager(log);
		}
		protected override void OnResize(ResizeEventArgs e)
		{
			log.Log($"Window resized to {e.Width}, {e.Height}");
			GL.Viewport(0, 0, e.Width, e.Height);
			base.OnResize(e);
		}
		protected override void OnLoad()
		{
			sceneManager.LoadScenes(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty);
			base.OnLoad();
			log.Log($"Drawer fully loaded...");
		}
		protected override void OnUpdateFrame(FrameEventArgs args)
		{
			sceneManager.activeScene.Update();
			base.OnUpdateFrame(args);
		}
		protected override void OnRenderFrame(FrameEventArgs args)
		{
			sceneManager.activeScene.Draw(this);
			base.OnRenderFrame(args);
		}
		protected override void OnUnload()
		{
			sceneManager.activeScene.Dispose();
			base.OnUnload();
			log.Log($"Drawer fully unloaded...");
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			log.Log($"Closing drawer...");
			exitCallBack();
			base.OnClosing(e);
		}
	}
}
