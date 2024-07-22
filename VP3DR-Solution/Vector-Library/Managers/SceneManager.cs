using System;
using System.Reflection;
using Vector_Library;
using Vector_Library.Interfaces;

namespace Vector_Library.Managers
{
    public class SceneManager
	{
		public IScene activeScene;
		private IScene fallBack;
		private List<IScene> loadedScenes;
		private Logger log;
		public SceneManager(Logger log)
		{
			this.log = log;
			loadedScenes = new List<IScene>();
			// set fallback scene
			fallBack = new DefaultScene();
			loadedScenes.Add(fallBack);
			SetActiveScene(0);
			Log("SceneManager initialized...");
		}
		public void LoadScenes(string folderLocation)
		{
			string sceneFolder = @"\Scenes";
			try
			{
				// check if a scene folder exists, and make it if it doesn't and back out
				if (FileManager.ForceDir(string.Concat(folderLocation, sceneFolder)))
				{
					Log($"There wasn't a scenes folder in {folderLocation}, so one was created. Please move all Scene .dll's to that location.", Logger.Level.warn);
					return;
				}
				// load all files from the scene folder
				Log($"Loading scenes from {string.Concat(folderLocation, sceneFolder)}...");
				string[] sceneFiles = Directory.GetFiles(string.Concat(folderLocation, sceneFolder));
				List<string> actualDlls = new List<string>();
				foreach (string s in sceneFiles)
				{
					string fileExt = Path.GetExtension(s);
					if (fileExt == ".dll")
					{
						actualDlls.Add(s);
					}
				}
				// populate scenes
				foreach (string s in actualDlls)
				{
					try
					{
						// load the assembly and check type
						Assembly dll = Assembly.LoadFile(s);
						foreach (Type type in dll.GetTypes())
						{
							if (type.IsAssignableFrom(typeof(IScene)))
							{
								// add it to the list if its an IScene
								dynamic? c = Activator.CreateInstance(type);
								if (c != null)
									loadedScenes.Add((IScene)c);
							}
						}
					}
					catch (Exception e)
					{
						Log($"Couldn't load file {Path.GetFileName(s)} because {e.Message}", Logger.Level.warn);
					}
				}
			}
			catch (Exception e)
			{
				Log($"Error in loading scenes: {e.Message}\n{e.StackTrace}", Logger.Level.error);
			}
		}
		public bool SetActiveScene(int index)
		{
			try
			{
				// dispose the old scene
				if (activeScene != null)
					activeScene.Dispose();
				// bring in the new one from the index provided
				activeScene = loadedScenes[index];
				activeScene.Load();
				return true;
			}
			catch (Exception e)
			{
				activeScene = fallBack;
				activeScene.Load();
				Log($"Couldn't load scene at {index} index.\n{e.Message}\n{e.StackTrace}", Logger.Level.error);
				return false;
			}
		}
		private void Log(string message, Logger.Level level = Logger.Level.info)
		{
			log.Log(message, level);
		}
	}
}
