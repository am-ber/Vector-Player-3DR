

namespace Vector_Library.Interfaces
{
	public interface IScene : IDisposable
	{
		public struct SceneInfo
		{
			public int index;
			public string Name;
			public string Description;

			public SceneInfo(int index, string name, string description)
			{
				this.index = index;
				Name = name;
				Description = description;
			}
			public SceneInfo(int index, SceneInfo info)
			{
				this.index = index;
				Name = info.Name;
				Description = info.Description;
			}
		}
		public SceneInfo Info { get; }
		/// <summary>
		/// Used as an initializer.
		/// </summary>
		public void Load();
		/// <summary>
		/// Runs in a loop before the draw method.
		/// </summary>
		public void Update();
		/// <summary>
		/// Runs in a loop to draw onto the screen.
		/// </summary>
		public void Draw();
	}
}
