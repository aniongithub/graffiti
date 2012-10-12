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
using System.Collections.Generic;
using System.Linq;
using Graffiti.Core.Animation;
using Graffiti.Core.Brushes;
using Graffiti.Core.Extensions;
using Graffiti.Core.Geometry;
using Graffiti.Core.Rendering;
using Microsoft.Xna.Framework;

namespace Graffiti.Core.Text
{
    public enum HAlignment
    {
        Left,
        Middle,
        Right
    }

    public enum VAlignment
    {
        Top,
        Middle,
        Bottom
    }

    public sealed class BitmapText: IBitmapText
    {
        private readonly BitmapFont _bitmapFont;
        private readonly IIndexedMesh[] _textQuads;
        private readonly string _text;

        public BitmapText(IBitmapFont bitmapFont, IBrush brush, string text, TexcoordGenerationMode uMappingMode = TexcoordGenerationMode.FullBoundingBox, TexcoordGenerationMode vMappingMode = TexcoordGenerationMode.FullBoundingBox, HAlignment hAlignment = HAlignment.Middle, VAlignment vAlignment = VAlignment.Middle)
        {
            Transform = (ConstantMatrix) Matrix.Identity;
            Rectangle fullRect = bitmapFont.Measure(text);

            Vector3 centerOffset = Vector3.Zero;
            switch (hAlignment)
            {
                case HAlignment.Left:
                    centerOffset.X = 0;
                    break;

                case HAlignment.Middle:
                    centerOffset.X = fullRect.Width / 2f;
                    break;

                case HAlignment.Right:
                    centerOffset.X = fullRect.Width;
                    break;
            }

            switch (vAlignment)
            {
                case VAlignment.Top:
                    centerOffset.Y = 0;
                    break;

                case VAlignment.Middle:
                    centerOffset.Y = fullRect.Height / 2f;
                    break;

                case VAlignment.Bottom:
                    centerOffset.Y = fullRect.Height;

                    break;
            }

            _bitmapFont = bitmapFont as BitmapFont;
            var pageCount = _bitmapFont.PageLayers.Count;
            _text = text;
            Brush = brush;

            _textQuads = new IIndexedMesh[pageCount];

            var charactersByPage = (from char ch in text
                                   select new KeyValuePair<int, char>(_bitmapFont.Chars[ch].Page, ch)).ToMultiValueDictionary();

            var w = (float)_bitmapFont.ScaleW;
            var h = (float)_bitmapFont.ScaleH;
            var xPos = 0f;

            foreach (var kvp in charactersByPage)
            {
                var charCount = kvp.Value.Count;
                _textQuads[kvp.Key] = new IndexedMesh(charCount * 4, charCount * 6)
                                          {
                                              Brush = new TextBrush { TextLayer = _bitmapFont.PageLayers[kvp.Key], SubBrush = Brush }
                                          };
            }

            var vertexIndexCounts = (from i in Enumerable.Range(0, _bitmapFont.Pages.Count)
                                    select Tuple.Create(0, 0)).ToArray();

            foreach (var ch in text)
            {
                var ci = _bitmapFont.Chars[ch];

                var vertices = _textQuads[ci.Page].Vertices;
                var indices = _textQuads[ci.Page].Indices;

                var vertexCount = vertexIndexCounts[ci.Page].Item1;
                var indexCount = vertexIndexCounts[ci.Page].Item2;

                var saveVertexCount = vertexCount;

                Vector2 quadTopLeft;
                Vector2 quadTopRight;
                Vector2 quadBottomRight;
                Vector2 quadBottomLeft;

                var left = xPos + ci.XOffset;
                var top = 0f + ci.YOffset;

                switch (uMappingMode)
                {
                    case TexcoordGenerationMode.PerQuad:
                        quadTopLeft.X = 0f;
                        quadTopRight.X = 1f;
                        quadBottomRight.X = 1f;
                        quadBottomLeft.X = 0f;

                        break;

                    // These two modes are the same in this case
                    case TexcoordGenerationMode.FullBoundingBox:
                    case TexcoordGenerationMode.Unwrapped_Linear:
                        quadTopLeft.X = left / fullRect.Width;
                        quadTopRight.X = (left + ci.Width) / fullRect.Width;
                        quadBottomRight.X = (left + ci.Width) / fullRect.Width;
                        quadBottomLeft.X = left / fullRect.Width;

                        break;

                    default:
                        throw new NotSupportedException(string.Format("Unsupported texture mapping mode along X-Axis: {0}", uMappingMode));
                }

                switch (vMappingMode)
                {
                    case TexcoordGenerationMode.PerQuad:
                        quadTopLeft.Y = 0f;
                        quadTopRight.Y = 0f;
                        quadBottomRight.Y = 1f;
                        quadBottomLeft.Y = 1f;

                        break;

                    // These two modes are the same in this case
                    case TexcoordGenerationMode.FullBoundingBox:
                    case TexcoordGenerationMode.Unwrapped_Linear:
                        quadTopLeft.Y = top / fullRect.Height;
                        quadTopRight.Y = top / fullRect.Height;
                        quadBottomRight.Y = (top + ci.Height) / fullRect.Height;
                        quadBottomLeft.Y = (top + ci.Height) / fullRect.Height;

                        break;

                    default:
                        throw new NotSupportedException(string.Format("Unsupported texture mapping mode along Y-Axis: {0}", vMappingMode));
                }

                var topLeft = new Texcoords(quadTopLeft, new Vector2(ci.X / w, ci.Y / h));
                var topRight = new Texcoords(quadTopRight, new Vector2(((ci.X + ci.Width) / w), ci.Y / h));
                var bottomRight = new Texcoords(quadBottomRight, new Vector2(((ci.X + ci.Width) / w), ((ci.Y + ci.Height) / h)));
                var bottomLeft = new Texcoords(quadBottomLeft, new Vector2(ci.X / w, ((ci.Y + ci.Height) / h)));

                vertices[vertexCount++] = new Vertex
                {
                    Position = new Vector3(left - centerOffset.X, top - centerOffset.Y, 0f),
                    Texcoords = topLeft,
                    Color = Color.White
                };

                vertices[vertexCount++] = new Vertex
                {
                    Position = new Vector3(left + ci.Width - centerOffset.X, top - centerOffset.Y, 0f),
                    Texcoords = topRight,
                    Color = Color.White
                };

                vertices[vertexCount++] = new Vertex
                {
                    Position = new Vector3(left + ci.Width - centerOffset.X, top + ci.Height - centerOffset.Y, 0f),
                    Texcoords = bottomRight,
                    Color = Color.White
                };

                vertices[vertexCount++] = new Vertex
                {
                    Position = new Vector3(left - centerOffset.X, top + ci.Height - centerOffset.Y, 0f),
                    Texcoords = bottomLeft,
                    Color = Color.White
                };

                xPos += ci.XAdvance;

                indices[indexCount++] = (short)(saveVertexCount + 0);
                indices[indexCount++] = (short)(saveVertexCount + 1);
                indices[indexCount++] = (short)(saveVertexCount + 2);
                indices[indexCount++] = (short)(saveVertexCount + 0);
                indices[indexCount++] = (short)(saveVertexCount + 2);
                indices[indexCount++] = (short)(saveVertexCount + 3);

                vertexIndexCounts[ci.Page] = Tuple.Create(vertexCount, indexCount);
            }
        }

