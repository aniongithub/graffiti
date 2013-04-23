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
        private readonly BasicEffect _basicEffect;
        private readonly AlphaTestEffect _alphaTestEffect;
        private readonly Dictionary<IBrush, IRenderBucket> _renderBuckets =
            new Dictionary<IBrush, IRenderBucket>();
        private readonly ArrayList<VertexPositionColorTexture> _bucketVertices = new ArrayList<VertexPositionColorTexture>();

        public MultipassSimpleEffectRenderer_SingleChannel(GraphicsDevice device)
        {
            _device = device;
            _basicEffect = new BasicEffect(_device)
            {
                LightingEnabled = false,
                VertexColorEnabled = true,
                TextureEnabled = true
            };
            _alphaTestEffect = new AlphaTestEffect(_device)
            {
                VertexColorEnabled = true
            };
        }

        public void Flush()
        {
            foreach (var kvp in _renderBuckets)
            {
                var bucket = kvp.Value;
                var brush = kvp.Key;

                foreach (var layer in brush)
                {
                    #region AlphaTest renderer
                    if (layer.AlphaTestEnable == true)
                    {
                        _bucketVertices.Clear();

                        var transform = layer.Transform.Current;

                        _alphaTestEffect.Texture = layer.Texture;
                        _alphaTestEffect.ReferenceAlpha = layer.ReferenceAlpha;
                        _alphaTestEffect.AlphaFunction = layer.AlphaFunction;

                        _alphaTestEffect.Projection = Projection;
                        _alphaTestEffect.World = World;
                        _alphaTestEffect.View = View;

                        _device.SamplerStates[0] = (layer as Layer).SamplerState;
                        for (int i = 0; i < kvp.Value.Vertices.Count; i++)
                        {
                            var incomingVertex = bucket.Vertices[i];
                            var color = layer.Color.Current;
                            var vertex = new VertexPositionColorTexture(incomingVertex.Position,
                                new Color(color.R, color.G, color.B, color.A),
                                Vector2.Transform(incomingVertex.Texcoords[layer.TexCoordChannel], transform));
                            _bucketVertices.Add(vertex);
                        }

                        foreach (var pass in _alphaTestEffect.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            _device.BlendState = layer.BlendState;

                            _device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                                                              _bucketVertices.GetArray(), 0,
                                                              kvp.Value.Vertices.Count,
                                                              (kvp.Value.Indices as ArrayList<short>).GetArray(), 0,
                                                              kvp.Value.Indices.Count / 3);
                        }

                        continue;
                    }
                    #endregion

                    #region Basic effect
                    {
                        _bucketVertices.Clear();

                        var transform = layer.Transform.Current;

                        _basicEffect.Texture = layer.Texture;
                        _basicEffect.Projection = Projection;
                        _basicEffect.World = World;
                        _basicEffect.View = View;

                        _device.SamplerStates[0] = (layer as Layer).SamplerState;
                        for (int i = 0; i < kvp.Value.Vertices.Count; i++)
                        {
                            var incomingVertex = bucket.Vertices[i];
                            var color = layer.Color.Current;
                            var vertex = new VertexPositionColorTexture(incomingVertex.Position,
                                new Color(color.R, color.G, color.B, color.A),
                                Vector2.Transform(incomingVertex.Texcoords[layer.TexCoordChannel], transform));
                            _bucketVertices.Add(vertex);
                        }

                        foreach (var pass in _basicEffect.CurrentTechnique.Passes)
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
                    #endregion
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

        public Matrix Projection { get; set; }
        public Matrix View { get; set; }
        public Matrix World { get; set; }
    }
}