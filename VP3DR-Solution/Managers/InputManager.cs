using Core_Project;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Managers
{
	public class InputManager
	{
		private IDrawer drawer;
		private Action exitCallBack;
		private int previousScrollValue;
		private Matrix rotateL, rotateR;
		public InputManager(IDrawer drawer, Action exitCallBack)
		{
			this.drawer = drawer;
			this.exitCallBack = exitCallBack;

			Init();
		}
		private void Init()
		{
			rotateL = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
			rotateR = Matrix.CreateRotationY(MathHelper.ToRadians(-1f));
		}
		public void CheckInput()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				exitCallBack();
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Left))
			{
				drawer.MoveCamera(Vector3.Transform(drawer.CameraPos(), rotateL));
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				drawer.MoveCamera(Vector3.Transform(drawer.CameraPos(), rotateR));
			}
			if (Mouse.GetState().ScrollWheelValue < previousScrollValue)
			{
				drawer.MoveCamera(drawer.CameraPos() + new Vector3(0, -1, 0));
				drawer.UpdateViewMatrix();
			}
			if (Mouse.GetState().ScrollWheelValue > previousScrollValue)
			{
				drawer.MoveCamera(drawer.CameraPos() + new Vector3(0, 1, 0));
				drawer.UpdateViewMatrix();
			}
			previousScrollValue = Mouse.GetState().ScrollWheelValue;
		}
	}
}
