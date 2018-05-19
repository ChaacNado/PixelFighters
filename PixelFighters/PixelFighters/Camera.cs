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
        Vector2 centre;
        Viewport view;
        public Vector2 cameraFocus;
        public float zoom;

        public Camera(Viewport newView)
        {
            this.view = newView;
        }

        public void Update(GameTime gameTime)
        {
            centre = new Vector2(cameraFocus.X - ScreenManager.Instance.Dimensions.X / 2, cameraFocus.Y - ScreenManager.Instance.Dimensions.Y / 2);
            transform = Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0)) * Matrix.CreateScale(new Vector3(zoom, zoom, 1));
        }
    }
}
