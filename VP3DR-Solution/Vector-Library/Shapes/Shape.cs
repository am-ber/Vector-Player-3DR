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
		private PrimitiveType pType;
		public Shape(GraphicsDevice graphics, PrimitiveType pType, Color c, params Vector3[] vertices)
		{
			shapeVertices = new VertexPositionColor[vertices.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				shapeVertices[i] = new VertexPositionColor(vertices[i], c);
			}
			InitBuffer(graphics, pType);
		}
		public Shape(GraphicsDevice graphics, PrimitiveType pType, params VertexPositionColor[] shapeVertices)
		{
			this.shapeVertices = shapeVertices;
			InitBuffer(graphics, pType);
		}
		private void InitBuffer(GraphicsDevice graphics, PrimitiveType pType)
		{
			buffer = new VertexBuffer(graphics, typeof(VertexPositionColor), shapeVertices.Length, BufferUsage.WriteOnly);
			buffer.SetData(shapeVertices);
			this.graphics = graphics;
			this.pType = pType;
		}
		public void Draw()
		{
			graphics.SetVertexBuffer(buffer);
			graphics.DrawPrimitives(pType, 0, shapeVertices.Length);
		}
	}
}
