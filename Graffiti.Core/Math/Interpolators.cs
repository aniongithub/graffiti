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
using Microsoft.Xna.Framework;

namespace Graffiti.Core.Math
{
    public sealed class Vector3Interpolator : IInterpolator<Vector3>
    {
        #region IInterpolator<Vector3> Members

        public Vector3 Lerp(Vector3 value1, Vector3 value2, float lambda)
        {
            return (1 - lambda) * value1 + lambda * value2;
        }

        public Vector3 CubicBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float lambda)
        {
            // TODO: Continue here
            throw new NotImplementedException();
        }

        #endregion
    }

    public sealed class QuaternionInterpolator: IInterpolator<Quaternion>
    {
        #region IInterpolator<Quaternion> Members        

        public Quaternion Lerp(Quaternion value1, Quaternion value2, float lambda)
        {
            return Quaternion.Lerp(value1, value2, lambda);
        }

        public Quaternion CubicBezier(Quaternion a, Quaternion b, Quaternion c, Quaternion d, float lambda)
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    public sealed class MatrixInterpolator : IInterpolator<Matrix>
    {
        #region IInterpolator<Matrix> Members

        public Matrix Lerp(Matrix value1, Matrix value2, float lambda)
        {
            throw new NotSupportedException();
        }

        public Matrix CubicBezier(Matrix a, Matrix b, Matrix c, Matrix d, float lambda)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}