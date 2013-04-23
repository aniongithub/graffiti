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
using Graffiti.Core.Animation.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graffiti.Core.Brushes
{
    public sealed class Layer: ILayer
    {
        public Texture2D Texture { get; set; }
        public int TexCoordChannel { get; set; }
        public IAnimatable<Matrix> Transform { get; set; }
        public IAnimatable<Color> Color { get; set; }
        public TextureAddressMode AddressU { get; set; }
        public TextureAddressMode AddressV { get; set; }
        public BlendState BlendState { get; set; }

        public bool AlphaTestEnable { get; set; }
        public int ReferenceAlpha { get; set; }
        public CompareFunction AlphaFunction { get; set; }

        public Layer()
        {
            Color = (ConstantColor)Microsoft.Xna.Framework.Color.White;
            Transform = (ConstantMatrix)Matrix.Identity;
            AddressU = TextureAddressMode.Wrap;
            AddressV = TextureAddressMode.Wrap;
            BlendState = new BlendState();
        }

        public void Update(float timeInMilliSeconds)
        {
            Transform.Update(timeInMilliSeconds);
            Color.Update(timeInMilliSeconds);
        }

        private SamplerState _samplerState;
        internal SamplerState SamplerState 
        { 
            get 
            {
                if (_samplerState == null)
                    _samplerState = new SamplerState
                    {
                        AddressU = this.AddressU,
                        AddressV = this.AddressV
                    };

                return _samplerState;
            }
        } 
    }
}