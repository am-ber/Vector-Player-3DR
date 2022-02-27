using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.ComponentModel;
using Managers;
using System.Reflection;

namespace Core_Project
{
	internal class Drawer : GameWindow
	{
		private int bufferHandle, programHandle, arrayHandle;
		private string vertexShader, pixelShader;
		private Action exitCallBack;
		private Logger log;
		public Drawer(int width, int height, Action onExit, Logger log) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
		{
			this.log = log;
			CenterWindow(new Vector2i(width, height));
			exitCallBack = onExit;
		}
		protected override void OnResize(ResizeEventArgs e)
		{
			log.Log($"Window resized to {e.Width}, {e.Height}");
			GL.Viewport(0, 0, e.Width, e.Height);
			base.OnResize(e);
		}
		protected override void OnLoad()
		{
			GL.ClearColor(Color4.Black);

			LoadShaders();

			float[] vertices = new float[]
			{
				0.0f, 0.5f, 0f,
				0.5f, -0.5f, 0f,
				-0.5f, -0.5f, 0f,
			};

			bufferHandle = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			arrayHandle = GL.GenVertexArray();
			GL.BindVertexArray(arrayHandle);

			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);

			GL.BindVertexArray(0);

			// shader initializing
			int vertexHandle = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(vertexHandle, vertexShader);
			GL.CompileShader(vertexHandle);
			int pixelHandle = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(pixelHandle, pixelShader);
			GL.CompileShader(pixelHandle);

			// attach shaders
			programHandle = GL.CreateProgram();
			GL.AttachShader(programHandle, vertexHandle);
			GL.AttachShader(programHandle, pixelHandle);
			GL.LinkProgram(programHandle);

			// clean up shaders
			GL.DetachShader(programHandle, vertexHandle);
			GL.DetachShader(programHandle, pixelHandle);
			GL.DeleteShader(vertexHandle);
			GL.DeleteShader(pixelHandle);

			base.OnLoad();
			log.Log($"Drawer fully loaded...");
		}
		private void LoadShaders()
		{
			string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			vertexShader = FileManager.GetText(string.Concat(folder, @"\Shaders\VertexShader.vert"));
			pixelShader = FileManager.GetText(string.Concat(folder, @"\Shaders\PixelShader.frag"));
			log.Log($"Shaders loaded from file...");
		}
		protected override void OnUpdateFrame(FrameEventArgs args)
		{
			base.OnUpdateFrame(args);
		}
		protected override void OnRenderFrame(FrameEventArgs args)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.UseProgram(programHandle);
			GL.BindVertexArray(arrayHandle);
			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

			this.Context.SwapBuffers();
			base.OnRenderFrame(args);
		}
		protected override void OnUnload()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.DeleteBuffer(bufferHandle);

			GL.UseProgram(0);
			GL.DeleteProgram(programHandle);

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
