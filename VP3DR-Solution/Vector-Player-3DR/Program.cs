using System;
using Vector_Library;
class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("Launching Vector Player 3D Remaster...");
		Core core = new Core();
		core.Run();
		Console.Write("Press any key to continue...");
		Console.ReadLine(); // For pausing the console before closing.
	}
}