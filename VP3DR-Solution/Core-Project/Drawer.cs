using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.ComponentModel;

namespace Core_Project
{
	internal class Drawer : GameWindow
	{
		private Action exitCallBack;
		public Drawer(int width, int height, Action onExit) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
		{
			CenterWindow(new Vector2i(width, height));
			exitCallBack = onExit;
		}
		protected override void OnResize(ResizeEventArgs e)
		{
			base.OnResize(e);
		}
		protected override void OnUpdateFrame(FrameEventArgs args)
		{
			base.OnUpdateFrame(args);
		}
		protected override void OnRenderFrame(FrameEventArgs args)
		{
			GL.ClearColor(Color4.Black);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			this.Context.SwapBuffers();
			base.OnRenderFrame(args);
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			exitCallBack();
			base.OnClosing(e);
		}
	}
}
