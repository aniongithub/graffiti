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
using Graffiti.Math;

namespace Graffiti.Core.Math
{
    public class TranslateTransform<T> : Transform
        where T : IInterpolator<Vector3>, new()
    {
        protected static readonly T _interpolator = new T();
        
        private Matrix _current;
        public override Matrix Current
        {
            get { return _current; }
        }

        new public IKeyframes<Vector3> Keyframes { get; set; }

        public override Mode Mode { get; set; }

        public override void Update(float timeInMilliSeconds)
        {
            _current = Matrix.CreateTranslation(Keyframes.GetValueAt(_interpolator, timeInMilliSeconds, Mode));
        }

        public static IAnimatable<Matrix> Procedural(Func<float, Vector3> function)
        {
            return new Animatable<Matrix>(t => Matrix.CreateTranslation(function(t)));
        }
    }
    
    public sealed class TranslateTransform: TranslateTransform<Vector3Interpolator>
    {
    }

    public sealed class DiscreteTranslateTransform : TranslateTransform<DiscreteInterpolator<Vector3>>
    { 
    }
}