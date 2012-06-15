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
using Graffiti.Math;

namespace Graffiti.Core.Animation
{
    public sealed class Animatable<T, TInterpolator> : IAnimatable<T, TInterpolator>
        where T : struct, IComparable<T>
        where TInterpolator : IInterpolator<T>, new()
    {
        private static readonly TInterpolator _interpolator = new TInterpolator();

        private T _current;

        public T Current
        {
            get
            {
                return _current;
            }
        }

        public void Update(float timeInMilliSeconds)
        {
            _current = Keyframes.GetValueAt(_interpolator, timeInMilliSeconds, Mode);
        }

        public IKeyframes<T> Keyframes
        {
            get;
            set;
        }

        public Mode Mode
        {
            get;
            set;
        }

        public static implicit operator T(Animatable<T, TInterpolator> animatable)
        {
            return animatable._current;
        }
    }
}