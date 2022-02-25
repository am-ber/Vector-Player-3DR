using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_Library
{
	public interface IScene
	{
		public void Draw();
		public void Update();
		public void Dispose();
	}
}
