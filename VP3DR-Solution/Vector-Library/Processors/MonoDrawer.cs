using System.ComponentModel;
using System.Reflection;
using Vector_Library.Managers;

namespace Vector_Library.Processors
{
	public class MonoDrawer
	{
		private bool fullscreen = false;
		private (int, int) defaultWindowSize;
		// objects
		private Logger log;
		public MonoDrawer(int width, int height, Logger log)
		{
			this.log = log;

			defaultWindowSize = (width, height);
		}
		public void Initialize()
		{
			log.Log("Drawer Initialized...");
		}
		public void LoadContent()
		{
			log.Log("Drawer content Loaded...");
		}
		public void Update()
		{
			
		}
		public void Draw()
		{
			
		}
	}
}
