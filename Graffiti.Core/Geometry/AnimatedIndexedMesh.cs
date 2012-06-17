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
using Graffiti.Core.Brushes;
using Graffiti.Core.Math;
using Graffiti.Core.Rendering;
using Graffiti.Math;
using Graffiti.Core.Animation;

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Graffiti.Core.Geometry
{
    public class AnimatedIndexedMesh<TInterpolator> : IAnimatedIndexedMesh<TInterpolator>
        where TInterpolator : IInterpolator<IVertex>, new()
    {
        private readonly List<IAnimatable<IVertex, TInterpolator>> _animatedVertices = new List<IAnimatable<IVertex, TInterpolator>>();

        #region IAnimatedIndexedMesh<TInterpolator> Members

        public IList<IAnimatable<IVertex, TInterpolator>> AnimatedVertices
        {
            get
            {
                return _animatedVertices;
            }
        }

        #endregion

        #region IIndexedMesh Members

        public short[] Indices { get; protected set; }

        private IVertex[] _vertices;
        public IVertex[] Vertices
        {
            get 
            {
                if ((_vertices == null) || (_vertices.Length != _animatedVertices.Count))
                    Array.Resize(ref _vertices, _animatedVertices.Count);

                for (int i = 0; i < _animatedVertices.Count; i++)
                    _vertices[i] = _animatedVertices[i].Current;

                return _vertices;
            }
        }

        #endregion

        #region IRenderable Members

        public void Render(IRenderer renderer, Matrix parentTransform)
        {
            var bucket = renderer[Brush];
            bucket.Add(Transform.Current * parentTransform, Vertices, Indices);
        }

        public IBrush Brush { get; set; }

        #endregion

        #region IPoseable Members

        public IAnimatable<Matrix> Transform { get; set; }

        #endregion

        #region IUpdateable Members

        public void Update(float timeInMilliSeconds)
        {
            foreach (var animVertex in _animatedVertices)
                animVertex.Update(timeInMilliSeconds);
        }

        #endregion
    }
}