        public void Render(IRenderer renderer, Matrix parentTransform)
        {
            var current = Transform.Current;
            for (int i = 0; i < _textQuads.Length; i++)
                _textQuads[i].Render(renderer, current);
        }

        public IBrush Brush { get; set; }
        public IAnimatable<Matrix> Transform { get; set; }

        public string Text
        {
            get { return _text; }
        }

        public void Update(float timeInMilliSeconds)
        {
            if (Transform != null)
                Transform.Update(timeInMilliSeconds);
            if (Brush != null)
                Brush.Update(timeInMilliSeconds);
        }
    }

    public static class BitmapTextExtensions
    {
        public static IBitmapText Build(this IBitmapFont font, string text, IBrush brush, TexcoordGenerationMode uMappingMode = TexcoordGenerationMode.FullBoundingBox, TexcoordGenerationMode vMappingMode = TexcoordGenerationMode.FullBoundingBox, HAlignment hAlignment = HAlignment.Middle, VAlignment vAlignment = VAlignment.Middle)
        {
            return new BitmapText(font as BitmapFont, brush, text, uMappingMode, vMappingMode, hAlignment, vAlignment);
        }
        public static Rectangle Measure(this IBitmapFont font, string text)
        {
            var bitmapFont = font as BitmapFont;
            
            int width = 0;
            int height = 0;

            foreach (var ch in text)
            {
                var ci = bitmapFont.Chars[ch];
                width += ci.XAdvance;
                height = System.Math.Max(height, ci.Height);
            }

            return new Rectangle(0, 0, width, height);
        }
    }
}