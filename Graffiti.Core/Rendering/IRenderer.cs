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
using Graffiti.Core.Brushes;
using Graffiti.Core.Rendering.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graffiti.Core.Rendering
{
    public interface IRenderer: IEffectMatrices
    {
        void Flush();
    }

    [Flags]
    public enum Features
    {
        MultiPass,
        MultiTexturing,

        PreTransformed,
        GpuTransformed,

        SingleChannelTexCoords,
        DualChannelTexCoords
    }

    public static class Renderer
    {
        public static IRenderer Create(GraphicsDevice device, Features rendererFeatures)
        {
            switch (rendererFeatures)
            {
                case Features.MultiPass | Features.PreTransformed | Features.SingleChannelTexCoords :
                    return new MultipassSimpleEffectRenderer_SingleChannel(device);

                default:
                    throw new NotSupportedException();
            }
        }

        public static IRenderer Create(GraphicsDevice device, Features rendererFeatures, Matrix? projection = null, Matrix? world = null, Matrix? view = null)
        {
            var result = Create(device, rendererFeatures);
            result.Projection = projection ?? Matrix.Identity;
            result.World = world ?? Matrix.Identity;
            result.View = view ?? Matrix.Identity;

            return result;
        }
    }

    internal interface IRenderer<TVertex, TTexcoords> : IRenderer
        where TTexcoords: struct, ITexcoords
        where TVertex: struct, IVertex<TTexcoords>
    {
        IRenderBucket<TVertex, TTexcoords> this[IBrush brush]
        {
            get;
        }
    }
}