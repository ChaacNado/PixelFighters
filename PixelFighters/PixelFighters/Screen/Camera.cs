using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PixelFighters
{
    public class Camera
    {
        public Matrix transform;
        public Vector2 pos;
        Viewport view;
        public Vector2 cameraFocus;
        public float zoom;
        public bool inMenu;

        public Camera(Viewport view)
        {
            this.view = view;
        }

        public void Update(GameTime gameTime)
        {
            pos = new Vector2(cameraFocus.X - ScreenManager.Instance.Dimensions.X / 2, cameraFocus.Y - ScreenManager.Instance.Dimensions.Y / 2);

            ///Ger fast position på kameran inuti menyer
            if (inMenu)
            {
                if (pos.X < 0)
                {
                    pos.X = 0;
                }
                if (pos.X > ScreenManager.Instance.Dimensions.X)
                {
                    pos.X = ScreenManager.Instance.Dimensions.X;
                }

                if (pos.Y < 0)
                {
                    pos.Y = 0;
                }
                if (pos.Y > 0)
                {
                    pos.Y = 0;
                }
            }
            ///Begränsar området kameran kan röra sig när spelet spelas
            else
            {
                if (pos.X < -300)
                {
                    pos.X = -300;
                }
                if (pos.X > 300)
                {
                    pos.X = 300;
                }

                if (pos.Y < -700)
                {
                    pos.Y = -700;
                }
                if (pos.Y > 80)
                {
                    pos.Y = 80;
                }
            }

            transform = Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) * Matrix.CreateScale(new Vector3(zoom, zoom, 1));
        }
    }
}
