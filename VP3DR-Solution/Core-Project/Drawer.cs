using System.ComponentModel;
using Managers;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core_Project
{
	public class Drawer : Game, IDrawer
	{
		private Action exitCallBack;
		private Logger log;
		private SceneManager sceneManager;

		public Drawer(int width, int height, Action onExit, Logger log) 
		{
			this.log = log;
			exitCallBack = onExit;
			sceneManager = new SceneManager(log);
		}
	}
}
