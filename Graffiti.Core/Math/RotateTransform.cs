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
using Graffiti.Core.Animation;
using Microsoft.Xna.Framework;

namespace Graffiti.Core.Math
{
    public sealed class RotateTransform : Transform, IAnimatable<Quaternion, QuaternionInterpolator>, IAnimatable<Matrix>
    {
        private static readonly QuaternionInterpolator _interpolator = new QuaternionInterpolator();
        
        protected override Matrix GetMyValue()
        {
            return Matrix.CreateFromQuaternion(_current);
        }

        protected internal override Matrix GetValue()
        {
            return Matrix.CreateFromQuaternion(_current);
        }

        private Quaternion _current;
        public Quaternion Current
        {
            get { return _current; }
        }

        public IKeyframes<Quaternion> Keyframes { get; set; }

        public Mode Mode { get; set; }

        public void Update(float timeInMilliSeconds)
        {
            _current = Keyframes.GetValueAt(_interpolator, timeInMilliSeconds, Mode);
        }

        Matrix IAnimatable<Matrix>.Current
        {
            get { return Matrix.CreateFromQuaternion(_current); }
        }

        IKeyframes<Matrix> IAnimatable<Matrix>.Keyframes
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

        Mode IAnimatable<Matrix>.Mode
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