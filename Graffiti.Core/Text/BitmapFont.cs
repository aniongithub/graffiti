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

using System.Collections.Generic;

namespace Graffiti.Core.Text
{
    internal sealed class BitmapFont: IBitmapFont
    {
        public string Face { get; set; }
        public int Size { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public int LineHeight { get; set; }
        public int Base { get; set; }
        public int ScaleW { get; set; }
        public int ScaleH { get; set; }
        public IDictionary<int, string> Pages { get; set; }
        public IDictionary<int, ICharInfo> Chars { get; set; }
        public IDictionary<int, TextBrush> PageBrushes { get; set; }
    }

    internal struct CharInfo : ICharInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int XAdvance { get; set; }
        public int Page { get; set; }
        public int Channel { get; set; }
    }
}