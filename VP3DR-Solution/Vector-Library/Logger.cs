using System.Reflection;
using System.Text;
using Vector_Library.Managers;

namespace Vector_Library
{
	public class Logger
	{
		public enum Level
		{
			info,
			warn,
			error
		}
		public bool initialized = false;
		// private
		private string fileName = string.Empty;
		private string? directory = string.Empty;
		private StringBuilder log = new StringBuilder();
		private bool spamProtection = true;
		// methods
		public Logger(string fileName)
		{
			this.fileName = fileName;
			directory = string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"\logs\");
			// check if the logging folder is there
			
			if (FileManager.ForceDir(directory))
			{
				Log($"Created directory: {directory}");
			}
			else
			{
				Log($"Couldn't make directory: {directory}");
			}
			initialized = true;
		}
		public void Log(string message, Level level = Level.info)
		{
			// check for any errors or warnings
			string addition = string.Empty;
			switch (level)
			{
				case Level.warn:
					addition = "\t------- warning -------\n";
					break;
				case Level.error:
					addition = "\t------- ERROR -------\n";
					break;
			}
			// write to console
			Console.Write(addition);
			Console.WriteLine(message);
			// append to log file
			if (initialized)
			{
				log.Append(addition);
				log.AppendLine(message);
			}
		}
		public bool Write()
		{
			// if there's no buffer then don't need to do anything
			if (log.Length <= 0)
			{
				return true;
			}
			// try to write to file
			try
			{
				File.AppendAllText(@$"{directory}{fileName}", log.ToString());
				log.Clear();
				spamProtection = true;
				return true;
			}
			catch (Exception e)
			{
				if (spamProtection)
					Console.WriteLine($"Exception while writting log to file: {e.Message}\n{e.StackTrace}");
				spamProtection = false;
			}
			return false;
		}
	}
}
