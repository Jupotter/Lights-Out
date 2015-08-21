using System.Collections.Generic;
using System.Linq;
using libtcod;

namespace Lights_Out
{
    public class Map
    {
        public const int MAP_WIDTH  = 80;
        public const int MAP_HEIGHT = 50;

        readonly Game _game;
        Stairs _stair;
        public int MaxMonster;

        private int _currentMonsterNum;

        public Stairs Stair
        {
            get { return _stair; }
            set { _stair = value; }
        }

        readonly bool[,] _known;
        public int StartPosX;
        public int StartPosY;

        readonly List<Monster> _dead;
        readonly List<Monster> _monsters;
        readonly List<Light> _lights;
        readonly List<Light> _deadLights;
        readonly List<Item> _items;
        public Player Player;

        public TCODMap TCODMap { get; private set; }

        public Dijkstra Dijkstra { get; private set; }

        public int CurrentMonsterNum
        {
            get { return _currentMonsterNum; }
        }


        public Map(Game game)
        {
            _currentMonsterNum = 0;
            _game = game;

            MaxMonster = 10;
            TCODMap = new TCODMap(MAP_WIDTH, MAP_HEIGHT);
            _known = new bool[MAP_WIDTH, MAP_HEIGHT];
            _lights = new List<Light>();
            _monsters = new List<Monster>();
            _items = new List<Item>();
            _dead = new List<Monster>();
            _deadLights = new List<Light>();
            Dijkstra = new Dijkstra(this);

            for (int i = 0; i < MAP_WIDTH; i++)
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    this[i, j] = true;
                }
        }

        public void Draw(TCODConsole cons)
        {
            cons.setForegroundColor(TCODColor.white);
            for (int i = 0; i < MAP_WIDTH; i++)
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    cons.putChar(i, j, this[i, j] ? '#' : '.');
                }

            _stair.Draw(cons);
            cons.putChar(StartPosX, StartPosY, '<');

            foreach (Item item in _items)
            {
                item.Draw(cons);
            }

            foreach (Monster mons in _monsters)
            {
                mons.Draw(cons);
            }

            Player.Draw(cons);

            for (int i = 0; i < MAP_WIDTH; i++)
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    /*int intens;
                    Light light = LightAt(i, j);
                    
                    if (light == null)
                        intens = 0;
                    else
                    {
                        intens = light.IntensityAt(i, j);
                        color = color.Multiply(light.Color);
                    }
                    float value = (float)intens / 20 + (Game.ShowWall ? 0.05f : 0f);
                    color.setValue(System.Math.Min(value, 1f));//*/

                    TCODColor color = cons.getCharForeground(i, j);
                    TCODColor newCol = ColorAt(i, j);

                    if (newCol.NotEqual(TCODColor.black))
                        _known[i, j] = true;
                    color = color.Multiply(newCol);

                    cons.setCharForeground(i, j, color);
                }
        }

        public Light LightAt(int x, int y)
        {
            Light max = null;
            int intens = 0;
            int t;
            foreach (Light light in _lights)
            {
                t = light.IntensityAt(x, y);
                if (t > intens)
                {
                    intens = t;
                    max = light;
                }
            }
            t = Player.Light.IntensityAt(x, y);
            if (t > intens)
            {
                max = Player.Light;
            }
            return max;
        }

        public TCODColor ColorAt(int x, int y)
        {
            TCODColor col = TCODColor.black;
            int intens = 0;
            int t;
            foreach (Light light in _lights)
            {
                t = light.IntensityAt(x, y);
                if (t > intens)
                {
                    intens = t;
                }
                col = col.Plus(light.Color.Multiply((float)t/20));
            }
            t = Player.Light.IntensityAt(x, y);
            if (t > intens)
            {
                intens = t;
                col = col.Plus(Player.Light.Color);
            }
            col.setValue((float)intens / 20 + (Game.ShowWall||_known[x,y] ? 0.05f : 0f));
            return col;
        }

        public int IntensityAt(int x, int y)
        {
            int intens = _lights.Select(light => light.IntensityAt(x, y)).Aggregate(0, System.Math.Max);
            int pl = Player.Light.IntensityAt(x, y);
            intens = System.Math.Max(intens, pl);
            return intens;
        }

        public void Update()
        {
            _dead.Clear();
            Dijkstra.Clear();
            foreach (Light light in _lights)
            {
                Dijkstra.AddStartPos(light.PosX, light.PosY, 40 - light.IntensityAt(light.PosX, light.PosY)*2);
            }
            Dijkstra.AddStartPos(Player.Light.PosX, Player.Light.PosY, 20 - Player.Light.IntensityAt(Player.Light.PosX, Player.Light.PosY));

            Dijkstra.ComputeDijkstra();

            foreach (Monster mons in _monsters)
            {
                mons.Act();
                int intens = IntensityAt(mons.PosX, mons.PosY);
                if (intens > 0)
                    mons.TakeDamage(intens);
            }
            foreach (Light light in _lights)
            {
                light.Update();
            }
            Player.Light.Update();
            foreach (Monster mons in _dead)
            {
                _monsters.Remove(mons);
                _game.AddMonster(this);
            }
            foreach (Light light in _deadLights)
            {
                _lights.Remove(light);
            }
        }

        public bool SetStartPos(int x, int y)
        {
            if (x >= 0 && x < MAP_WIDTH
                && y >= 0 && y < MAP_HEIGHT)
            {
                StartPosX = x;
                StartPosY = y;
                return true;
            }
            return false;
        }

        public Monster ContainMonster(int x, int y)
        {
            return _monsters.Find(m => m.PosX == x && m.PosY == y);
        }

        public Light ContainLight(int x, int y)
        {
            return _lights.Find(m => m.PosX == x && m.PosY == y);
        }

        public void AddLight(Light light)
        {
            _lights.Add(light);
        }

        public void RemoveLight(Light light)
        {
            light.RemoveFromMap();
            _deadLights.Add(light);
        }

        public void RemoveLighstAt(int x, int y)
        {
            _deadLights.AddRange(_lights.FindAll(l => l.PosX == x && l.PosY == y));
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        public void RemoveItemsAt(int x, int y)
        {
            _items.RemoveAll(i => i.PosX == x && i.PosY == y);
        }

        public List<Item> GetItemsAt(int x, int y)
        {
            return _items.FindAll(i => i.PosX == x && i.PosY == y);
        }

        public void AddCreature(Monster monster)
        {
            if (CurrentMonsterNum <= MaxMonster)
            {
                _currentMonsterNum = CurrentMonsterNum + 1;
                _monsters.Add(monster);
            }
        }

        public void RemoveCreature(Monster monster)
        {
            _dead.Add(monster);
            _currentMonsterNum = CurrentMonsterNum - 1;
        }

        public bool this[int x, int y]
        {
            get { return !TCODMap.isWalkable(x, y); }
            set { TCODMap.setProperties(x, y, !value, !value); }
        }
    }
}
