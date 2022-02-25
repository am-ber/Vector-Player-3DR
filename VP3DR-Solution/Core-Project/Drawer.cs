using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using System.ComponentModel;

namespace Core_Project
{
	internal class Drawer : GameWindow
	{
		public Drawer(int width, int height, string title) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
		{
			
			Load += load;
			UpdateFrame += update;
			RenderFrame += renderFrame;
			Closing += close;
		}
		private void load()
		{

		}
		private void update(FrameEventArgs args)
		{

		}
		private void renderFrame(FrameEventArgs args)
		{

		}
		private void close(CancelEventArgs args)
		{

		}
	}
}
