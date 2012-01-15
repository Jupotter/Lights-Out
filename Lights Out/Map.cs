using System.Collections.Generic;
using libtcod;

namespace Lights_Out
{
    public class Map
    {
        public const int MAP_WIDTH  = 80;
        public const int MAP_HEIGHT = 50;

        Game game;
        Stairs stair;
        public int maxMonster;
        int currentMonster = 0;

        public Stairs Stair
        {
            get { return stair; }
            set { stair = value; }
        }

        TCODMap tcodmap;
        Dijkstra dijkstra;
        public int StartPosX;
        public int StartPosY;

        List<Monster> dead;
        List<Monster> monsters;
        List<Light> lights;
        List<Light> deadLights;
        List<Item> items;
        public Player Player;

        public TCODMap TCODMap
        { get { return tcodmap; } }
        public Dijkstra Dijkstra
        { get { return dijkstra; } }


        public Map(Game game)
        {
            this.game = game;

            maxMonster = 10;
            tcodmap = new TCODMap(MAP_WIDTH, MAP_HEIGHT);
            lights = new List<Light>();
            monsters = new List<Monster>();
            items = new List<Item>();
            dead = new List<Monster>();
            deadLights = new List<Light>();
            dijkstra = new Dijkstra(this);

            for (int i = 0; i < MAP_WIDTH; i++)
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    this[i, j] = true;
                }
        }

        public void Draw(TCODConsole cons)
        {
            cons.setForegroundColor(TCODColor.sepia);
            for (int i = 0; i < MAP_WIDTH; i++)
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    cons.putChar(i, j, this[i, j] ? '#' : '.');

                    foreach (Item item in items.FindAll(item => item.PosX == i && item.PosY == j))
                    {
                        item.Draw(cons);
                    }

                    foreach (Monster mons in monsters.FindAll(item => item.posX == i && item.posY == j))
                    {
                        mons.Draw(cons);
                    }

                }

            stair.Draw(cons);
            Player.Draw(cons);

            for (int i = 0; i < MAP_WIDTH; i++)
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    int intens = IntensityAt(i, j);

                    TCODColor color = cons.getCharForeground(i, j);
                    float value = (float)intens / 20 + (Game.ShowWall ? 0.05f : 0f);
                    color.setValue(System.Math.Min(value, 1f));
                    cons.setCharForeground(i, j, color);
                }
        }

        public int IntensityAt(int X, int Y)
        {
            int intens = 0;
            foreach (Light light in lights)
            {
                int t = light.IntensityAt(X, Y);
                intens = System.Math.Max(intens, t);
            }
            int pl = Player.Light.IntensityAt(X, Y);
            intens = System.Math.Max(intens, pl);
            return intens;
        }

        public void Update()
        {
            dead.Clear();
            dijkstra.Clear();
            foreach (Light light in lights)
            {
                dijkstra.AddStartPos(light.PosX, light.PosY, 20 - light.IntensityAt(light.PosX, light.PosY));
            }
            dijkstra.AddStartPos(Player.Light.PosX, Player.Light.PosY, 20 - Player.Light.IntensityAt(Player.Light.PosX, Player.Light.PosY));

            dijkstra.ComputeDijkstra();

            foreach (Monster mons in monsters)
            {
                mons.Act();
                int intens = IntensityAt(mons.posX, mons.posY);
                if (intens > 0)
                    mons.TakeDamage(intens);
            }
            foreach (Light light in lights)
            {
                light.Update();
            }
            Player.Light.Update();
            foreach (Monster mons in dead)
            {
                monsters.Remove(mons);
                game.AddMonster(this);
            }
            foreach (Light light in deadLights)
            {
                lights.Remove(light);
            }
        }

        public bool SetStartPos(int X, int Y)
        {
            if (X >= 0 && X < Map.MAP_WIDTH
                && Y >= 0 && Y < Map.MAP_HEIGHT)
            {
                StartPosX = X;
                StartPosY = Y;
                return true;
            }
            return false;
        }

        public Monster ContainMonster(int X, int Y)
        {
            return monsters.Find(m => m.posX == X && m.posY == Y);
        }

        public Light ContainLight(int X, int Y)
        {
            return lights.Find(m => m.PosX == X && m.PosY == Y);
        }

        public void AddLight(Light light)
        {
            lights.Add(light);
        }

        public void RemoveLight(Light light)
        {
            light.RemoveFromMap();
            deadLights.Add(light);
        }

        public void RemoveLighstAt(int X, int Y)
        {
            deadLights.AddRange(lights.FindAll(l => l.PosX == X && l.PosY == Y));
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public void RemoveItemsAt(int X, int Y)
        {
            items.RemoveAll(i => i.PosX == X && i.PosY == Y);
        }

        public List<Item> GetItemsAt(int X, int Y)
        {
            return items.FindAll(i => i.PosX == X && i.PosY == Y);
        }

        public void AddCreature(Monster monster)
        {
            if (currentMonster <= maxMonster)
            {
                currentMonster += 1;
                monsters.Add(monster);
            }
        }

        public void RemoveCreature(Monster monster)
        {
            dead.Add(monster);
            currentMonster -= 1;
        }

        public bool this[int x, int y]
        {
            get { return !tcodmap.isWalkable(x, y); ; }
            set { tcodmap.setProperties(x, y, !value, !value); }
        }
    }
}
