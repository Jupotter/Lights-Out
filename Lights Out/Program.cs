using System;
using libtcod;

namespace Lights_Out
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            TCODConsole.initRoot(80, 61, "Lights Out");
            TCODSystem.setFps(100);
            TCODConsole.setKeyboardRepeat(250, 100);
            Game game = new Game();

            game.Draw();
            while (!TCODConsole.isWindowClosed() && !game.Exit)
            {
                game.Update();
            }
        }
    }
}
