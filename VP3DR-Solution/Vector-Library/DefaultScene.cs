using System;
using static Vector_Library.Interfaces.IScene;
using System.Reflection;
using Vector_Library.Interfaces;

namespace Vector_Library
{
    public class DefaultScene : IScene
	{
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
		public void Draw()
		{
		}
		public void Dispose()
		{
		}
	}
}
