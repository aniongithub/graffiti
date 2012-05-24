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

namespace Graffiti.Core.Rendering
{
    public interface ITexcoords
    {
        Vector2 this[int index] { get; set; }
        int Length { get; }
    }

    public static class Texcoords
    {
        public static TTexcoords Create<TTexcoords>(Vector2 coords)
            where TTexcoords : struct, ITexcoords
        {
            var result = new TTexcoords();
            for (int i = 0; i < result.Length; i++)
                result[i] = coords;

            return result;
        }
    }
    
    public interface IVertex<TTexcoords>
        where TTexcoords: struct, ITexcoords
    {
        Vector3 Position { get; set; }
        Color Color { get; set; }
        TTexcoords Texcoords { get; set; }
    }
}