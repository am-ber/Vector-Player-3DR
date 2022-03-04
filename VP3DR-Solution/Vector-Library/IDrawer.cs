using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.ComponentModel;
using System.Reflection;

namespace Core_Project
{
	public interface IDrawer
	{
		public IGLFWGraphicsContext Context { get; }
	}
}
