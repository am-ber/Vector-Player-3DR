using System;
using static Vector_Library.IScene;
using System.Reflection;
using Core_Project;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Vector_Library
{
	public class DefaultScene : IScene
	{
		private SceneInfo info;
		private List<Shape> shapes;
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
			shapes = new List<Shape>();
		}
		public void Load(IDrawer d)
		{
			shapes.Add(new Shape(d.GetGraphicsDevice(), PrimitiveType.TriangleList,
				new VertexPositionColor(new Vector3(0, 20, 0), Color.Red), 
				new VertexPositionColor(new Vector3(-20, -20, 0), Color.Green),
				new VertexPositionColor(new Vector3(20, -20, 0), Color.Blue)
				));
		}
		public void Update()
		{

		}
		public void Draw(IDrawer d)
		{
			d.CameraLookAt(0, 0, 0);
			d.MoveCamera(0, 20, -100);
			// draw all shapes
			foreach (Shape s in shapes)
			{
				s.Draw();
			}
		}
		public void Dispose()
		{
			
		}
	}
}
