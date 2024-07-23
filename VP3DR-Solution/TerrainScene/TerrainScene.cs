using Vector_Library;
using Vector_Library.Interfaces;
using static Vector_Library.Interfaces.IScene;

namespace Terrain
{
    public class TerrainScene : IScene
	{
		public SceneInfo Info { get; }
		private Core core;
		private Logger logger;
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
