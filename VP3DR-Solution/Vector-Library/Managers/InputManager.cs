using System;
using Raylib_cs;

namespace Vector_Library.Managers
{
	public class InputManager
	{
		private Logger logger;
		public InputManager(Core core)
		{
			logger = core.logger;
			logger.Log("Input Manager Initialized...");
		}
	}
}
