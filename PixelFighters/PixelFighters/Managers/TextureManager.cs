﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class TextureManager
    {
        #region Variables

        ContentManager content;

        public SpriteFont spriteFont;
        public Texture2D rectTex, fadeTex, boxManTex;

        private static TextureManager instance;

        #endregion

        #region Properties
        ///Skapar bara en instans av klassen
        public static TextureManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TextureManager();
                }
                return instance;
            }
        }
        #endregion

        #region Main Methods
        public void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            spriteFont = Content.Load<SpriteFont>("font1");
            rectTex = Content.Load<Texture2D>("tile");
            fadeTex = Content.Load<Texture2D>("fade");
            boxManTex = Content.Load<Texture2D>("boxMan");
        }
        #endregion
    }
}
