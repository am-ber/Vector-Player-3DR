using System;
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
			
		}
		public void Update()
		{

		}
		public void Draw(IDrawer d)
		{
			
		}
		public void Dispose()
		{
			
		}
	}
}
