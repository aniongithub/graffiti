using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Graffiti.Samples.Windows.XNA.BitmapFont
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public sealed class CharInfo
        {
            public int X { get; internal set; }
            public int Y { get; internal set; }
            public int Width { get; internal set; }
            public int Height { get; internal set; }
            public int XOffset { get; internal set; }
            public int YOffset { get; internal set; }
            public int XAdvance { get; internal set; }
            public int Page { get; internal set; }
            public int Channel { get; internal set; }
        }

        public sealed class FontInfo
        {
            public string Face { get; internal set; }
            public int Size { get; internal set; }
            public bool Bold { get; internal set; }
            public bool Italic { get; internal set; }
            public int LineHeight { get; internal set; }
            public int Base { get; internal set; }
            public int ScaleW { get; internal set; }
            public int ScaleH { get; internal set; }
            public IDictionary<int, string> Pages { get; internal set; }
            public IDictionary<int, CharInfo> Chars { get; internal set; }
        }

        protected override void LoadContent()
        {
            using (var stream = Content.OpenStream("Content/Segoe_WP_64x64.fnt"))
            {
                var document = XDocument.Load(stream);
                var infoNode = document.Element("font").Element("info");
                var commonNode = document.Element("font").Element("common");
                var pagesNode = document.Element("font").Element("pages");
                var charsNode = document.Element("font").Element("chars");

                var fontInfo = new FontInfo
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

                    Chars = (from ch in charsNode.Elements("char")
                            select new KeyValuePair<int, CharInfo>
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

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }

    internal static class ConversionExtensions
    {
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            var result = new Dictionary<TKey, TValue>();
            foreach (var kvp in items)
                result.Add(kvp.Key, kvp.Value);
            
            return result;
        }
    }
}
