namespace Vector_Library.Interfaces
{
	public abstract class Scene : IDisposable
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
		public (int width, int height) windowSize;
		protected Logger logger;
		protected Core core;
		public SceneInfo Info { get; protected set; }
		/// <summary>
		/// Used as an initializer.
		/// </summary>
		public abstract void Load();
		/// <summary>
		/// Runs in a loop before the draw method.
		/// </summary>
		public abstract void Update();
		/// <summary>
		/// Runs in a loop to draw onto the screen.
		/// </summary>
		public abstract void Draw();
		/// <summary>
		/// For cleaning up resources when a scene is discarded.
		/// </summary>
		public virtual void Dispose() { }
	}
}
