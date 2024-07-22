using Vector_Library.Interfaces;
using static Vector_Library.Interfaces.IScene;

namespace Terrain
{
    public class TerrainScene : IScene
	{
		private SceneInfo info;
		SceneInfo IScene.Info
		{
			get => info;
			set => info = value;
		}
		public TerrainScene()
		{
			info = new SceneInfo()
			{
				Name = "Terrain Generator",
				Description = "Fly over an infinite generation of terrain with the original vector landscape that started it all."
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
