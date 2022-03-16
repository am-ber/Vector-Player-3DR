using System.ComponentModel;
using Managers;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core_Project
{
	public class Drawer : Game, IDrawer
	{
		// objects
		private Action exitCallBack;
		private Logger log;
		private SceneManager sceneManager;
		private InputManager im;
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		// camera variables
		private float fov = MathHelper.ToRadians(70f);
		private Vector3 camTarget, camPosition;
		private Matrix projectionMatrix, viewMatrix, worldMatrix;
		private BasicEffect basicEffect;
		public Drawer(int width, int height, Action onExit, Logger log) 
		{
			this.log = log;
			exitCallBack = onExit;

			graphics = new GraphicsDeviceManager(this);
			graphics.PreferMultiSampling = true;
			Content.RootDirectory = "Content";
		}
		#region Initializers
		protected override void Initialize()
		{
			base.Initialize();
			InitializeCamera();
			InitializeShaders();
			UpdateViewMatrix();

			graphics.GraphicsDevice.Clear(Color.DarkGray);
			log.Log("Drawer Initialized...");
		}
		private void InitializeCamera()
		{
			camPosition = Vector3.Zero;
			camTarget = Vector3.Zero;
			// matricies
			projectionMatrix = Matrix.CreatePerspectiveFieldOfView(fov, GraphicsDevice.DisplayMode.AspectRatio, 1f, 10000f);
			worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);
		}
		private void InitializeShaders()
		{
			basicEffect = new BasicEffect(GraphicsDevice);
			basicEffect.Alpha = 1.0f;
			basicEffect.VertexColorEnabled = true;
			basicEffect.LightingEnabled = false;
		}
		#endregion
		protected override void LoadContent()
		{
			base.LoadContent();
			spriteBatch = new SpriteBatch(GraphicsDevice);
			sceneManager = new SceneManager(this, log);
			im = new InputManager(this, Exit, OnKey);
			log.Log("Drawer content Loaded...");
		}
		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			im.CheckInput();
			UpdateViewMatrix();
		}
		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
			// draw effect
			basicEffect.Projection = projectionMatrix;
			basicEffect.View = viewMatrix;
			basicEffect.World = worldMatrix;

            // optimization
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            // draw scene and color
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
			}
			sceneManager.activeScene.Draw(this);
		}
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);
			log.Log("");
			exitCallBack();
		}
        protected void OnKey(Keys key)
        {
			log.Log($"Key: {Enum.GetName(key)}");
        }
		#region Interface Accessibles
		void IDrawer.Log(string message, IDrawer.Level level)
		{
			log.Log(message, (Logger.Level)((int) level));
		}
		public void CameraLookAt(float x, float y, float z)
		{
			camTarget = new Vector3(x, y, z);
		}
		public void CameraLookAt(Vector3 pos)
		{
			camTarget = pos;
		}
		public Vector3 CameraPos()
		{
			return camPosition;
		}
		public void MoveCamera(Vector3 pos)
		{
			camPosition = pos;
		}
		public void MoveCamera(float x, float y, float z)
		{
			camPosition = new Vector3(x, y, z);
		}
		public GraphicsDevice GetGraphicsDevice()
		{
			return GraphicsDevice;
		}
		public void UpdateViewMatrix()
		{
			viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
		}
		#endregion
	}
}
