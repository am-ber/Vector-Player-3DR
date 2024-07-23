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
		private Logger logger;
		public SceneManager(Logger logger)
		{
			this.logger = logger;
			loadedScenes = new List<IScene>();
			// set fallback scene
			fallBack = new DefaultScene();
			loadedScenes.Add(fallBack);
			SetActiveScene(0);
			logger.Log("SceneManager initialized...");
		}
		public void LoadScenes(string? folderLocation = null)
		{
			// Check for folderLocation
			if (folderLocation == null || folderLocation.Equals(string.Empty))
			{
				string sceneFolder = @"\Scenes";
				folderLocation = string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), sceneFolder);
			}
			try
			{
				// check if a scene folder exists, and make it if it doesn't and back out
				if (FileManager.CreateDirectory(folderLocation))
				{
					logger.Log($"There wasn't a scenes folder in {folderLocation}, so one was created. Please move all Scene .dll's to that location.", Logger.Level.warn);
					return;
				}
				// load all files from the scene folder
				logger.Log($"Loading scenes from {folderLocation}...");
				string[] sceneFiles = Directory.GetFiles(folderLocation);
				List<string> actualDlls = new List<string>();
				foreach (string s in sceneFiles)
				{
					string fileExt = Path.GetExtension(s);
					if (fileExt == ".dll")
					{
						actualDlls.Add(s);
					}
				}
				logger.Log($"Found {sceneFiles.Length} scenes.");
				// populate scenes
				foreach (string s in actualDlls)
				{
					logger.Log($"Attempting to bind {s} to a scene object.");
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
						logger.Log($"Couldn't load file {Path.GetFileName(s)} because {e.Message}", Logger.Level.warn);
					}
				}
			}
			catch (Exception e)
			{
				logger.Log($"Error in loading scenes: {e.Message}\n{e.StackTrace}", Logger.Level.error);
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
				logger.Log($"Couldn't load scene at {index} index.\n{e.Message}\n{e.StackTrace}", Logger.Level.error);
				return false;
			}
		}
	}
}
