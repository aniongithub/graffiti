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
using Microsoft.Xna.Framework.Graphics;

namespace Graffiti.Core.Text
{
    internal sealed class BitmapText<TVertex, TTexcoords>: IBitmapText<TVertex, TTexcoords>
        where TTexcoords : struct, ITexcoords
        where TVertex : struct, IVertex<TTexcoords>
    {
        private readonly BitmapFont _bitmapFont;
        private readonly TextBrush[] _textPageBrushes;
        private readonly IIndexedMesh<TVertex, TTexcoords>[] _textQuads;
        private readonly string _text;

        public BitmapText(BitmapFont bitmapFont, string text)
        {
            var pageCount = bitmapFont.PageBrushes.Count;

            _bitmapFont = bitmapFont;
            _text = text;
            _textPageBrushes = new TextBrush[pageCount];
            for (int i = 0; i < pageCount; i++)
                _textPageBrushes[i] = bitmapFont.PageBrushes[i];

            _textQuads = new IIndexedMesh<TVertex, TTexcoords>[pageCount];

            var charactersByPage = (from page in Enumerable.Range(0, pageCount)
                                   from ch in text
                                   select new KeyValuePair<int, char>(page, ch)).ToMultiValueDictionary();

            var w = (float)bitmapFont.ScaleW;
            var h = (float)bitmapFont.ScaleH;
            var xPos = 0f;

            foreach (var kvp in charactersByPage)
            {
                var charCount = kvp.Value.Count;
                _textQuads[kvp.Key] = new IndexedMesh<TVertex, TTexcoords>(charCount * 4, charCount * 6)
                                          {
                                              Brush = _textPageBrushes[kvp.Key]
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

                var topLeft = Texcoords.Create<TTexcoords>(new Vector2(ci.X / w, ci.Y / h));
                var bottomLeft = Texcoords.Create<TTexcoords>(new Vector2(ci.X / w, ((ci.Y + ci.Height) / h)));
                var topRight = Texcoords.Create<TTexcoords>(new Vector2(((ci.X + ci.Width) / w), ci.Y / h));
                var bottomRight = Texcoords.Create<TTexcoords>(new Vector2(((ci.X + ci.Width) / w), ((ci.Y + ci.Height) / h)));

                var left = xPos + ci.XOffset;
                var top = 0f + ci.YOffset;

                vertices[vertexCount++] = new TVertex
                {
                    Position = new Vector3(left, top, 0f),
                    Texcoords = topLeft,
                    Color = Color.White
                };

                vertices[vertexCount++] = new TVertex
                {
                    Position = new Vector3(left + ci.Width, top, 0f),
                    Texcoords = topRight,
                    Color = Color.White
                };

                vertices[vertexCount++] = new TVertex
                {
                    Position = new Vector3(left + ci.Width, top + ci.Height, 0f),
                    Texcoords = bottomRight,
                    Color = Color.White
                };

                vertices[vertexCount++] = new TVertex
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

        private IBrush _brush;
        public IBrush Brush
        {
            get { return _brush; }
            set 
            {
                _brush = value;
                for (int i = 0; i < _textPageBrushes.Length; i++)
                    _textPageBrushes[i].SubBrush = value;
            }
        }
        public Matrix Transform { get; set; }

        public string Text
        {
            get { return _text; }
        }
    }

    public static class BitmapTextExtensions
    {
        public static IBitmapText<TVertex, TTexcoords> Build<TVertex, TTexcoords>(this IBitmapFont font, string text, IBrush brush)
            where TTexcoords: struct, ITexcoords
            where TVertex: struct, IVertex<TTexcoords>
        {
            return new BitmapText<TVertex, TTexcoords>(font as BitmapFont, text)
                       {
                           Brush = brush
                       };
        }

        public static Rectangle Measure(this IBitmapFont font, string text)
        {
            throw new NotImplementedException();
        }
    }
}