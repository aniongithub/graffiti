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

using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Graffiti.Core.Animation;

namespace Graffiti.Core.Math
{
    public sealed class TransformGroup: Transform, IAnimatable<Matrix, MatrixInterpolator>, IEnumerable<Transform>
    {
        private readonly List<Transform> _children = new List<Transform>(); 
        
        protected override Matrix GetMyValue()
        {
            return Matrix.Identity;
        }

        protected internal override Matrix GetValue()
        {
            var current = GetMyValue();
            foreach (var child in _children)
                current *= child.GetValue();

            return current;
        }

        public void Add(Transform child)
        {
            _children.Add(child);
        }

        public Matrix Current
        {
            get { return GetValue(); }
        }

        public IKeyframes<Matrix> Keyframes { get; set; }

        public Mode Mode { get; set; }

        public void Update(float timeInMilliSeconds)
        {
            foreach (var child in _children)
            {
                var updateable = child as IUpdateable;
                if (updateable != null)
                    updateable.Update(timeInMilliSeconds);
            }
        }

        public IEnumerator<Transform> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}