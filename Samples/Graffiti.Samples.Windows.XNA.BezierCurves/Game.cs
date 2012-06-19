using Graffiti.Core.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Graffiti.Samples.Windows.XNA.BezierCurves
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private ICurve _curve;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _curve = Content.ParsePathGeometry(@"F1 M 68.4668,321.002C 68.4668,321.002 200.48,53.9754 344.494,95.9796C 488.509,137.984 521.512,221.992 506.511,327.003C 491.509,432.013 524.513,483.018 614.521,453.015C 704.53,423.012 785.539,182.988 710.531,122.982");
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private float _time;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
