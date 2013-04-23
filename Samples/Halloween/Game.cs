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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Graffiti.Core.Primitives;
using Graffiti.Core.Rendering;
using Graffiti.Core.Extensions;

namespace Graffiti.Samples.Halloween
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Group _scene;
        IRenderer _renderer;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            GraphicsDevice.Viewport = graphics.AdaptViewport(1280, 800);

            _renderer = Renderer.Create(GraphicsDevice, 
                Features.MultiPass | Features.PreTransformed | Features.SingleChannelTexCoords | Features.AlphaTest,
            projection: Matrix.CreateOrthographicOffCenter(0, 1280, 800, 0, 0, 100));

            Window.ClientSizeChanged += Window_ClientSizeChanged;

            base.Initialize();
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            GraphicsDevice.Viewport = graphics.AdaptViewport(1280, 800);
        }

        protected override void LoadContent()
        {
            _scene = Scene.Compose(Content);
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
            _scene.Update(_time);

            base.Update(gameTime);
        }

        private static readonly Color _backgroundColor = new Color(111, 01, 108);
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);

            _scene.Render(_renderer, Matrix.Identity);
            
            _renderer.Flush();

            base.Draw(gameTime);
        }
    }
}
