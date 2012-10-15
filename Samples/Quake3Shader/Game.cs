#region License and Copyright Notice
// Copyright (c) 2010 Ananth Balasubramaniam
// All rights reserved.
// 
// The contents of this file are made available under the terms of the
// Eclipse Public License v1.0 (the "License") which accompanies this
// distribution, and is available at the following URL:
// http://www.opensource.org/licenses/eclipse-1.0.php
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either expressed or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// By using this software in any fashion, you are agreeing to be bound by the
// terms of the License.
#endregion

using Graffiti.Core.Animation;
using Graffiti.Core.Brushes;
using Graffiti.Core.Math;
using Graffiti.Core.Primitives;
using Graffiti.Core.Rendering;
using Graffiti.Math;
using Graffiti.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Graffiti.Samples.Quake3Shader
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphics;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        private IRenderer _renderer;
        private Quad _quad;

        protected override void Initialize()
        {
            GraphicsDevice.Viewport = graphics.AdaptViewport(256, 256);

            base.Initialize();

            var halfWidth = GraphicsDevice.Viewport.Width / 2f;
            var halfHeight = GraphicsDevice.Viewport.Height / 2f;
            
            _renderer = Renderer.Create(
                GraphicsDevice, Features.MultiPass | Features.PreTransformed | Features.SingleChannelTexCoords,
                projection: Matrix.CreateOrthographicOffCenter(-halfWidth, halfWidth, halfHeight, -halfHeight, 0, 1));

            _quad = new Quad
            {
                Transform = (ConstantMatrix)Matrix.CreateScale(256f),
                Brush = new Brush
                {
                    new Layer
                        {
                            BlendState = BlendState.Opaque,
                            Texture = Content.Load<Texture2D>("Content/basewall01bit")
                        },
                    new Layer
                        {
                            BlendState = BlendState.Additive,
                            Texture = Content.Load<Texture2D>("Content/basewall01bitfx"),
                            Transform = new TransformGroup
                                            {
                                                new TranslateTransform
                                                    {
                                                        Keyframes = new Keyframes<Vector3>
                                                                        {
                                                                            { 0f, Vector3.Zero },
                                                                            { 1000f, new Vector3(6f, 1f, 0f) }
                                                                        },
                                                        Mode = Mode.Loop
                                                    }
                                            }
                        },
                    new Layer
                        {
                            BlendState = BlendState.NonPremultiplied,
                            Texture = Content.Load<Texture2D>("Content/basewall01bit")
                        }
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

        private float _time;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            _time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            _quad.Brush.Update(_time);

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
