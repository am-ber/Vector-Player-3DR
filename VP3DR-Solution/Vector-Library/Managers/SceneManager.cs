using System;
using System.Reflection;
using System.Text;
using Vector_Library;
using Vector_Library.Interfaces;
using Vector_Library.Processors;

namespace Vector_Library.Managers
{
	public class SceneManager
	{
		public IScene activeScene;
		// Privates
		private IScene fallBack;
		private List<IScene> loadedScenes;
		private Logger logger;
		private SceneProcessor sceneProcessor;
		public SceneManager(Core core, SceneProcessor sceneProcessor)
		{
			logger = core.logger;
			this.sceneProcessor = sceneProcessor;
			loadedScenes = new List<IScene>();
			// set fallback scene
			fallBack = new DefaultScene(core);
			loadedScenes.Add(fallBack);
			SetActiveScene(0);
			logger.Log("SceneManager initialized...");
		}
		/// <summary>
		/// Used to load <seealso cref="IScene"/> objects dynamically from a folder.
		/// If no folder is given we will check in ..\Scenes where ".." is the executing assembly.
		/// </summary>
		/// <param name="folderLocation"></param>
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
					logger.Log($"There wasn't a folder named {folderLocation}, so one was created. Please move all Scene .dll's to that location.", Logger.Level.warn);
					return;
				}
				// load all files from the scene folder
				logger.Log($"Loading scenes from {folderLocation}...");
				string[] sceneFiles = Directory.GetFiles(folderLocation);
				List<string> foundDlls = new List<string>();
				foreach (string file in sceneFiles)
				{
					string fileExt = Path.GetExtension(file);
					if (fileExt == ".dll")
					{
						foundDlls.Add(file);
					}
				}
				logger.Log($"Found {sceneFiles.Length} potential new scenes.");
				// populate scenes
				foreach (string dllFile in foundDlls)
				{
					logger.Log($"Attempting to bind {dllFile} to a scene object.");
					try
					{
						// load the assembly and check type
						Assembly dll = Assembly.LoadFile(dllFile);
						foreach (Type type in dll.GetTypes())
						{
							// add it to the list if its an IScene
							dynamic? instanceType = Activator.CreateInstance(type);
							if (instanceType != null)
							{
								// check if the instance type is of IScene so we can add it to the list
								if (instanceType.GetType().IsAssignableTo(typeof(IScene)))
								{
									IScene scene = (IScene)instanceType;
									loadedScenes.Add(scene);
									logger.Log($"Successfully added {scene.Info.Name}");
								}
							}
						}
					}
					catch (Exception e)
					{
						logger.Log($"Couldn't load file {Path.GetFileName(dllFile)} because {e.Message}", Logger.Level.warn);
					}
				}
			}
			catch (Exception e)
			{
				logger.Log($"Error in loading scenes: {e.Message}\n{e.StackTrace}", Logger.Level.error);
			}
			// Iterate through the list of scenes to log what all we have in memory
			StringBuilder printableSceneList = new StringBuilder();
			foreach (IScene scene in loadedScenes)
			{
				printableSceneList.Append($"{scene.Info.Name}, ");
			}
			logger.Log($"Currently loaded scenes ({loadedScenes.Count}): \n\t{printableSceneList.ToString()}");
		}
		/// <summary>
		/// Returns wither it successfully added the scene given to the active scene or not.
		/// </summary>
		/// <param name="scene"></param>
		/// <returns></returns>
		public bool SetActiveScene(IScene scene)
		{
			return SetActiveScene(loadedScenes.IndexOf(scene));
		}
		/// <summary>
		/// Returns wither it successfully added the index of the scene desired to the active scene or not.
		/// </summary>
		/// <param name="scene"></param>
		/// <returns></returns>
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
