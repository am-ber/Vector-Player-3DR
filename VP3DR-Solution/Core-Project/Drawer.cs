﻿using System.ComponentModel;
using Managers;
using System.Reflection;

namespace Core_Project
{
	public class Drawer : IDrawer
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
		public void Run()
		{

		}
	}
}
