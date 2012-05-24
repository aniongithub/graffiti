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

using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace Graffiti.Core.Rendering
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Texcoords_SingleChannel : ITexcoords
    {
        private Vector2 _texcoords;

        public Vector2 this[int index]
        {
            get { return _texcoords; }
            set { _texcoords = value; }
        }

        public int Length
        {
            get { return 1; }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex<TTexcoords> : IVertex<TTexcoords>
        where TTexcoords: struct, ITexcoords
    {
        #region IVertex Members

        public Vector3 Position { get; set; }

        public Color Color { get; set; }

        public TTexcoords Texcoords { get; set; }

        #endregion
    }
}