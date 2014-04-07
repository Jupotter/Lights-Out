using libtcod;

namespace Lights_Out
{
    public class MapGen
    {
        readonly TCODRandom _rand;

        public MapGen(uint seed)
        {
            _rand = new TCODRandom(seed);
        }

        public MapGen()
        {
            _rand = new TCODRandom();
        }

        public void Generate(int level, out Map map, Game game)
        {
            map = new Map(game);
            TCODBsp root = new TCODBsp(1, 1, Map.MAP_WIDTH - 2, Map.MAP_HEIGHT - 2);
            root.splitRecursive(_rand, level, 3, 3, 1.5f, 1.5f);
            //map = new Map(80, 50, new TCODConsole(5, 5));
            GenRoom(root, ref map);
            int x;
            int y;
            FindOpenSpot(out x, out y, map);
            map.SetStartPos(x, y);
            FindOpenSpot(out x, out y, map);
            map.Stair = new Stairs(x, y);
            for (int i = 0; i < 20; i++)
            {
                PlaceRandomItem(map);
            }
        }

        public void PlaceRandomItem(Map map)
        {
            int x, y;
            int n = _rand.getInt(0, ItemsData.TotalWeight);
            FindOpenSpot(out x, out y, map);
            Item item = ItemsData.PickWeightedItem(n);
            n = _rand.getInt(0, 10);
            item.SwitchLight(n == 0);
            item.Drop(x, y, map);
        }

        public void FindOpenSpot(out int x, out int y, Map map)
        {
            do
            {
                x = _rand.getInt(1, Map.MAP_WIDTH - 2);
                y = _rand.getInt(1, Map.MAP_HEIGHT - 2);
            } while (map[x, y]);

        }

        private void GenRoom(TCODBsp bsp, ref Map map)
        {
            if (bsp.isLeaf())
            {
                float propX1 = _rand.getInt(0, 33);
                float propX2 = _rand.getInt(0, 33);
                float propY1 = _rand.getInt(0, 33);
                float propY2 = _rand.getInt(0, 33);

                int x = bsp.x + (int)(propX1 / 100f * bsp.w);
                int w = bsp.w - (x - bsp.x) - (int)(propX2 / 100f * bsp.w);
                int y = bsp.y + (int)(propY1 / 100f * bsp.h);
                int h = bsp.h - (y - bsp.y) - (int)(propY2 / 100f * bsp.h);

                for (int i = x; i < x + w; i++)
                    for (int j = y; j < y + h; j++)
                    {
                        map[i, j] = false;
                    }
            }
            else
            {
                TCODBsp left = bsp.getLeft();
                TCODBsp right = bsp.getRight();

                GenRoom(left, ref map);
                GenRoom(right, ref map);

                if (bsp.horizontal)
                {
                    int midx = (left.x + left.x + left.w) / 2;
                    int midL = (left.y + left.y + left.h) / 2;
                    int midR = (right.y + right.y + right.h) / 2;
                    for (int y = midL; y < midR; y++)
                    {
                        map[midx, y] = false;
                    }
                }
                else
                {
                    int midy = (left.y + left.y + left.h) / 2;
                    int midL = (left.x + left.x + left.w) / 2;
                    int midR = (right.x + right.x + right.w) / 2;
                    for (int x = midL; x < midR; x++)
                    {
                        map[x, midy] = false;
                    }
                }
            }
        }
    }
}

