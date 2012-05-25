#region Using Statements

using Graffiti.Core.Brushes;
using Graffiti.Core.Primitives;
using Graffiti.Core.Rendering;
using Graffiti.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Graffiti.Samples.Windows.MonoGame.Quake3Shader
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = ".";
        }

        private IRenderer _renderer;
        private Quad<Vertex<Texcoords_SingleChannel>, Texcoords_SingleChannel> _quad;

        protected override void Initialize()
        {
            base.Initialize();

            var halfWidth = GraphicsDevice.Viewport.Width / 2f;
            var halfHeight = GraphicsDevice.Viewport.Height / 2f;

            _renderer = Renderer.Create(
                GraphicsDevice, Features.MultiPass | Features.PreTransformed | Features.SingleChannelTexCoords,
                projection: Matrix.CreateOrthographicOffCenter(-halfWidth, halfWidth, halfHeight, -halfHeight, 0, 1));

            _quad = new Quad<Vertex<Texcoords_SingleChannel>, Texcoords_SingleChannel>
            {
                Transform = Matrix.CreateScale(256f),
                Brush = new Brush
                {
                    new Layer
                        {
                            BlendState = BlendState.Opaque,
                            Texture = Content.Load<Texture2D>("Content/basewall01bit")
                        },
                    //new Layer
                    //    {
                    //        BlendState = BlendState.Additive,
                    //        Texture = Content.Load<Texture2D>("Content/basewall01bitfx"),
                    //        LayerTransforms = new LayerTransforms
                    //        {
                    //            t => Transforms.Translation<Axes.X>(Functions.Linear(t, 0.005f)),
                    //            t => Transforms.Translation<Axes.Y>(Functions.Linear(t, 0.001f))
                    //        }
                    //    },
                    //new Layer
                    //    {
                    //        BlendState = BlendState.Additive,
                    //        Texture = Content.Load<Texture2D>("Content/envmap2"),
                    //        LayerTransforms = new LayerTransforms
                    //        {
                    //            t => Transforms.ScaleAt<Axes.X>(0.5f, 0.5f, 0.5f),
                    //            t => Transforms.ScaleAt<Axes.Y>(0.5f, 0.5f, 0.5f)
                    //        }
                    //    },
                    //new Layer
                    //    {
                    //        BlendState = BlendState.AlphaBlend,
                    //        Texture = Content.Load<Texture2D>("Content/basewall01bit")
                    //    }
                }
            };
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _quad.Brush.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _quad.Render(_renderer, Matrix.Identity);
            _renderer.Flush();

            base.Draw(gameTime);
        }
    }
}
