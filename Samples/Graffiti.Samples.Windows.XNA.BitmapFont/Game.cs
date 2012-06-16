using Graffiti.Core;
using Graffiti.Core.Brushes;
using Graffiti.Core.Rendering;
using Graffiti.Core.Text;
using Graffiti.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Graffiti.Samples.Windows.XNA.BitmapFont
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        private IRenderer _renderer;
        protected override void Initialize()
        {
            base.Initialize();

            var halfWidth = GraphicsDevice.Viewport.Width / 2f;
            var halfHeight = GraphicsDevice.Viewport.Height / 2f;

            _renderer = Renderer.Create(
                GraphicsDevice, Features.MultiPass | Features.PreTransformed | Features.DualChannelTexCoords,
                projection: Matrix.CreateOrthographicOffCenter(-halfWidth, halfWidth, halfHeight, -halfHeight, 0, 1));
        }

        private IBitmapFont _font;
        private IRenderable _text; 

        protected override void LoadContent()
        {
            _font = Content.LoadBitmapFont("Content/Broadway_64x64.fnt");
            _text = _font.Build("Hello, Graffiti!",
                new Brush
                    {
                        new Layer
                            {
                                Texture = Content.Load<Texture2D>("Content/Gradient"),
                                Color = Color.White,
                                BlendState = BlendState.AlphaBlend,
                                LayerTransforms = new LayerTransforms
                                {
                                    t => Transforms.Translation<Axes.X>(Functions.Linear(t, 0.001f))
                                }
                            }
                    });
            _text.Transform = Matrix.CreateTranslation(-400, -240, 0f);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        private float _time;

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _time += gameTime.ElapsedGameTime.Milliseconds;
            _text.Brush.Update(_time);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _text.Render(_renderer, Matrix.Identity);
            
            _renderer.Flush();

            base.Draw(gameTime);
        }
    }
}
