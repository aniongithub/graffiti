using System.IO;
using Microsoft.Xna.Framework.Content;

namespace Microsoft.Xna.Framework
{
    public static class ContentExtensions
    {
        public static Stream OpenStream(this ContentManager content, string name)
        {
            return new FileStream(name, FileMode.Open);
        }
    }
}