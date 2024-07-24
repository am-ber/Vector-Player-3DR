using System.Diagnostics;
using Raylib_cs;
using Vector_Library.Managers;
using Vector_Library.Processors;

namespace Vector_Library
{
	public class Core
	{
		public static Core Instance;
		// public
		public static readonly double version = 0.2;
		public Logger logger;
		public bool logActive;
		public AudioProcessor audioProcessor;
		public SceneManager sceneManager;
		public InputManager inputManager;
		public SceneProcessor sceneProcessor;
		// private
		private Stopwatch macroWatch, microWatch;
		// methods
		public Core()
		{
			if (Instance != null)
			{
				Instance.logger.Log("A new instance of core was started but one already exists!\n" +
					"Kill the existing instance first if you want to start a new Core!", Logger.Level.error);
				return;
			}
			// After singleton pattern checking we initialize the rest of the object.
			Instance = this;

			logActive = false;
			macroWatch = new Stopwatch();
			microWatch = new Stopwatch();
			// Start stop watches for analytics
			macroWatch.Start();
			InitializeLogging();
			InitializeProcessors();
			InitializeManagers();
			macroWatch.Stop();
			logger.Log($"Initialized everything in {macroWatch.ElapsedMilliseconds} ms");
		}
		#region Initializers
		private void InitializeLogging()
		{
			microWatch.Restart();
			// create log file
			logger = new Logger();
			logger.Log($"Initializing version {version}...");
			logActive = true;
			logger.Log($"Initialized logging in {microWatch.ElapsedMilliseconds} ms");
			microWatch.Stop();
		}
		private void InitializeManagers()
		{
			// Input Manager
			microWatch.Restart();
			inputManager = new InputManager(this);
			// Scene Manager
			logger.Log($"Initialized Input Manager in {microWatch.ElapsedMilliseconds} ms");
			microWatch.Restart();
			sceneManager = new SceneManager(this);
			sceneManager.LoadScenes();
			logger.Log($"Initialized Scene Manager in {microWatch.ElapsedMilliseconds} ms");
			microWatch.Stop();
		}
		private void InitializeProcessors()
		{
			// Scene Processor
			microWatch.Restart();
			sceneProcessor = new SceneProcessor(this);
			logger.Log($"Initialized Scene Processing in {microWatch.ElapsedMilliseconds} ms");
			microWatch.Restart();
			audioProcessor = new AudioProcessor(this);
			logger.Log($"Initialized Audio Processing in {microWatch.ElapsedMilliseconds} ms");
			microWatch.Stop();
		}
		#endregion
		/// <summary>
		/// Runs the core execution of the Vector Player Library.
		/// This is expected to be the entry point after creating
		/// an instance of the Core.
		/// </summary>
		public void Run()
		{
			logger.Log("\n----------------------\nRunning Core method...\n----------------------\n");
			try
			{
				while (!Raylib.WindowShouldClose())
				{
					microWatch.Restart();
					sceneProcessor.Update();
					sceneProcessor.Draw();
					microWatch.Stop();
				}
			}
			catch (Exception e)
			{
				logger.Log($"Error running drawer: {e.Message}\n{e.StackTrace}");
			}
			Exit();
		}
		public void Exit()
		{
			sceneManager.Exit();
			logger.Log("Closing application...");
		}
	}
}
