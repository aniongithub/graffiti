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

using Graffiti.Core.Primitives;
using Graffiti.Core.Brushes;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Graffiti.Core.Math;
using Microsoft.Xna.Framework;
using Graffiti.Core.Animation.Constants;
using Graffiti.Core.Animation;
using Graffiti.Math;
namespace Graffiti.Samples.Halloween
{
    public static class Scene
    {
        private static Group _scene;

        public static Group Compose(ContentManager content)
        {
            if (_scene == null)
            {
                var starBrush = new Brush
                    {
                        new Layer
                        {
                            Texture = content.Load<Texture2D>("Content/Star"),
                            BlendState = BlendState.AlphaBlend,
                            AddressU = TextureAddressMode.Clamp,
                            AddressV = TextureAddressMode.Clamp,
                            AlphaTestEnable = true,
                            AlphaFunction = CompareFunction.Greater,
                            ReferenceAlpha = 128,
                            Transform = new TransformGroup
                            {
                                (ConstantTransform)Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, 0f)),
                                RotateTransform.Procedural(t => Quaternion.CreateFromAxisAngle(Vector3.UnitZ, Functions.Linear(t, 0.0001f))),
                                ScaleTransform.Procedural(t => new Vector3(Functions.Sine(t, 1f, 0.2f, 0f, 20f), Functions.Sine(t, 1f, 0.2f, 0f, 20f), 1f)),
                                (ConstantTransform)Matrix.CreateTranslation(new Vector3(0.5f, 0.5f, 0f)),
                            }
                        }
                    };

