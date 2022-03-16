using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Core_Project
{
	public interface IDrawer
	{
		protected enum Level
		{
			info,
			warn,
			error
		}
		public GraphicsDevice GetGraphicsDevice();
		public void CameraLookAt(float x, float y, float z);
		public void CameraLookAt(Vector3 pos);
		public Vector3 CameraPos();
		public void MoveCamera(float x, float y, float z);
		public void MoveCamera(Vector3 pos);
		public void UpdateViewMatrix();
		protected void Log(string message, Level level = Level.info);
	}
}
