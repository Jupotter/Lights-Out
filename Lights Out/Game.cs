using libtcod;

namespace Lights_Out
{
    public class Game
    {
        static public bool ShowWall = false;
        static public bool MonsterAI = true;
        static public bool MonsterDamage = true;

        Dungeon dungeon;
        Map map { get { return dungeon.CurrentLevel; } }
        Player player;
        TCODConsole root = TCODConsole.root;
        MapGen gen { get { return dungeon.Gen; } }
        Messages messages;
        TCODConsole mapConsole;

        int turnInDark = 0;

        public bool Exit = false;

        public Game()
        {
            TCODConsole msgCons = new TCODConsole(80, 10);
            messages = new Messages(msgCons);

            mapConsole = new TCODConsole(80, 50);

            dungeon = new Dungeon(this);

            dungeon.GenNewLevel();
            dungeon.CurrentDepth = 1;
            

            player = new Player(dungeon.CurrentLevel);
            map.Player = player;
            player.PlaceAt(map.StartPosX, map.StartPosY, map);

            for (int i = 0; i < 10; i++)
            {
                AddMonster(map);
            }
        }

        public void AddMonster(Map map)
        {
            int x, y;
            do
                gen.FindOpenSpot(out x, out y, map);
            while (Math.Distance_King(x, y, player.posX, player.posY) < 30);
            Monster m = new Monster('X', TCODColor.red, map, 50);
            m.PlaceAt(x, y, map);
        }

        public void Draw()
        {
            int lightLenght = (int)((1-player.Light.Used) * 80);

            root.setBackgroundColor(TCODColor.black);
            root.setForegroundColor(TCODColor.white);
            map.Draw(mapConsole);
            messages.Draw();
            TCODConsole.blit(mapConsole, 0, 0, 80, 50, root, 0, 1);
            TCODConsole.blit(messages.Console, 0, 0, 80, 10, root, 0, 51);
            root.hline(0, 0, 80, TCODBackgroundFlag.Set);
            root.setBackgroundColor(TCODColor.amber);
            root.setForegroundColor(TCODColor.amber);
            root.hline(0, 0, lightLenght, TCODBackgroundFlag.Set);
            TCODConsole.flush();
        }

        public void Update()
        {
            bool endturn;
            do
            {
                TCODKey key = TCODConsole.waitForKeypress(true);
                endturn = HandleKeyPress(key);
                Draw();
            } while (!endturn && ! Exit);
            map.Update();
            Draw();
            if (map.IntensityAt(player.posX, player.posY) == 0)
                if (turnInDark == 3)
                    Exit = true;
                else turnInDark++;
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
                        endTurn = true;
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
                        endTurn = true;
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
                        player.Inventory.RemoveAllAtLetter(key.Character);
                        endTurn = true;
                    }
                    break;
                case '>':
                    if (player.posX == map.Stair.PosX && player.posY == map.Stair.PosY)
                    {
                        dungeon.GoToMap(dungeon.CurrentDepth + 1, player);
                        for (int n = 0; n < 10; n++)
                        {
                            AddMonster(map);
                        }
                    }
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
