using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Project
{
	public class Core
	{
		private Drawer _drawer;
		public Core()
		{
			_drawer = new Drawer(1270, 720, "Vector Player 3DR");
		}

		public void Run()
		{
			_drawer.Run();
		}
	}
}
