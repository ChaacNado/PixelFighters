using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class ScreenManager
    {
        #region Variables
        private static ScreenManager instance;
        #endregion

        #region Properties
        ///Skapar bara en instance av klassen
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }

        ///Skärmens bredd och höjd
        public Vector2 Dimensions { get; set; }
        #endregion
    }
}
