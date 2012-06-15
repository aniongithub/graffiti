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
using Graffiti.Core.Collections;
using Microsoft.Xna.Framework;

namespace Graffiti.Core.Rendering
{
    internal sealed class PreTransformedRenderBucket : IRenderBucket
    {
        private readonly IList<IVertex> _vertices = new List<IVertex>();
        private readonly IList<short> _indices = new ArrayList<short>();

        #region IRenderBucket<TVertex, TTexcoords> Members

        void IRenderBucket.Add(Matrix transform, IEnumerable<IVertex> vertices, IEnumerable<short> indices)
        {
            var startVertex = _vertices.Count;
            foreach (var vertex in vertices)
                _vertices.Add(transform == Matrix.Identity ? vertex : new Vertex
                                                    {
                                                        Position = Vector3.Transform(vertex.Position, transform),
                                                        Color = vertex.Color,
                                                        Texcoords = vertex.Texcoords
                                                    });
            foreach (var index in indices)
                _indices.Add((short)(index + startVertex));
        }
        void IRenderBucket.Clear()
        {
            _vertices.Clear();
            _indices.Clear();
        }

        public IList<IVertex> Vertices
        {
            get { return _vertices; }
        }
        public IList<short> Indices
        {
            get { return _indices; }
        }

        #endregion
    }
}