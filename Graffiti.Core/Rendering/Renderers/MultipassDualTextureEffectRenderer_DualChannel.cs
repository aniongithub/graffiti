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

using System.Collections.Generic;
using Graffiti.Core.Brushes;
using Graffiti.Core.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graffiti.Core.Rendering.Renderers
{
    internal sealed class MultipassDualTextureEffectRenderer_DualChannel : IRenderer<Vertex<Texcoords_DualChannel>, Texcoords_DualChannel>
    {
        private readonly GraphicsDevice _device;
        private readonly BasicEffect _effect;
        private readonly DualTextureEffect _dualTextureEffect;
        private readonly Dictionary<IBrush, IRenderBucket<Vertex<Texcoords_SingleChannel>, Texcoords_SingleChannel>> _renderBuckets =
            new Dictionary<IBrush, IRenderBucket<Vertex<Texcoords_SingleChannel>, Texcoords_SingleChannel>>();
        private readonly ArrayList<VertexPositionColorTexture> _bucketVertices = new ArrayList<VertexPositionColorTexture>();

        public IRenderBucket<Vertex<Texcoords_DualChannel>, Texcoords_DualChannel> this[Brushes.IBrush brush]
        {
            get { throw new System.NotImplementedException(); }
        }

        public void Flush()
        {
            throw new System.NotImplementedException();
        }

        public Matrix Projection
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public Matrix View
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public Matrix World
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
    }
}