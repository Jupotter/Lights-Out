using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    class Game
    {
        static public bool ShowWall = false;
        static public bool MonsterAI = true;
        static public bool MonsterDamage = true;

        Map map;
        Player player;
        TCODConsole root = TCODConsole.root;
        MapGen gen;

        public bool Exit = false;

        public Game()
        {
            gen = new MapGen();
            gen.Generate(5, out map);
            player = new Player(map);
            map.Player = player;
            player.PlaceAt(map.StartPosX, map.StartPosY);

            for (int i = 0; i < 10; i++)
            {
                AddMonster();
            }
        }

        public void AddMonster()
        {
            int x, y;
            gen.FindOpenSpot(out x, out y, map);
            Monster m = new Monster('X', TCODColor.red, map, 50);
            m.PlaceAt(x, y);
        }

        public void Draw()
        {
            map.Draw(root);
            player.Draw(root);
            TCODConsole.flush();
        }

        public void Update()
        {
            map.Update();
            bool endturn;
            do
            {
                TCODKey key = TCODConsole.waitForKeypress(true);
                endturn = HandleKeyPress(key);
                Draw();
            } while (!endturn && ! Exit);
            if (map.IntensityAt(player.posX, player.posY) == 0)
                Exit = true;
        }

        public bool HandleKeyPress(TCODKey key)
        {
            bool endTurn = false;
            #region switch (key.KeyCode)
            switch (key.KeyCode)
            {
                case TCODKeyCode.KeypadZero:
                    break;
                case TCODKeyCode.KeypadOne:
                    player.Move(-1, 1);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadTwo:
                    player.Move(0, 1);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadThree:
                    player.Move(1, 1);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadFour:
                    player.Move(-1, 0);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadFive:
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadSix:
                    player.Move(1, 0);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadSeven:
                    player.Move(-1, -1);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadEight:
                    player.Move(0, -1);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadNine:
                    player.Move(1, -1);
                    endTurn = true;
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
                    endTurn = true;
                    break;
                case TCODKeyCode.Left:
                    player.Move(-1, 0);
                    endTurn = true;
                    break;
                case TCODKeyCode.Right:
                    player.Move(1, 0);
                    endTurn = true;
                    break;
                case TCODKeyCode.Up:
                    player.Move(0, -1);
                    endTurn = true;
                    break;
                case TCODKeyCode.Down:
                    player.Move(0, 1);
                    endTurn = true;
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
                    endTurn = true;
                    break;
                case 'j':
                    player.Move(0, 1);
                    endTurn = true;
                    break;
                case 'k':
                    player.Move(0, -1);
                    endTurn = true;
                    break;
                case 'l':
                    player.Move(1, 0);
                    endTurn = true;
                    break;
                case 'y':
                    player.Move(-1, -1);
                    endTurn = true;
                    break;
                case 'u':
                    player.Move(1, -1);
                    endTurn = true;
                    break;
                case 'b':
                    player.Move(-1, 1);
                    endTurn = true;
                    break;
                case 'n':
                    player.Move(1, 1);
                    endTurn = true;
                    break;
                case '.':
                    endTurn = true;
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
                        i.Drop(player.posX, player.posY, map);
                    }
                    break;
                case 'g':
                    foreach (Item item in map.GetItemsAt(player.posX, player.posY))
                    {
                        item.Get();
                        player.Inventory.Add(item);
                    }
                    endTurn = true;
                    break;
                case 'i':
                    player.Inventory.Draw(root);
                    TCODConsole.flush();
                    TCODConsole.waitForKeypress(true);
                    break;
                case 'e':
                    player.Inventory.Draw(root);
                    TCODConsole.flush();
                    key = TCODConsole.waitForKeypress(true);
                    i = player.Inventory.GetAtLetter(key.Character);
                    if (i != null)
                    {
                        player.Equip(i);
                    }
                    endTurn = true;
                    break;
                case 'r':
                    player.Inventory.Draw(root);
                    TCODConsole.flush();
                    key = TCODConsole.waitForKeypress(true);
                    i = player.Inventory.GetAtLetter(key.Character);
                    if (i != null)
                    {
                        i.Use();
                    }
                    endTurn = true;
                    break;
                case 'w':
                    ShowWall = !ShowWall;
                    break;
                case 'a':
                    MonsterAI = !MonsterAI;
                    break;
                case 'z':
                    MonsterDamage = !MonsterDamage;
                    break;
                default:
                    break;
            }
            #endregion
            return endTurn;
        }
    }
}
