using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pung.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// This code was taken from the XNA Wiki at : 
    /// http://www.xnawiki.com/index.php?title=Single-Color_Texture
    /// All credit given to author.
    /// 
    /// </remarks>
    class ColorTexture
    {
        public static Texture2D Create(GraphicsDevice graphicsDevice)
        {
            return Create(graphicsDevice, 1, 1, new Color()); 
        }

        public static Texture2D Create(GraphicsDevice graphicsDevice, Color color)
        {
            return Create(graphicsDevice, 1, 1, new Color());
        }

        public static Texture2D Create(GraphicsDevice graphicsDevice, int width, int heigth, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, heigth, false, SurfaceFormat.Color);

            Color[] colors = new Color[width * heigth];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = new Color(color.ToVector3());

            }
            texture.SetData(colors);

            return texture;

        }

    }
}
