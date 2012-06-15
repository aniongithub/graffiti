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
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Graffiti.Core.Brushes;
using Graffiti.Core.Text;
using Graffiti.Core.Extensions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
    public static partial class ContentExtensions
    {
        public static IBitmapFont LoadBitmapFont(this ContentManager content, string name)
        {
            var currentContentPath = Path.GetDirectoryName(name);
            using (var stream = OpenStream(content, name))
            {
                var document = XDocument.Load(stream);
                var infoNode = document.Element("font").Element("info");
                var commonNode = document.Element("font").Element("common");
                var pagesNode = document.Element("font").Element("pages");
                var charsNode = document.Element("font").Element("chars");

                return new BitmapFont
                           {
                               Face = infoNode.Attribute("face").Value,
                               Size = int.Parse(infoNode.Attribute("size").Value),
                               Bold = int.Parse(infoNode.Attribute("bold").Value) == 0 ? false : true,
                               Italic = int.Parse(infoNode.Attribute("italic").Value) == 0 ? false : true,

                               LineHeight = int.Parse(commonNode.Attribute("lineHeight").Value),
                               Base = int.Parse(commonNode.Attribute("base").Value),
                               ScaleW = int.Parse(commonNode.Attribute("scaleW").Value),
                               ScaleH = int.Parse(commonNode.Attribute("scaleH").Value),

                               Pages = (from page in pagesNode.Elements("page")
                                        select new KeyValuePair<int, string>(
                                            int.Parse(page.Attribute("id").Value),
                                            page.Attribute("file").Value
                                            )).ToDictionary(),

                               PageLayers = (from page in pagesNode.Elements("page")
                                               select new KeyValuePair<int, ILayer>(
                                                   int.Parse(page.Attribute("id").Value),
                                                        new Layer
                                                            {
                                                                Texture = content.Load<Texture2D>(Path.Combine(currentContentPath, Path.GetFileNameWithoutExtension(page.Attribute("file").Value))),
                                                                BlendState = BlendState.AlphaBlend
                                                            }
                                                   )).ToDictionary(),

                               Chars = (from ch in charsNode.Elements("char")
                                        select new KeyValuePair<int, ICharInfo>
                                            (
                                            int.Parse(ch.Attribute("id").Value),
                                            new CharInfo
                                                {
                                                    X = int.Parse(ch.Attribute("x").Value),
                                                    Y = int.Parse(ch.Attribute("y").Value),
                                                    Width = int.Parse(ch.Attribute("width").Value),
                                                    Height = int.Parse(ch.Attribute("height").Value),
                                                    XOffset = int.Parse(ch.Attribute("xoffset").Value),
                                                    YOffset = int.Parse(ch.Attribute("yoffset").Value),
                                                    XAdvance = int.Parse(ch.Attribute("xadvance").Value),
                                                    Page = int.Parse(ch.Attribute("page").Value),
                                                    Channel = int.Parse(ch.Attribute("chnl").Value),
                                                }
                                            )).ToDictionary()
                           };
            }
        }
    }
}