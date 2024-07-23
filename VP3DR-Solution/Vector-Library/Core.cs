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
		public int iterationsPerSecond;
		public (int width, int height) defaultWindowSize;
		// private
		private SceneProcessor sceneProcessor;
		private Stopwatch macroWatch, microWatch;
		private SceneManager sceneManager;
		private InputManager inputManager;
		// methods
		public Core((int width, int height) defaultWindowSize)
		{
			if (Instance != null)
			{
				Instance.logger.Log("A new instance of core was started but one already exists!\n" +
					"Kill the existing instance first if you want to start a new Core!", Logger.Level.error);
				return;
			}
			logActive = false;
			macroWatch = new Stopwatch();
			microWatch = new Stopwatch();
			// Start stop watches for analytics
			macroWatch.Start();
			InitializeLogging();
			InitializeManagers();
			InitializeProcessors();
			macroWatch.Stop();
			logger.Log($"Initialized everything in {macroWatch.ElapsedMilliseconds} ms");

			Instance = this;
			this.defaultWindowSize = defaultWindowSize;
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
			sceneProcessor = new SceneProcessor(defaultWindowSize.width, defaultWindowSize.height, logger);
			sceneProcessor.Initialize();
			logger.Log($"Initialized Scene Processing in {microWatch.ElapsedMilliseconds} ms");
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
					sceneProcessor.Draw(sceneManager.activeScene);
					microWatch.Stop();
					SetIterationCounter(microWatch.ElapsedMilliseconds);
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
			logger.Log("Closing application...");
		}
		private void SetIterationCounter(long ellapsedMilliseconds)
		{
			try
			{
				iterationsPerSecond = (int)MathF.Round(1000 / ellapsedMilliseconds);
			}
			catch (DivideByZeroException)
			{
				iterationsPerSecond = 1000;
			}
		}
	}
}
