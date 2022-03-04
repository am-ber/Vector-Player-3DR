using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using static Vector_Library.IScene;
using System.Reflection;
using Core_Project;

namespace Vector_Library
{
	public class DefaultScene : IScene
	{
		private int bufferHandle = 0, programHandle = 0, arrayHandle = 0;
		private string vertexShader = string.Empty, pixelShader = string.Empty;
		private SceneInfo info;
		SceneInfo IScene.Info
		{
			get => info;
			set => info = value;
		}
		public DefaultScene()
		{
			info = new SceneInfo()
			{
				Name = "Default Scene",
				Description = "Enjoy the generic equalizer scene without needing the fancy business of hard GPU usage."
			};
		}
		public void Load()
		{
			GL.ClearColor(Color4.Black);

			string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			vertexShader = File.ReadAllText(string.Concat(folder, @"\Shaders\VertexShader.vert"));
			pixelShader = File.ReadAllText(string.Concat(folder, @"\Shaders\PixelShader.frag"));
			Console.WriteLine($"Shaders loaded from file...");

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
		}
		public void Update()
		{

		}
		public void Draw(IDrawer d)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.UseProgram(programHandle);
			GL.BindVertexArray(arrayHandle);
			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

			d.Context.SwapBuffers();
		}
		public void Dispose()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.DeleteBuffer(bufferHandle);

			GL.UseProgram(0);
			GL.DeleteProgram(programHandle);
		}
	}
}
