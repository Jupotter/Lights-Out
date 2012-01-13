using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    class Game
    {
        Map map;
        Player player;
        TCODConsole root = TCODConsole.root;

        public bool Exit = false;

        public Game()
        {
            MapGen gen = new MapGen();
            gen.Generate(5, out map);
            player = new Player(map);
            map.Player = player;
            player.PlaceAt(map.StartPosX, map.StartPosY);

            for (int i = 0; i < 10; i++)
            {
                int x, y;
                gen.FindOpenSpot(out x, out y, map);
                Monster m = new Monster('X', TCODColor.red, map, 20);
                m.PlaceAt(x, y);
            }
        }

        public void Draw()
        {
            map.Draw(root);
            player.Draw(root);
            TCODConsole.flush();
        }

        public void Update()
        {
            TCODKey key = TCODConsole.waitForKeypress(true);
            HandleKeyPress(key);
            map.Update();
        }

        public void HandleKeyPress(TCODKey key)
        {
            #region switch (key.KeyCode)
            switch (key.KeyCode)
            {
                case TCODKeyCode.KeypadZero:
                    break;
                case TCODKeyCode.KeypadOne:
                    player.Move(-1, 1);
                    break;
                case TCODKeyCode.KeypadTwo:
                    player.Move(0, 1);
                    break;
                case TCODKeyCode.KeypadThree:
                    player.Move(1, 1);
                    break;
                case TCODKeyCode.KeypadFour:
                    player.Move(-1, 0);
                    break;
                case TCODKeyCode.KeypadFive:
                    break;
                case TCODKeyCode.KeypadSix:
                    player.Move(1, 0);
                    break;
                case TCODKeyCode.KeypadSeven:
                    player.Move(-1, -1);
                    break;
                case TCODKeyCode.KeypadEight:
                    player.Move(0, -1);
                    break;
                case TCODKeyCode.KeypadNine:
                    player.Move(1, -1);
                    break;
                case TCODKeyCode.KeypadAdd:
                    break;
                case TCODKeyCode.KeypadSubtract:
                    break;
                case TCODKeyCode.KeypadMultiply:
                    break;
                case TCODKeyCode.KeypadDivide:
                    break;
                case TCODKeyCode.KeypadEnter:
                    break;
                case TCODKeyCode.KeypadDecimal:
                    break;
                case TCODKeyCode.Left:
                    player.Move(-1, 0);
                    break;
                case TCODKeyCode.Right:
                    player.Move(1, 0);
                    break;
                case TCODKeyCode.Up:
                    player.Move(0, -1);
                    break;
                case TCODKeyCode.Down:
                    player.Move(0, 1);
                    break;
                case TCODKeyCode.Alt:
                    break;
                case TCODKeyCode.Apps:
                    break;
                case TCODKeyCode.Backspace:
                    break;
                case TCODKeyCode.Capslock:
                    break;
                case TCODKeyCode.Char:
                    break;
                case TCODKeyCode.Control:
                    break;
                case TCODKeyCode.Delete:
                    break;
                case TCODKeyCode.Eight:
                    break;
                case TCODKeyCode.End:
                    break;
                case TCODKeyCode.Enter:
                    break;
                case TCODKeyCode.Escape:
                    Exit = true;
                    break;
                case TCODKeyCode.F1:
                    break;
                case TCODKeyCode.F2:
                    break;
                case TCODKeyCode.F3:
                    break;
                case TCODKeyCode.F4:
                    break;
                case TCODKeyCode.F5:
                    break;
                case TCODKeyCode.F6:
                    break;
                case TCODKeyCode.F7:
                    break;
                case TCODKeyCode.F8:
                    break;
                case TCODKeyCode.F9:
                    break;
                case TCODKeyCode.F10:
                    break;
                case TCODKeyCode.F11:
                    break;
                case TCODKeyCode.F12:
                    break;
                case TCODKeyCode.Five:
                    break;
                case TCODKeyCode.Four:
                    break;
                case TCODKeyCode.Home:
                    break;
                case TCODKeyCode.Insert:
                    break;
                case TCODKeyCode.Lwin:
                    break;
                case TCODKeyCode.Nine:
                    break;
                case TCODKeyCode.NoKey:
                    break;
                case TCODKeyCode.Numlock:
                    break;
                case TCODKeyCode.One:
                    break;
                case TCODKeyCode.Pagedown:
                    break;
                case TCODKeyCode.Pageup:
                    break;
                case TCODKeyCode.Pause:
                    break;
                case TCODKeyCode.Printscreen:
                    break;
                case TCODKeyCode.Rwin:
                    break;
                case TCODKeyCode.Scrolllock:
                    break;
                case TCODKeyCode.Seven:
                    break;
                case TCODKeyCode.Shift:
                    break;
                case TCODKeyCode.Six:
                    break;
                case TCODKeyCode.Space:
                    break;
                case TCODKeyCode.Tab:
                    break;
                case TCODKeyCode.Three:
                    break;
                case TCODKeyCode.Two:
                    break;
                case TCODKeyCode.Zero:
                    break;
                default:
                    break;
            }
            #endregion

            #region switch (key.Character)
            switch (key.Character)
            {
                #region Movement
                case 'h':
                    player.Move(-1, 0);
                    break;
                case 'j':
                    player.Move(0, 1);
                    break;
                case 'k':
                    player.Move(0, -1);
                    break;
                case 'l':
                    player.Move(1, 0);
                    break;
                case 'y':
                    player.Move(-1, -1);
                    break;
                case 'u':
                    player.Move(1, -1);
                    break;
                case 'b':
                    player.Move(-1, 1);
                    break;
                case 'n':
                    player.Move(1, 1);
                    break;
                #endregion
                case 'd':
                    player.Inventory.Draw(root);
                    TCODConsole.flush();
                    key = TCODConsole.waitForKeypress(true);
                    Item i = player.Inventory.GetAtLetter(key.Character);
                    player.Inventory.RemoveAtLetter(key.Character, 1);
                    if (i != null)
                    {
                        Light l = new Light(10);
                        i.SetLight(l);
                        i.Drop(player.posX, player.posY, map);
                    }
                    break;
                case 'g':
                    foreach (Item item in map.GetItemsAt(player.posX, player.posY))
                    {
                        item.Get();
                        player.Inventory.Add(item);
                    }
                    break;
                case 'i':
                    player.Inventory.Draw(root);
                    TCODConsole.flush();
                    TCODConsole.waitForKeypress(true);
                    break;
                default:
                    break;
            }
            #endregion
        }
    }
}
