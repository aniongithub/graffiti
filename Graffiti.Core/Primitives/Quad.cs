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

using Graffiti.Core.Geometry;
using Graffiti.Core.Rendering;
using Microsoft.Xna.Framework;

namespace Graffiti.Core.Primitives
{
    public sealed class Quad : IndexedMesh
    {
        public Quad()
        {
            Vertices = new IVertex[]
            {
                    new Vertex
                        {
                            Position = new Vector3(-1f, -1f, 0f),
                            Color = Color.White,
                            Texcoords = new Texcoords(new Vector2(0f, 0f))
                        },
                    new Vertex
                        {
                            Position = new Vector3(1f, -1f, 0f),
                            Color = Color.White,
                            Texcoords = new Texcoords(new Vector2(1f, 0f))
                        },
                    new Vertex
                        {
                            Position = new Vector3(1f, 1f, 0f),
                            Color = Color.White,
                            Texcoords = new Texcoords(new Vector2(1f, 1f))
                        },
                    new Vertex
                        {
                            Position = new Vector3(-1f, 1f, 0f),
                            Color = Color.White,
                            Texcoords = new Texcoords(new Vector2(0f, 1f))
                        },
            };
            Indices = new short[] { 0, 1, 2, 0, 2, 3 };
        }

        public Quad(float left, float top, float width, float height, float depth = 0f)
        {
            var centerX = left + (width / 2);
            var centerY = top + (height / 2);

            Vertices = new IVertex[]
            {
                    new Vertex
                        {
                            Position = new Vector3(left, top, depth),
                            Color = Color.White,
                            Texcoords = new Texcoords(new Vector2(0f, 0f))
                        },
                    new Vertex
                        {
                            Position = new Vector3(left + width, top, depth),
                            Color = Color.White,
                            Texcoords = new Texcoords(new Vector2(1f, 0f))
                        },
                    new Vertex
                        {
                            Position = new Vector3(left + width, top + height, depth),
                            Color = Color.White,
                            Texcoords = new Texcoords(new Vector2(1f, 1f))
                        },
                    new Vertex
                        {
                            Position = new Vector3(left, top + height, depth),
                            Color = Color.White,
                            Texcoords = new Texcoords(new Vector2(0f, 1f))
                        },
            };
            Indices = new short[] { 0, 1, 2, 0, 2, 3 };
        }
    }
}