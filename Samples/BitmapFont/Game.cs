using Graffiti.Core;
using Graffiti.Core.Animation;
using Graffiti.Core.Brushes;
using Graffiti.Core.Math;
using Graffiti.Core.Rendering;
using Graffiti.Core.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Graffiti.Core.Animation.Constants;

namespace Graffiti.Samples.BitmapFont
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
                                Color = (ConstantColor)Color.White,
                                BlendState = BlendState.AlphaBlend,
                                Transform = new TranslateTransform
                                                {
                                                    Mode = Mode.Loop,
                                                    Keyframes = new Keyframes<Vector3>
                                                                    {
                                                                        { 0f, Vector3.Zero },
                                                                        { 1000f, Vector3.One }
                                                                    }
                                                }
                            }
                    });
            _text.Transform = new TransformGroup
                                  {
                                      new RotateTransform
                                          {
                                              Keyframes = new Keyframes<Quaternion>
                                                              {
                                                                  { 0f, Quaternion.CreateFromYawPitchRoll(0, 0, 0) },
                                                                  { 100f, Quaternion.CreateFromYawPitchRoll(0, 0, 0.1f) },
                                                                  { 200f, Quaternion.CreateFromYawPitchRoll(0, 0, 0) },
                                                                  { 300f, Quaternion.CreateFromYawPitchRoll(0, 0, -0.1f) },
                                                                  { 400f, Quaternion.CreateFromYawPitchRoll(0, 0, 0) },
                                                              },
                                             Mode = Mode.Loop
                                          },
                                      
                                      new ScaleTransform
                                          {
                                              Keyframes = new Keyframes<Vector3>
                                                              {
                                                                  { 0f, Vector3.One },
                                                                  { 250f, new Vector3(1.2f, 1.2f, 1f) },
                                                                  { 500f, Vector3.One },
                                                              },
                                              Mode = Mode.Loop
                                          }
                                  };
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
            _text.Transform.Update(_time);

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
