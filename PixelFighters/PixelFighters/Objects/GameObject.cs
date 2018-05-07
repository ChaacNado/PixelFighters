using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public abstract class GameObject
    {
        public Vector2 pos;
        protected Texture2D tex;
        public Color color;

        public GameObject(Texture2D tex)
        {
            this.tex = tex;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
