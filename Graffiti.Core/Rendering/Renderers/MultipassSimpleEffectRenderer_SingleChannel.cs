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
    internal sealed class MultipassSimpleEffectRenderer_SingleChannel: IRenderer
    {
        private readonly GraphicsDevice _device;
        private readonly BasicEffect _effect;
        private readonly Dictionary<IBrush, IRenderBucket> _renderBuckets =
            new Dictionary<IBrush, IRenderBucket>();
        private readonly ArrayList<VertexPositionColorTexture> _bucketVertices = new ArrayList<VertexPositionColorTexture>();

        public MultipassSimpleEffectRenderer_SingleChannel(GraphicsDevice device)
        {
            _device = device;
            _effect = new BasicEffect(_device)
            {
                LightingEnabled = false,
                VertexColorEnabled = true,
                TextureEnabled = true
            };
        }

        public void Flush()
        {
            _effect.Projection = Projection;
            _effect.World = World;
            _effect.View = View;

            foreach (var kvp in _renderBuckets)
            {
                var bucket = kvp.Value;
                var brush = kvp.Key;

                foreach (var layer in brush)
                {
                    _bucketVertices.Clear();

                    var transform = layer.Transform.Current;

                    _effect.Texture = layer.Texture;

                    _device.SamplerStates[0] = new SamplerState
                                                   {
                                                       AddressU = layer.AddressU,
                                                       AddressV = layer.AddressV
                                                   };
                    for (int i = 0; i < kvp.Value.Vertices.Count; i++)
                    {
                        var incomingVertex = bucket.Vertices[i];
                        var vertex = new VertexPositionColorTexture(incomingVertex.Position,
                            new Color(layer.Color.R * incomingVertex.Color.R,
                                layer.Color.G * incomingVertex.Color.G,
                                layer.Color.B * incomingVertex.Color.B,
                                layer.Color.A * incomingVertex.Color.A),
                                Vector2.Transform(incomingVertex.Texcoords[layer.TexCoordChannel], transform));
                        _bucketVertices.Add(vertex);
                    }

                    foreach (var pass in _effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        _device.BlendState = layer.BlendState;

                        _device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                                                          _bucketVertices.GetArray(), 0,
                                                          kvp.Value.Vertices.Count,
                                                          (kvp.Value.Indices as ArrayList<short>).GetArray(), 0,
                                                          kvp.Value.Indices.Count / 3);
                    }
                }
                
                bucket.Clear();
            }

            _renderBuckets.Clear();
        }
        
        IRenderBucket IRenderer.this[IBrush brush]
        {
            get
            {
                IRenderBucket bucket;
                var exists = _renderBuckets.TryGetValue(brush, out bucket);
                if (!exists)
                {
                    bucket = new PreTransformedRenderBucket();
                    _renderBuckets.Add(brush, bucket);
                }

                return bucket;
            }
        }

        public Matrix Projection
        {
            get { return _effect.Projection; }
            set { _effect.Projection = value; }
        }
        public Matrix View
        {
            get { return _effect.View; }
            set { _effect.View = value; }
        }
        public Matrix World
        {
            get { return _effect.World; }
            set { _effect.World = value; }
        }
    }
}