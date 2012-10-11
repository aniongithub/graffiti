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
using Microsoft.Xna.Framework.Graphics;
namespace Graffiti.Core.Extensions
{
    public static class GraphicsDeviceManagerExtensions
    {
        private struct RectangleF
        {
            public float Width;
            public float Height;
        }

        private static RectangleF ScaleRect(RectangleF dest, RectangleF src, bool keepWidth, bool keepHeight)
        {
            RectangleF destRect = new RectangleF();

            float sourceAspect = src.Width / src.Height;
            float destAspect = dest.Width / dest.Height;

            if (sourceAspect > destAspect)
            {
                // wider than high keep the width and scale the height
                destRect.Width = dest.Width;
                destRect.Height = dest.Width / sourceAspect;

                if (keepHeight)
                {
                    float resizePerc = dest.Height / destRect.Height;
                    destRect.Width = dest.Width * resizePerc;
                    destRect.Height = dest.Height;
                }
            }
            else
            {
                // higher than wide – keep the height and scale the width
                destRect.Height = dest.Height;
                destRect.Width = dest.Height * sourceAspect;

                if (keepWidth)
                {
                    float resizePerc = dest.Width / destRect.Width;
                    destRect.Width = dest.Width;
                    destRect.Height = dest.Height * resizePerc;
                }

            }

            return destRect;
        }

        public static Viewport AdaptViewport(this GraphicsDeviceManager graphics, int refWidth, int refHeight)
        {
            int deviceWidth = graphics.PreferredBackBufferWidth;
            int deviceHeight = graphics.PreferredBackBufferHeight;
            var scaled = ScaleRect(new RectangleF { Width = deviceWidth, Height = deviceHeight },
                new RectangleF { Width = refWidth, Height = refHeight },
                false, false);
            var left = (deviceWidth - scaled.Width) / 2;
            var top = (deviceHeight - scaled.Height) / 2;

            return new Viewport((int)left, (int)top, (int)scaled.Width, (int)scaled.Height);
        }
    }
}