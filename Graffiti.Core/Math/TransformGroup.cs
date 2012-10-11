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
    public sealed class TransformGroup: Transform, IEnumerable<IAnimatable<Matrix>>
    {
        private readonly List<IAnimatable<Matrix>> _children = new List<IAnimatable<Matrix>>(); 
        
        public TransformGroup()
        { 
        }

        public TransformGroup(Mode mode)
        {
            Mode = mode;
        }

        public void Add(IAnimatable<Matrix> child)
        {
            _children.Add(child);
        }

        private Matrix _current = Matrix.Identity;
        public override Matrix Current
        {
            get { return _current; }
        }

        public override Mode Mode { get; set; }

        public override void Update(float timeInMilliSeconds)
        {
            _current = Matrix.Identity;
            foreach (var child in _children)
            {
                var updateable = child as IUpdateable;
                if (updateable != null)
                    updateable.Update(timeInMilliSeconds);
                
                _current *= child.Current;
            }
        }

        public IEnumerator<IAnimatable<Matrix>> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}