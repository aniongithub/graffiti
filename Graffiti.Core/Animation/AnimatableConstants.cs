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
using Graffiti.Core.Math;
using Graffiti.Math;
using Microsoft.Xna.Framework;

namespace Graffiti.Core.Animation
{
    public abstract class AnimatableConstant<T, TInterpolator> : IAnimatable<T, TInterpolator>
        where TInterpolator : IInterpolator<T>, new()
    {
        private readonly T _value;

        protected AnimatableConstant(T value)
        {
            _value = value;
        }

        public T Current
        {
            get { return _value; }
        }

        public IKeyframes<T> Keyframes
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

        public Mode Mode
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

        public void Update(float timeInMilliSeconds)
        {
            // Do nothing
        }
    }

    public sealed class ConstantMatrix : AnimatableConstant<Matrix, MatrixInterpolator>
    {
        public ConstantMatrix(Matrix value)
            : base(value)
        {}

        public static implicit operator ConstantMatrix(Matrix value)
        {
            return new ConstantMatrix(value);
        }
    }
    
    public sealed class ConstantVector3 : AnimatableConstant<Vector3, Vector3Interpolator>
    {
        public ConstantVector3(Vector3 value)
            : base(value)
        { }

        public static implicit operator ConstantVector3(Vector3 value)
        {
            return new ConstantVector3(value);
        }
    }
}