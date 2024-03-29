﻿using System;
using System.IO;

namespace Managers
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
		public static bool ForceDir(string directory)
		{
			try
			{
				Directory.CreateDirectory(directory);
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}
	}
}
