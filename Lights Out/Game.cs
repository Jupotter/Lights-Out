using libtcod;

namespace Lights_Out
{
    public class Game
    {
 // ReSharper disable InconsistentNaming
        static public bool ShowWall = true;
        static public bool MonsterAI = true;
        static public bool MonsterDamage = true;
        static public bool InfiniteTorch = true;
// ReSharper restore InconsistentNaming

        readonly Dungeon _dungeon;
        Map Map { get { return _dungeon.CurrentLevel; } }
        readonly Player _player;
        readonly TCODConsole _root = TCODConsole.root;
        MapGen Gen { get { return _dungeon.Gen; } }
        readonly Messages _messages;
        readonly TCODConsole _mapConsole;

        int _turnInDark;

        public bool Exit = false;

        public Game()
        {
            _turnInDark = 0;
            TCODConsole msgCons = new TCODConsole(80, 10);
            _messages = new Messages(msgCons);

            _mapConsole = new TCODConsole(80, 50);

            _dungeon = new Dungeon(this);

            _dungeon.GenNewLevel();
            _dungeon.CurrentDepth = 1;
            

            _player = new Player(_dungeon.CurrentLevel);
            Map.Player = _player;
            _player.PlaceAt(Map.StartPosX, Map.StartPosY, Map);

            for (int i = 0; i < 10; i++)
            {
                AddMonster(Map);
            }
        }

        public void AddMonster(Map map)
        {
            int x, y;
            do
                Gen.FindOpenSpot(out x, out y, map);
            while (Math.Distance_King(x, y, _player.PosX, _player.PosY) < 30);
            Monster m = new Monster('X', TCODColor.red, map, 50);
            m.PlaceAt(x, y, map);
        }

        public void Draw()
        {
            int lightLenght = (int)((1-_player.Light.Used) * 80);

            _root.setBackgroundColor(TCODColor.black);
            _root.setForegroundColor(TCODColor.white);
            Map.Draw(_mapConsole);
            _messages.Draw();
            TCODConsole.blit(_mapConsole, 0, 0, 80, 50, _root, 0, 1);
            TCODConsole.blit(_messages.Console, 0, 0, 80, 10, _root, 0, 51);
            _root.hline(0, 0, 80, TCODBackgroundFlag.Set);
            _root.setBackgroundColor(TCODColor.amber);
            _root.setForegroundColor(TCODColor.amber);
            _root.hline(0, 0, lightLenght, TCODBackgroundFlag.Set);
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
            if (!Exit)
            {
                Map.Update();
                Draw();
                if (Map.IntensityAt(_player.PosX, _player.PosY) == 0)
                    if (_turnInDark == 3)
                        Exit = true;
                    else _turnInDark++;
            }
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
                    _player.Move(-1, 1);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadTwo:
                    _player.Move(0, 1);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadThree:
                    _player.Move(1, 1);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadFour:
                    _player.Move(-1, 0);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadFive:
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadSix:
                    _player.Move(1, 0);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadSeven:
                    _player.Move(-1, -1);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadEight:
                    _player.Move(0, -1);
                    endTurn = true;
                    break;
                case TCODKeyCode.KeypadNine:
                    _player.Move(1, -1);
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
                    _player.Move(-1, 0);
                    endTurn = true;
                    break;
                case TCODKeyCode.Right:
                    _player.Move(1, 0);
                    endTurn = true;
                    break;
                case TCODKeyCode.Up:
                    _player.Move(0, -1);
                    endTurn = true;
                    break;
                case TCODKeyCode.Down:
                    _player.Move(0, 1);
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
            }
            #endregion

            #region switch (key.Character)
            switch (key.Character)
            {
                #region Movement
                case 'h':
                    _player.Move(-1, 0);
                    endTurn = true;
                    break;
                case 'j':
                    _player.Move(0, 1);
                    endTurn = true;
                    break;
                case 'k':
                    _player.Move(0, -1);
                    endTurn = true;
                    break;
                case 'l':
                    _player.Move(1, 0);
                    endTurn = true;
                    break;
                case 'y':
                    _player.Move(-1, -1);
                    endTurn = true;
                    break;
                case 'u':
                    _player.Move(1, -1);
                    endTurn = true;
                    break;
                case 'b':
                    _player.Move(-1, 1);
                    endTurn = true;
                    break;
                case 'n':
                    _player.Move(1, 1);
                    endTurn = true;
                    break;
                case '.':
                    endTurn = true;
                    break;
                #endregion
                case 'd':
                    _player.Inventory.Draw(_root);
                    TCODConsole.flush();
                    key = TCODConsole.waitForKeypress(true);
                    Item i = _player.Inventory.GetAtLetter(key.Character);
                    _player.Inventory.RemoveAtLetter(key.Character, 1);
                    if (i != null)
                    {
                        i.Drop(_player.PosX, _player.PosY, Map);
                    }
                    break;
                case 'g':
                    foreach (Item item in Map.GetItemsAt(_player.PosX, _player.PosY))
                    {
                        item.Get();
                        _player.Inventory.Add(item);
                    }
                    endTurn = true;
                    break;
                case 'i':
                    _player.Inventory.Draw(_root);
                    TCODConsole.flush();
                    TCODConsole.waitForKeypress(true);
                    break;
                case 'e':
                    _player.Inventory.Draw(_root);
                    TCODConsole.flush();
                    key = TCODConsole.waitForKeypress(true);
                    i = _player.Inventory.GetAtLetter(key.Character);
                    if (i != null)
                    {
                        _player.Equip(i);
                    }
                    endTurn = true;
                    break;
                case 'r':
                    _player.Inventory.Draw(_root);
                    TCODConsole.flush();
                    key = TCODConsole.waitForKeypress(true);
                    i = _player.Inventory.GetAtLetter(key.Character);
                    if (i != null)
                    {
                        i.Use();
                        _player.Inventory.RemoveAllAtLetter(key.Character);
                        endTurn = true;
                    }
                    break;
                case '>':
                    if (_player.PosX == Map.Stair.PosX && _player.PosY == Map.Stair.PosY)
                    {
                        _dungeon.GoToMap(_dungeon.CurrentDepth + 1, _player);
                        for (int n = Map.CurrentMonsterNum; n < Map.MaxMonster; n++)
                        {
                            AddMonster(Map);
                        }
                    }
                    break;
                case '<':
                    if (_player.PosX == Map.StartPosX && _player.PosY == Map.StartPosY)
                    {
                        if (_dungeon.CurrentDepth != 1)
                            _dungeon.GoToMap(_dungeon.CurrentDepth - 1, _player);
                        else
                        {
                            Messages.AddMessage("If you go up, you'll exit the Dungeon. Are you sure?");
                            key = TCODConsole.waitForKeypress(true);
                            if (key.Character == 'Y')
                                Exit = true;
                            else if (key.Character != 'N')
                                Messages.AddMessage("Y or N only");
                        }
                    }
                    break;
                #region Debug
                case 'w':
                    ShowWall = !ShowWall;
                    break;
                case 'a':
                    MonsterAI = !MonsterAI;
                    break;
                case 'z':
                    MonsterDamage = !MonsterDamage;
                    break;
                case 'q':
                    InfiniteTorch = !InfiniteTorch;
                    break;

                    #endregion
            }
            #endregion
            return endTurn;
        }
    }
}
