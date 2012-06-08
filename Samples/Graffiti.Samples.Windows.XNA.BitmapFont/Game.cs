using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Graffiti.Core;
using Graffiti.Core.Brushes;
using Graffiti.Core.Rendering;
using Graffiti.Core.Text;
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
                GraphicsDevice, Features.MultiPass | Features.PreTransformed | Features.SingleChannelTexCoords,
                projection: Matrix.CreateOrthographicOffCenter(-halfWidth, halfWidth, halfHeight, -halfHeight, 0, 1));
        }

        private IBitmapFont _segoeWP;
        private IRenderable<Vertex<Texcoords_SingleChannel>, Texcoords_SingleChannel> _text; 

        protected override void LoadContent()
        {
            _segoeWP = Content.LoadBitmapFont("Content/Segoe_WP_Light_64x64.fnt");
            _text = _segoeWP.Build<Vertex<Texcoords_SingleChannel>, Texcoords_SingleChannel>("A quick brown fox jumped over the lazy dog",
                new Brush
                    {
                        new Layer
                            {
                                Texture = Content.Load<Texture2D>("Content/Solid"),
                                Color = Color.Red,
                                BlendState = BlendState.Additive
                            }
                    });
            _text.Transform = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-400, -240, 0f);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            _text.Render(_renderer, Matrix.Identity);
            
            _renderer.Flush();

            base.Draw(gameTime);
        }
    }
}
