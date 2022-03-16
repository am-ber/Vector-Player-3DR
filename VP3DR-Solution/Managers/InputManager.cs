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
		private Action<Keys> inputCallback;
		private int previousScrollValue;
		private Matrix rotateL, rotateR;
		public InputManager(IDrawer drawer, Action exitCallBack, Action<Keys> inputCallback)
		{
			this.drawer = drawer;
			this.exitCallBack = exitCallBack;
			this.inputCallback = inputCallback;
			Init();
		}
		private void Init()
		{
			rotateL = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
			rotateR = Matrix.CreateRotationY(MathHelper.ToRadians(-1f));
		}
		public void CheckInput()
		{
			// keyboard input
			if (Keyboard.GetState().GetPressedKeyCount() > 0)
			{
				HandleMultipleKeys(Keyboard.GetState().GetPressedKeys());
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
				if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) &&
					Keyboard.GetState().IsKeyDown(Keys.Enter))
				{
					drawer.ToggleFullscreen();
				}
			}
			// mouse input
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
		private void HandleMultipleKeys(Keys[] keys)
        {
			foreach(Keys key in keys)
            {
				inputCallback(key);
			}
        }
	}
}
