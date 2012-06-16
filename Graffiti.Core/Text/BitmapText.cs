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
using Graffiti.Core.Brushes;
using Graffiti.Core.Extensions;
using Graffiti.Core.Geometry;
using Graffiti.Core.Rendering;
using Microsoft.Xna.Framework;

namespace Graffiti.Core.Text
{
    public enum TextureMappingMode
    { 
        PerCharacter,
        FullRectangle
    }

    internal sealed class BitmapText: IBitmapText
    {
        private readonly BitmapFont _bitmapFont;
        private readonly IIndexedMesh[] _textQuads;
        private readonly string _text;

        public BitmapText(BitmapFont bitmapFont, IBrush brush, string text, TextureMappingMode textureMappingMode)
        {
            Rectangle fullRect = Rectangle.Empty;

            if (textureMappingMode == TextureMappingMode.FullRectangle)
                fullRect = bitmapFont.Measure(text);

            var pageCount = bitmapFont.PageLayers.Count;

            _bitmapFont = bitmapFont;
            _text = text;
            Brush = brush;

            _textQuads = new IIndexedMesh[pageCount];

            var charactersByPage = (from ch in text
                                   select new KeyValuePair<int, char>(bitmapFont.Chars[ch].Page, ch)).ToMultiValueDictionary();

            var w = (float)bitmapFont.ScaleW;
            var h = (float)bitmapFont.ScaleH;
            var xPos = 0f;

            foreach (var kvp in charactersByPage)
            {
                var charCount = kvp.Value.Count;
                _textQuads[kvp.Key] = new IndexedMesh(charCount * 4, charCount * 6)
                                          {
                                              Brush = new TextBrush { TextLayer = bitmapFont.PageLayers[kvp.Key], SubBrush = Brush }
                                          };
            }

            var vertexIndexCounts = (from i in Enumerable.Range(0, bitmapFont.Pages.Count)
                                    select Tuple.Create(0, 0)).ToArray();

            foreach (var ch in text)
            {
                var ci = bitmapFont.Chars[ch];

                var vertices = _textQuads[ci.Page].Vertices;
                var indices = _textQuads[ci.Page].Indices;

                var vertexCount = vertexIndexCounts[ci.Page].Item1;
                var indexCount = vertexIndexCounts[ci.Page].Item2;

                var saveVertexCount = vertexCount;

                var brushTopLeft = new Vector2(0f, 0f);
                var brushTopRight = new Vector2(1f, 0f);
                var brushBottomRight = new Vector2(1f, 1f);
                var brushBottomLeft = new Vector2(0f, 1f);

                var left = xPos + ci.XOffset;
                var top = 0f + ci.YOffset;

                if (textureMappingMode == TextureMappingMode.FullRectangle)
                {
                    brushTopLeft = new Vector2(left / fullRect.Width, top / fullRect.Height);
                    brushTopRight = new Vector2((left + ci.Width) / fullRect.Width, top / fullRect.Height);
                    brushBottomRight = new Vector2((left + ci.Width) / fullRect.Width, (top + ci.Height) / fullRect.Height);
                    brushBottomLeft = new Vector2(left / fullRect.Width, (top + ci.Height) / fullRect.Height);
                }

                var topLeft = new Texcoords(brushTopLeft, new Vector2(ci.X / w, ci.Y / h));
                var topRight = new Texcoords(brushTopRight, new Vector2(((ci.X + ci.Width) / w), ci.Y / h));
                var bottomRight = new Texcoords(brushBottomRight, new Vector2(((ci.X + ci.Width) / w), ((ci.Y + ci.Height) / h)));
                var bottomLeft = new Texcoords(brushBottomLeft, new Vector2(ci.X / w, ((ci.Y + ci.Height) / h)));

                vertices[vertexCount++] = new Vertex
                {
                    Position = new Vector3(left, top, 0f),
                    Texcoords = topLeft,
                    Color = Color.White
                };

                vertices[vertexCount++] = new Vertex
                {
                    Position = new Vector3(left + ci.Width, top, 0f),
                    Texcoords = topRight,
                    Color = Color.White
                };

                vertices[vertexCount++] = new Vertex
                {
                    Position = new Vector3(left + ci.Width, top + ci.Height, 0f),
                    Texcoords = bottomRight,
                    Color = Color.White
                };

                vertices[vertexCount++] = new Vertex
                {
                    Position = new Vector3(left, top + ci.Height, 0f),
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
            for (int i = 0; i < _textQuads.Length; i++)
                _textQuads[i].Render(renderer, parentTransform * Transform);
        }

        public IBrush Brush { get; set; }
        public Matrix Transform { get; set; }

        public string Text
        {
            get { return _text; }
        }
    }

    public static class BitmapTextExtensions
    {
        public static IBitmapText Build(this IBitmapFont font, string text, IBrush brush, TextureMappingMode textureMappingMode = TextureMappingMode.FullRectangle)
        {
            return new BitmapText(font as BitmapFont, brush, text, textureMappingMode);
        }
        public static Rectangle Measure(this IBitmapFont font, string text)
        {
            var bitmapFont = font as BitmapFont;
            
            // var ci = bitmapFont.Chars[text.First()];

            int left = 0;// ci.XOffset;
            int top = 0;//ci.YOffset;
            int width = 0;
            int height = 0;

            var xPos = 0;

            foreach (var ch in text)
            {
                var ci = bitmapFont.Chars[ch];
                width += ci.XAdvance;
                height = System.Math.Max(height, ci.Height);

                // width += ci.XAdvance;
            }

            return new Rectangle(left, top, width, height);
        }
    }
}