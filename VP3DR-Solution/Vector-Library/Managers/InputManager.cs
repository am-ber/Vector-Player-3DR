﻿using Vector_Library.Interfaces;
using Raylib_cs;
using Vector_Library.Arithmetic;

namespace Vector_Library.Managers
{
	public class InputManager
	{
		/// <summary>
		/// Used for loading 
		/// </summary>
		enum InputMapping
		{
			Exit = KeyboardKey.Escape,
			NextScene = KeyboardKey.E,
			PreviousScene = KeyboardKey.Q,
			ToggleHardwareMonitor = KeyboardKey.M,
			ToggleSceneInformation = KeyboardKey.F1,
		}
		private Logger logger;
		private Core core;
		public InputManager(Core core)
		{
			this.core = core;
			logger = core.logger;
			Raylib.SetExitKey(KeyboardKey.Null);
			logger.Log("Input Manager Initialized...");
		}
		public void CheckInput(Scene scene)
		{
			// Grab the current keypress and print it
			int keyPressed = Raylib.GetKeyPressed();
			if (keyPressed != 0)
				logger.Log($"\tKey Pressed: {Enum.GetName(typeof(KeyboardKey), keyPressed)}");

			int sceneIndexMover = 0;
			// Switch for specific keys being pressed
			switch (keyPressed)
			{
				case (int)InputMapping.Exit:
					logger.Log("Exit toggled...");
					core.continueRunning = false;
					break;
				// A fancy way to not rewrite 2 lines of code so I can use modulo
				case (int)InputMapping.PreviousScene:
					sceneIndexMover -= 2;
					goto case (int)InputMapping.NextScene;
				case (int)InputMapping.NextScene:
					sceneIndexMover++;
					int currentSceneIndex = core.sceneManager.LoadedScenes.IndexOf(core.sceneManager.ActiveScene);
					int nextSceneIndex = MathmaticalExtentions.WheelMod((sceneIndexMover + currentSceneIndex), core.sceneManager.LoadedScenes.Count);
					logger.Log($"\tCurrent index is: {currentSceneIndex}, moving to index: {nextSceneIndex}");
					core.sceneManager.SetActiveScene(nextSceneIndex);
					break;
				case (int)InputMapping.ToggleSceneInformation:
					logger.Log("\t" + (scene.drawSceneInformation ? "Hidding" : "Showing") + " current scene information...");
					scene.drawSceneInformation = !scene.drawSceneInformation;
					break;
			}
		}
	}
}
