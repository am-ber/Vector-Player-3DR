using System;
using Vector_Library;
class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("Launching Vector Player 3D Remaster...");
		Core core = new Core((1280, 720));
		core.Run();
	}
}