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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graffiti.Core.Brushes
{
    internal sealed class DualLayer : IDualLayer
    {
        #region IDualLayer Members

        public ILayer Layer1 { get; set; }

        public ILayer Layer2 { get; set; }

        #endregion

        #region ILayer Members

        public Texture2D Texture
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public int TexCoordChannel
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public Color Color
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public TextureAddressMode AddressU
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public TextureAddressMode AddressV
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public BlendState BlendState
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public ILayerTransforms LayerTransforms
        {
            get { throw new NotSupportedException(); }
        }

        public Matrix CurrentLayerTransform
        {
            get { throw new NotSupportedException(); }
        }

        #endregion

        #region IUpdateable Members

        public void Update(float elapsedMilliseconds)
        {
            if (Layer1 != null)
                Layer1.Update(elapsedMilliseconds);
            if (Layer2 != null)
                Layer2.Update(elapsedMilliseconds);
        }

        #endregion
    }
}