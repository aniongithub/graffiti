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

using Graffiti.Core.Animation;
using Microsoft.Xna.Framework;
using System;
namespace Graffiti.Core.Animation.Constants
{
    public sealed class ConstantColor : IAnimatable<Color>
    {
        public Color Current
        {
            get { return Color; }
        }

        public Color Color { get; set; }

        IKeyframes<Color> IAnimatable<Color>.Keyframes
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

        Mode IAnimatable<Color>.Mode
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

        void IUpdateable.Update(float timeInMilliSeconds)
        {
            // Nothing to do
        }

        public static implicit operator Color(ConstantColor constantColor)
        {
            return constantColor.Color;
        }

        public static implicit operator ConstantColor(Color color)
        {
            return new ConstantColor { Color = color };
        }
    }
}