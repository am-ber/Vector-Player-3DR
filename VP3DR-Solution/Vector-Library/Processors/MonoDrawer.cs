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
		private Action exitCallBack;
		private Logger log;
		private SceneManager sceneManager;
		private InputManager im;
		public MonoDrawer(int width, int height, Action onExit, Logger log)
		{
			this.log = log;
			exitCallBack = onExit;

			defaultWindowSize = (width, height);
		}
		#region Initializers
		public void Initialize()
		{
			log.Log("Drawer Initialized...");
		}

		#endregion
		public void LoadContent()
		{
			sceneManager = new SceneManager(log);
			im = new InputManager();
			log.Log("Drawer content Loaded...");
		}
		public void Update()
		{
			im.CheckInput();
		}
		public void Draw()
		{
			
		}
		protected void OnExiting(object sender, EventArgs args)
		{
			log.Log("");
			exitCallBack();
		}
	}
}
