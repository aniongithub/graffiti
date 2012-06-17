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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graffiti.Core.Brushes
{
    public sealed class Layer: ILayer
    {
        public Texture2D Texture { get; set; }
        public int TexCoordChannel { get; set; }
        public IAnimatable<Matrix> Transform { get; set; }
        public Color Color { get; set; }
        public TextureAddressMode AddressU { get; set; }
        public TextureAddressMode AddressV { get; set; }
        public BlendState BlendState { get; set; }

        public Layer()
        {
            Color = Color.White;
            Transform = (ConstantMatrix)Matrix.Identity;
            AddressU = TextureAddressMode.Wrap;
            AddressV = TextureAddressMode.Wrap;
            BlendState = new BlendState();
        }

        public void Update(float timeInMilliSeconds)
        {
            Transform.Update(timeInMilliSeconds);
        }
    }
}