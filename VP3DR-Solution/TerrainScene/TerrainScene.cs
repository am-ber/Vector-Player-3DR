using Vector_Library;
using Vector_Library.Interfaces;
using static Vector_Library.Interfaces.Scene;

namespace Terrain
{
    public class TerrainScene : Scene
	{
		public TerrainScene() : this(Core.Instance) { }

		public TerrainScene(Core core)
		{
			this.core = core;
			logger = core.logger;
			Info = new SceneInfo()
			{
				Name = "Terrain Generator",
				Description = "Fly over an infinite generation of terrain with the original vector landscape that started it all."
			};
		}
		public override void Load()
		{

		}
		public override void Update()
		{

		}
		public override void Draw()
		{
			
		}
	}
}
