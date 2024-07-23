using System.Diagnostics;
using System.Reflection;
using System.Timers;
using Vector_Library.Managers;
using Vector_Library.Processors;

namespace Vector_Library
{
	public class Core
	{
		public static Core Instance;
		// public
		public static readonly double version = 0.1;
		public Logger logger;
		public bool logActive = false;
		// private
		private MonoDrawer drawer;
		private Stopwatch sw = new Stopwatch();
		private SceneManager sceneManager;
		private InputManager inputManager;
		// methods
		public Core()
		{
			if (Instance != null)
			{
				Instance.logger.Log("A new instance of core was started but one already exists!\n" +
					"Kill the existing instance first if you want to start a new Core!", Logger.Level.error);
				return;
			}
			sw.Start();
			InitializeLogging();
			sw.Stop();
			logger.Log($"Initialized logging and profiling in {sw.ElapsedMilliseconds} ms");
			sw.Start();
			InitializeDrawWindow();
			InitializeManagers();
			sw.Stop();
			logger.Log($"Initialized everything else in {sw.ElapsedMilliseconds} ms");

			Instance = this;
		}
		#region Initializers
		private void InitializeLogging()
		{
			// create log file
			try
			{
				logger = new Logger();
				logger.Log($"Initializing version {version}...");
				logActive = true;
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error creating logger, no logging will be done for this session: {e.Message}\n{e.StackTrace}");
			}
		}
		private void InitializeDrawWindow()
		{
			try
			{
				drawer = new MonoDrawer(1280, 720, logger);
			}
			catch (Exception e)
			{
				logger.Log($"Couldn't make drawer: {e.Message}\n{e.StackTrace}");
			}
		}
		private void InitializeManagers()
		{
			inputManager = new InputManager();
			sceneManager = new SceneManager(logger);
			sceneManager.LoadScenes();
		}
		#endregion
		/// <summary>
		/// Runs the core execution of the Vector Player Library.
		/// This is expected to be the entry point after creating
		/// an instance of the Core.
		/// </summary>
		public void Run()
		{
			try
			{

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
	}
}
