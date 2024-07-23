using System;
using System.IO;

namespace Vector_Library.Managers
{
	public static class FileManager
	{
		public static string GetText(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return string.Empty;
			}
			string sender = string.Empty;
			try
			{
				sender = File.ReadAllText(filePath);
			}
			catch(Exception e)
			{
				Console.WriteLine($"Can't read file: {e.Message}\n{e.StackTrace}");
			}
			return sender;
		}
		public static bool CreateDirectory(string directory)
		{
			try
			{
				if (Directory.Exists(directory))
				{
					return false;
				}
				Directory.CreateDirectory(directory);
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}
		public static bool CreateFile(string path, string fileName)
		{
			try
			{
				FileStream f = File.Create(path + fileName);
				f.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
				return false;
			}
		}
	}
}
