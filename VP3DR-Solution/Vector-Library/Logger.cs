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
		public string fileName = string.Empty;
		public string directory = string.Empty;
		// private
		private bool spamProtection = true;
		private StreamWriter logFile;
		// methods
		/// <summary>
		/// This handles all the logging for the Core application and every other library function.
		/// It will create a file in a specified directory OR in the executing assembly.
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="directory"></param>
		public Logger(string? fileName = null, string? directory = null)
		{
			// Check for the file directory
			if (directory == null || directory.Equals(string.Empty))
			{
				this.directory = string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"\logs\");
			} else
			{
				this.directory = directory;
			}
			// Try to make the logging folder
			if (FileManager.CreateDirectory(this.directory))
			{
				Log($"Created directory: {this.directory}");
			} else
			{
				Log($"Couldn't make directory: {this.directory}\n This directory might already exist.");
			}
			// Check for the file name
			if (fileName == null || fileName.Equals(string.Empty))
			{
				this.fileName = $"VP3DR_log_{DateTime.Now.ToString("M-dd-yy--HH-mm-ss")}.log";
			} else
			{
				this.fileName = fileName;
			}
			// Try to make the logging file in the directory
			if (FileManager.CreateFile(this.directory, this.fileName))
			{
				Log($"Created file named: {this.fileName}");
				logFile = File.AppendText(this.directory + this.fileName);
			} else
			{
				Log($"Couldn't make the file: {this.fileName} which means there might not be a logging file for this session!", Level.warn);
			}
			initialized = true;
		}
		/// <summary>
		/// Used to log onto the existing log file that was created on initialization.
		/// This will automatically append to the file as well.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		public void Log(string message, Level level = Level.info)
		{
			// Check for any errors or warnings
			StringBuilder sb = new StringBuilder();
			switch (level)
			{
				case Level.warn:
					sb.Append("\t------- warning -------\n");
					break;
				case Level.error:
					sb.Append("\t------- ERROR -------\n");
					break;
			}
			sb.Append(message);
			sb.Append("\n");
			Console.WriteLine(sb.ToString());
			// Append to log file
			if (initialized)
			{
				// Try to write to file
				try
				{
					// Lock the file for thread safety first
					lock (logFile)
					{
						logFile.Write(sb.ToString());
						logFile.Flush();
					}
					spamProtection = true;
				}
				catch (Exception e)
				{
					if (spamProtection)
						Console.WriteLine($"Exception while writting log to file: {e.Message}\n{e.StackTrace}");
					spamProtection = false;
				}
			}
		}
	}
}
