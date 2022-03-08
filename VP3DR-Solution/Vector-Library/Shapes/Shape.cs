using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Vector_Library
{
	internal class Shape
	{
		private VertexBuffer buffer;
		private VertexPositionColor[] shapeVertices { get; }
		private GraphicsDevice graphics;
		public Shape(GraphicsDevice graphics, Color c, params Vector3[] vertices)
		{
			shapeVertices = new VertexPositionColor[vertices.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				shapeVertices[i] = new VertexPositionColor(vertices[i], c);
			}
			InitBuffer(graphics);
		}
		public Shape(GraphicsDevice graphics, params VertexPositionColor[] shapeVertices)
		{
			this.shapeVertices = shapeVertices;
			InitBuffer(graphics);
		}
		private void InitBuffer(GraphicsDevice graphics)
		{
			buffer = new VertexBuffer(graphics, typeof(VertexPositionColor), shapeVertices.Length, BufferUsage.WriteOnly);
			this.graphics = graphics;
		}
		public void Draw()
		{
			graphics.SetVertexBuffer(buffer);
		}
	}
}
