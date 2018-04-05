﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    ///Kan användas för att korta ner GameplayManager
    ///Exempel: Texturer kan laddas in med följande rad i förslagsvis GameplayManager
    ///spriteBatch.Draw(TextureManager.Instance.sampleTex, new Vector2(120, 540), Color.White);
    public class TextureManager
    {
        #region Variables

        ContentManager content;

        public Texture2D rectTex;

        private static TextureManager instance;

        #endregion

        #region Properties
        ///Skapar bara en instance av klassen
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
            rectTex = Content.Load<Texture2D>("tile");
        }
        #endregion
    }
}
