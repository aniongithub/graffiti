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

using Graffiti.Core.Brushes;
using System;
using Graffiti.Core.Animation;
using Graffiti.Core.Rendering;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace Graffiti.Core.Primitives
{ 
    public sealed class Group: IRenderable, IEnumerable<IRenderable>, IUpdateable
    {
        private readonly List<IRenderable> _children = new List<IRenderable>();

        public Group()
        {
            Transform = (ConstantMatrix)Matrix.Identity;
        }

        public void Render(IRenderer renderer, Matrix parentTransform)
        {
            var current = Transform.Current;
            for (int i = 0; i < _children.Count; i++)
                _children[i].Render(renderer, current);
        }

        IBrush IRenderable.Brush
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public IAnimatable<Microsoft.Xna.Framework.Matrix> Transform
        {
            get;
            set;
        }

        public void Add(IRenderable renderable)
        {
            _children.Add(renderable);
        }

        public IEnumerator<IRenderable> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Update(float timeInMilliSeconds)
        {
            for (int i = 0; i < _children.Count; i++)
            {
                var child = _children[i];
                var brush = child.Brush;
                if (brush != null)
                    brush.Update(timeInMilliSeconds);
            }
        }
    }
}