                var swayTransform = new TransformGroup
                    {
                        (ConstantTransform)Matrix.CreateTranslation(new Vector3(-0.5f, 0f, 0f)),
                        new RotateTransform
                        {
                            Keyframes = new Keyframes<Quaternion>
                            {
                                { 0f,    Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 0) },
                                { 1000f, Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 0.1f) },
                                { 2000, Quaternion.CreateFromAxisAngle(Vector3.UnitZ, -0.1f) },
                                { 2500, Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 0) },
                            },
                            Mode = Mode.Loop
                        },
                        (ConstantTransform)Matrix.CreateTranslation(new Vector3(0.5f, 0f, 0f))
                    };

                _scene = new Group
                {
                    new Quad(172, 38, 666, 666, -1)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                Texture = content.Load<Texture2D>("Content/Tree"),
                                AlphaTestEnable = true,
                                AlphaFunction = CompareFunction.Greater,
                                ReferenceAlpha = 128
                            }
                        }
                    },

                    new Quad(0, 451, 844, 310, 0)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                Texture = content.Load<Texture2D>("Content/Base"),
                                AlphaTestEnable = true,
                                AlphaFunction = CompareFunction.Greater,
                                ReferenceAlpha = 128
                            }
                        }
                    },
                    new Quad(0, 0, 756, 761, -2)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                Texture = content.Load<Texture2D>("Content/Moon"),
                                AlphaTestEnable = true,
                                AlphaFunction = CompareFunction.Greater,
                                ReferenceAlpha = 128
                            }
                        }
                    },
                    new Quad(123, 128, 260, 260, 0)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                BlendState = BlendState.AlphaBlend,
                                Texture = content.Load<Texture2D>("Content/Jack O'Lantern 1 glow mask"),
                                Color = new Animatable<Color>(t => Color.Lerp(Color.Black, Color.OrangeRed, 
                                    Functions.Noise(t, 0f, 1f, 0f, 2f))),
                                Transform = swayTransform
                            },
                            new Layer
                            {
                                BlendState = BlendState.AlphaBlend,
                                Texture = content.Load<Texture2D>("Content/Jack O'Lantern 1"),
                                AddressU = TextureAddressMode.Clamp,
                                AddressV = TextureAddressMode.Clamp,
                                AlphaTestEnable = true,
                                ReferenceAlpha = 128,
                                AlphaFunction = CompareFunction.Greater,
                                Transform = swayTransform
                            }
                        }
                    },
                    new Quad(588, 8, 128, 128, 0)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                Texture = content.Load<Texture2D>("Content/Owl-EyeWhites"),
                                BlendState = BlendState.AlphaBlend,
                                AlphaTestEnable = true,
                                ReferenceAlpha = 128,
                                AlphaFunction = CompareFunction.Greater
                            },
                            new Layer
                            {
                                Texture = content.Load<Texture2D>("Content/Owl-Eyes"),
                                BlendState = BlendState.AlphaBlend,
                                AlphaTestEnable = true,
                                ReferenceAlpha = 128,
                                AlphaFunction = CompareFunction.Greater,
                                Transform = TranslateTransform.Procedural(t => new Vector3(Functions.Noise(t, -0.03f, 0.06f, 0f, 10f),
                                    Functions.Noise(t, -0.03f, 0.06f, 783f, 10f), 0f))
                            },
                            // TODO: later!
                            //new Layer
                            //{
                            //    Texture = Content.Load<Texture2D>("Content/Owl-Eyelids"),
                            //    AddressU = TextureAddressMode.Mirror,
                            //    AddressV = TextureAddressMode.Mirror,
                            //    BlendState = BlendState.AlphaBlend,
                            //    Transform = Animatable<Matrix>.Make(t => Matrix.CreateScale(new Vector3(1f, 0.0625f, 1f)))
                            //},
                            new Layer
                            {
                                Texture = content.Load<Texture2D>("Content/Owl"),
                                BlendState = BlendState.AlphaBlend,
                                AlphaTestEnable = true,
                                ReferenceAlpha = 128,
                                AlphaFunction = CompareFunction.Greater
                            }
                        }
                    },
                    new Quad(363, 315, 154, 154, 0)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                BlendState = BlendState.AlphaBlend,
                                Texture = content.Load<Texture2D>("Content/Jack O'Lantern 2 glow mask"),
                                Color = new Animatable<Color>(t => Color.Lerp(Color.Black, Color.OrangeRed, 
                                    Functions.Noise(t, 0f, 1f, 0f, 2f))),
                                Transform = swayTransform
                            },
                            new Layer
                            {
                                BlendState = BlendState.AlphaBlend,
                                Texture = content.Load<Texture2D>("Content/Jack O'Lantern 2"),
                                AddressU = TextureAddressMode.Clamp,
                                AddressV = TextureAddressMode.Clamp,
                                AlphaTestEnable = true,
                                ReferenceAlpha = 128,
                                AlphaFunction = CompareFunction.Greater,
                                Transform = swayTransform
                            }
                        }
                    },
                    new Quad(243, 80, 164, 164, 0)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                BlendState = BlendState.AlphaBlend,
                                Texture = content.Load<Texture2D>("Content/Jack O'Lantern 3 glow mask"),
                                Color = new Animatable<Color>(t => Color.Lerp(Color.Black, Color.OrangeRed, 
                                    Functions.Noise(t, 0f, 1f, 0f, 2f))),
                                Transform = swayTransform
                            },
                            new Layer
                            {
                                BlendState = BlendState.AlphaBlend,
                                Texture = content.Load<Texture2D>("Content/Jack O'Lantern 3"),
                                AddressU = TextureAddressMode.Clamp,
                                AddressV = TextureAddressMode.Clamp,
                                AlphaTestEnable = true,
                                ReferenceAlpha = 128,
                                AlphaFunction = CompareFunction.Greater,
                                Transform = swayTransform
                            }
                        }
                    },
                    new Quad(667, 418, 170, 170)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                BlendState = BlendState.AlphaBlend,
                                Texture = content.Load<Texture2D>("Content/Jack O'Lantern 4 glow mask"),
                                Color = new Animatable<Color>(t => Color.Lerp(Color.Black, Color.OrangeRed, 
                                    Functions.Noise(t, 0f, 1f, 0f, 2f))),
                                Transform = swayTransform
                            },
                            new Layer
                            {
                                BlendState = BlendState.AlphaBlend,
                                Texture = content.Load<Texture2D>("Content/Jack O'Lantern 4"),
                                AddressU = TextureAddressMode.Clamp,
                                AddressV = TextureAddressMode.Clamp,
                                AlphaTestEnable = true,
                                ReferenceAlpha = 128,
                                AlphaFunction = CompareFunction.Greater,
                                Transform = swayTransform
                            }
                        }
                    },

                    #region Stars

                    new Quad(1007, 175, 128, 128, -3) { Brush = starBrush },
                    new Quad(812, 38, 77, 77, -3) { Brush = starBrush },
                    new Quad(1049, 437, 37, 37, -3) { Brush = starBrush },
                    new Quad(1075, 111, 37, 37, -3) { Brush = starBrush },
                    new Quad(62, 513, 37, 37, -3) { Brush = starBrush },
                    new Quad(27, 608, 37, 37, -3) { Brush = starBrush },
                    new Quad(208, 535, 37, 37, -3) { Brush = starBrush },
                    new Quad(942, 473, 77, 77, -3) { Brush = starBrush },
                    new Quad(782, 288, 77, 77, -3) { Brush = starBrush },
                    new Quad(1140, 648, 77, 77, -3) { Brush = starBrush },
                    new Quad(-43, 378, 128, 128, -3) { Brush = starBrush },
                    new Quad(844, 621, 128, 128, -3) { Brush = starBrush },
                    new Quad(912, 235, 37, 37, -3) { Brush = starBrush },
                    new Quad(997, 342, 37, 37, -3) { Brush = starBrush },
                    new Quad(880, 407, 37, 37, -3) { Brush = starBrush },
                    new Quad(1137, 8, 37, 37, -3) { Brush = starBrush },
                    new Quad(957, 78, 37, 37, -3) { Brush = starBrush },
                    new Quad(1189, 473, 37, 37, -3) { Brush = starBrush },
                    new Quad(1165, 320, 37, 37, -3) { Brush = starBrush },
                    new Quad(1059, 621, 37, 37, -3) { Brush = starBrush },
                    new Quad(797, 500, 37, 37, -3) { Brush = starBrush },
                    new Quad(706, 608, 37, 37, -3) { Brush = starBrush },
                    new Quad(782, 638, 37, 37, -3) { Brush = starBrush },
                    
                    #endregion

                    new Quad(706, 624, 600, 137, 0)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                Texture = content.Load<Texture2D>("Content/Grass"),
                                BlendState = BlendState.AlphaBlend,
                                AddressU = TextureAddressMode.Wrap,
                                AddressV = TextureAddressMode.Clamp,
                                Transform = new TransformGroup
                                {
                                    (ConstantMatrix)Matrix.CreateTranslation(0f, -1f, 0f),
                                    ShearTransform.Procedural(t => new Vector2(0f, Functions.Sine(t, 0f, 0.1f, 0f, 5f))),
                                    (ConstantMatrix)Matrix.CreateTranslation(0f, 1f, 0f)
                                }
                            }
                        }
                    },

                    // TODO: Replace the linear with noise, perhaps?
                    new Quad(0, 557, 1280, 137, 0)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                Color = (ConstantColor)new Color(255, 255, 255, 50),
                                Texture = content.Load<Texture2D>("Content/Mists2"),
                                BlendState = BlendState.Additive,
                                AddressU = TextureAddressMode.Wrap,
                                AddressV = TextureAddressMode.Clamp,
                                Transform = TranslateTransform.Procedural(t => new Vector3(Functions.Linear(t, 0.00001f), 0f, 0f))
                            },
                            new Layer
                            {
                                Color = (ConstantColor)new Color(255, 255, 255, 50),
                                Texture = content.Load<Texture2D>("Content/Mists4"),
                                BlendState = BlendState.Additive,
                                AddressU = TextureAddressMode.Wrap,
                                AddressV = TextureAddressMode.Clamp,
                                Transform = TranslateTransform.Procedural(t => new Vector3(Functions.Linear(t, 0.000001f), 0f, 0f))
                            }
                        }
                    },

                    new Quad(0, 557, 1280, 205, 0)
                    {
                        Brush = new Brush
                        {
                            new Layer
                            {
                                Color = (ConstantColor)new Color(255, 255, 255, 50),
                                Texture = content.Load<Texture2D>("Content/Mists1"),
                                BlendState = BlendState.Additive,
                                AddressU = TextureAddressMode.Wrap,
                                AddressV = TextureAddressMode.Clamp,
                                Transform = TranslateTransform.Procedural(t => new Vector3(Functions.Linear(t, 0.000025f), 0f, 0f))
                            },
                            new Layer
                            {
                                Color = (ConstantColor)new Color(255, 255, 255, 50),
                                Texture = content.Load<Texture2D>("Content/Mists3"),
                                BlendState = BlendState.Additive,
                                AddressU = TextureAddressMode.Wrap,
                                AddressV = TextureAddressMode.Clamp,
                                Transform = TranslateTransform.Procedural(t => new Vector3(Functions.Linear(t, 0.000006f), 0f, 0f))
                            }
                        }
                    }
                };
            }

            return _scene;
        }
    }
}