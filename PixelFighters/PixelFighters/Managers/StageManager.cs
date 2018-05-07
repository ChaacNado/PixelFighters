using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class StageManager
    {
        #region Variables
        ContentManager content;

        public Vector2 startPosOne, startPosTwo;
        List<string> strings;
        public Player p1, p2;
        public List<Platform> platforms;

        private static StageManager instance;
        #endregion

        #region Properties
        ///Skapar bara en instans av klassen
        public static StageManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StageManager();
                }
                return instance;
            }
        }
        #endregion

        #region Main Methods
        public void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");

            strings = new List<string>();
            platforms = new List<Platform>();

            p1 = new Player(AssetManager.Instance.boxManTex, startPosOne, new Rectangle(0, 0, 50, 50), 1);
            p2 = new Player(AssetManager.Instance.boxManTex, startPosTwo, new Rectangle(0, 0, 50, 50), 2);

            while (!AssetManager.Instance.streamReader.EndOfStream)
            {
                strings.Add(AssetManager.Instance.streamReader.ReadLine());
            }
            AssetManager.Instance.streamReader.Close();

            for (int j = 0; j < strings.Count; j++)
            {
                string[] coordinates = strings[j].Split(';');
                for (int i = 0; i < coordinates.Length; i++)
                {
                    string[] xy = coordinates[i].Split(',');
                    try
                    {
                        int x = Convert.ToInt32(xy[0]);
                        int y = Convert.ToInt32(xy[1]);
                        Vector2 pos = new Vector2(x, y);

                        Rectangle rect = new Rectangle(0, 0, 0, 0);
                        if (xy.Length == 4)
                        {
                            int w = Convert.ToInt32(xy[2]);
                            int h = Convert.ToInt32(xy[3]);
                            rect = new Rectangle(x, y, w, h);
                        }
                        if (j == 0)
                        {
                            rect = new Rectangle(0, 0, 50, 50);
                            startPosOne = new Vector2(x, y);
                        }
                        if (j == 1)
                        {
                            rect = new Rectangle(0, 0, 50, 50);
                            startPosTwo = new Vector2(x, y);
                        }
                        if (j == 2)
                        {
                            platforms.Add(new Platform(AssetManager.Instance.rectTex, rect));
                        }

                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Input string is not a sequence of digits.");
                    }
                }
            }
        }

        #endregion
    }
}
