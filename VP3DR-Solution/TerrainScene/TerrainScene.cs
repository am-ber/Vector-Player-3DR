using Core_Project;
using Vector_Library;
using static Vector_Library.IScene;

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
		public void Load(IDrawer d)
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
