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

using Microsoft.Xna.Framework;
using Graffiti.Core.Animation;
using System;

namespace Graffiti.Core.Math
{
    public sealed class ShearTransform : Transform
    {
        private static readonly Vector2Interpolator _interpolator = new Vector2Interpolator();

        private Matrix _current;
        public override Matrix Current
        {
            get { return _current; }
        }

        new public IKeyframes<Vector2> Keyframes { get; set; }

        public override Mode Mode { get; set; }

        public override void Update(float timeInMilliSeconds)
        {
            var shearXY = Keyframes.GetValueAt(_interpolator, timeInMilliSeconds, Mode);
            _current = Shear(shearXY.X, shearXY.Y);
        }

        private static Matrix Shear(float x, float y)
        { 
            return new Matrix(1f + x * y, x,  0f, 0f,
                              y         , 1f, 0f, 0f,
                              0f        , 0f, 1f, 0f,
                              0f        , 0f, 0f, 1f);
        }

        public static IAnimatable<Matrix> Procedural(Func<float, Vector2> function)
        {
            return new Animatable<Matrix>(t =>
            {
                var value = function(t);
                return Shear(value.X, value.Y);
            });
        }
    }
}