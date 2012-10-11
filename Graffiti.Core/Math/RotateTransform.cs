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
    public sealed class RotateTransform : Transform
    {
        private static readonly QuaternionInterpolator _interpolator = new QuaternionInterpolator();
        
        private Matrix _current;
        public override Matrix Current
        {
            get { return _current; }
        }

        public override void Update(float timeInMilliSeconds)
        {
            _current = Matrix.CreateFromQuaternion(Keyframes.GetValueAt(_interpolator, timeInMilliSeconds, Mode));
        }

        new public IKeyframes<Quaternion> Keyframes { get; set; }

        public override Mode Mode { get; set; }

        public static IAnimatable<Matrix> Procedural(Func<float, Quaternion> function)
        {
            return new Animatable<Matrix>(t => Matrix.CreateFromQuaternion(function(t)));
        }
    }
}