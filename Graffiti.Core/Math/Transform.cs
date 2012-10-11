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

namespace Graffiti.Core.Math
{
    public abstract class Transform: IAnimatable<Matrix, MatrixInterpolator>
    {
        public abstract Matrix Current
        {
            get;
        }

        public abstract void Update(float timeInMilliSeconds);

        public abstract Mode Mode { get; set; }

        public IKeyframes<Matrix> Keyframes { get; set; }
    }
}