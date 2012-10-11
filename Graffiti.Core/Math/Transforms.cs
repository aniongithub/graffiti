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
using Microsoft.Xna.Framework;

namespace Graffiti.Math
{
    public static class Transforms
    {
        public static Matrix Translation<T>(float amount) where T : Axes, new()
        {
            var translation = Vector3.Zero;
            switch (new T().Axis)
            {
                case 0:
                    translation.X = amount;
                    break;
                case 1:
                    translation.Y = amount;
                    break;
                case 2:
                    translation.Z = amount;
                    break;
            }

            return Matrix.CreateTranslation(translation);
        }

        public static Matrix RotationAround<T>(float degrees, float x, float y) where T : Axes, new()
        {
            switch (new T().Axis)
            {
                case 0:
                    return Matrix.CreateTranslation(-x, -y, 0) *
                           Matrix.CreateRotationX(MathHelper.ToRadians(degrees)) *
                           Matrix.CreateTranslation(x, y, 0);

                case 1:
                    return Matrix.CreateTranslation(-x, -y, 0) *
                           Matrix.CreateRotationY(MathHelper.ToRadians(degrees)) *
                           Matrix.CreateTranslation(x, y, 0);

                case 2:
                    return Matrix.CreateTranslation(-x, -y, 0) *
                           Matrix.CreateRotationZ(MathHelper.ToRadians(degrees)) *
                           Matrix.CreateTranslation(x, y, 0);

                default:
                    throw new ArgumentException(string.Format("Unknown axis {0}", typeof(T).Name));
            }
        }
        public static Matrix Rotation<T>(float degrees) where T : Axes, new()
        {
            switch (new T().Axis)
            {
                case 0:
                    return Matrix.CreateRotationX(MathHelper.ToRadians(degrees));

                case 1:
                    return Matrix.CreateRotationY(MathHelper.ToRadians(degrees));

                case 2:
                    return Matrix.CreateRotationZ(MathHelper.ToRadians(degrees));

                default:
                    throw new ArgumentException(string.Format("Unknown axis {0}", typeof(T).Name));
            }
        }

        public static Matrix Scale<T>(float amount) where T : Axes, new()
        {
            var scale = Vector3.One;
            switch (new T().Axis)
            {
                case 0:
                    scale.X = 1 / amount;
                    break;
                case 1:
                    scale.Y = 1 / amount;
                    break;
                case 2:
                    scale.Z = 1 / amount;
                    break;
            }

            return Matrix.CreateScale(scale);
        }
        public static Matrix ScaleAt<T>(float amount, float x, float y) where T : Axes, new()
        {
            var scale = Vector3.One;
            switch (new T().Axis)
            {
                case 0:
                    scale.X = 1 / amount;
                    break;
                case 1:
                    scale.Y = 1 / amount;
                    break;
                case 2:
                    scale.Z = 1 / amount;
                    break;
            }

            return Matrix.CreateTranslation(-x, -y, 0f) *
                   Matrix.CreateScale(scale) *
                   Matrix.CreateTranslation(x, y, 0f);
        }
    }
}