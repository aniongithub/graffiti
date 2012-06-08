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

using Graffiti.Core.Brushes;
using Graffiti.Core.Rendering;
using Microsoft.Xna.Framework;

namespace Graffiti.Core
{
    public interface IBehavior
    {
        // Nothing
    }

    public interface IPoseable : IBehavior
    {
        Matrix Transform { get; set; }
    }

    public interface IRenderable<TVertex, TTexcoords> : IPoseable
        where TTexcoords : struct, ITexcoords
        where TVertex : struct, IVertex<TTexcoords>
    {
        void Render(IRenderer renderer, Matrix parentTransform);
        IBrush Brush { get; set; }
    }

    internal static class IRenderableExtensions
    {
        public static void Render<TVertex, TTexcoords>(this IRenderable<TVertex, TTexcoords> renderable, IRenderer<TVertex, TTexcoords> renderer)
            where TTexcoords : struct, ITexcoords
            where TVertex : struct, IVertex<TTexcoords>
        {
            renderable.Render(renderer, Matrix.Identity);
        }
    }

    public interface IUpdateable : IBehavior
    {
        void Update(float elapsedMilliseconds);
    }
}
