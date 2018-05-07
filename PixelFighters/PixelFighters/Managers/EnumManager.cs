using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public enum GameState
    {
        TitleScreen,
        MainMenu,
        CharacterSelect,
        Playtime,
        Pause,
        Results,
        Options,
        SoundMusic,
        Graphics,
        Controls,
        Credits,
        Quit,
    }

    public enum StageState
    {
        stage1,
        stage2,
    }

    public enum MarkerState
    {
        MarkerState1,
        MarkerState2,
        MarkerState3,
        MarkerState4,
        MarkerState5,
        MarkerState6,
    }
}
