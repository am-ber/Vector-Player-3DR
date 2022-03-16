using Managers;
using System.Diagnostics;
using System.Timers;

namespace Core_Project
{
	public class Core
	{
		public static Core Instance = new Core();
		// public
		public static readonly double version = 0.1;
		public Logger log;
		public bool logActive = false;
		// private
		private MonoDrawer drawer;
		private System.Timers.Timer timer;
		private Stopwatch sw = new Stopwatch();
		// methods
		public Core()
		{
			sw.Start();
			InitializeLogging();
			InitializeProfiling();
			sw.Stop();
			Log($"Initialized logging and profiling in {sw.ElapsedMilliseconds}m");
			sw.Start();
			InitializeDrawWindow();
			sw.Stop();
			Log($"Initialized drawing window in {sw.ElapsedMilliseconds}m");
		}
		private void InitializeLogging()
		{
			// create log file
			try
			{
				log = new Logger($"VP3DR_log_{DateTime.Now.ToString("M-dd-yy--HH-mm-ss")}.log");
				log.Log($"Initializing version {version}...");
				logActive = true;
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error creating logger, no logging will be done for this session: {e.Message}\n{e.StackTrace}");
			}
		}
		private void InitializeProfiling()
		{
			try
			{
				// create log writting event every 10 seconds
				timer = new System.Timers.Timer(10000);
				timer.Elapsed += WriteEvent;
				timer.AutoReset = true;
				timer.Enabled = true;
			}
			catch (Exception e)
			{
				Log($"Error creating timer event: {e.Message}\n{e.StackTrace}", Logger.Level.error);
			}
		}
		private static void WriteEvent(Object source, ElapsedEventArgs e)
		{
			Instance.log.Write();
		}
		private void InitializeDrawWindow()
		{
			try
			{
				drawer = new MonoDrawer(1280, 720, Exit, log);
			}
			catch(Exception e)
			{
				Log($"Couldn't make drawer: {e.Message}\n{e.StackTrace}");
			}
		}
		public void Run()
		{
			try
			{
				drawer.Run();
			}
			catch(Exception e)
			{
				Log($"Error running drawer: {e.Message}\n{e.StackTrace}");
			}
		}
		public void Exit()
		{
			Log("Closing application...");
			if (logActive)
			{
				timer.Stop();
				log.Write();
			}
		}
		public void Log(string message, Logger.Level level = Logger.Level.info)
		{
			if (logActive)
			{
				log.Log(message, level);
			}
			else
			{
				Console.WriteLine(message);
			}
		}
	}
}
