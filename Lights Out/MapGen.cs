using System;
using libtcod;

namespace Lights_Out
{
	public class MapGen
	{
		TCODRandom rand;
		
		public MapGen(uint seed)
		{
			rand = new TCODRandom(seed);
		}
		
		public MapGen()
		{
			rand = new TCODRandom();
		}

        public void Generate(int level, out Map map)
        {
            map = new Map();
            TCODBsp root = new TCODBsp(1, 1, Map.MAP_WIDTH - 2, Map.MAP_HEIGHT - 2);
            root.splitRecursive(rand, level, 3, 3, 1.5f, 1.5f);
            //map = new Map(80, 50, new TCODConsole(5, 5));
            GenRoom(root, ref map);
            int x;
            int y;
            FindOpenSpot(out x, out y, map);
            map.SetStartPos(x, y);
        }
		
        public void FindOpenSpot(out int x, out int y, Map map)
        {
            do
            {
                x = rand.getInt(1, Map.MAP_WIDTH - 2);
                y = rand.getInt(1, Map.MAP_HEIGHT - 2);
            } while (map[x, y]);
            
        }

		private void GenRoom(TCODBsp bsp, ref Map map)
		{
			if (bsp.isLeaf())
			{
				/*int x = rand.getInt(bsp.x, bsp.x + bsp.w);
				int y = rand.getInt(bsp.y, bsp.y + bsp.h);
				int w = rand.getInt(x, bsp.x + bsp.w);
				int h = rand.getInt(y, bsp.y + bsp.h);
				//*/
				/*
				int x = bsp.x+rand.getInt(0, bsp.w-1);
				int y = bsp.y+rand.getInt(0, bsp.h-1);
				int w = bsp.w-(x-bsp.x+rand.getInt(0, bsp.w-x+bsp.x));
				int h = bsp.h-(y-bsp.y+rand.getInt(0,bsp.h -x+bsp.x));
				//*/
				/*
				int x = bsp.x + 1;
				int y = bsp.y + 1;
				int w = bsp.w-1;
				int h = bsp.h-1;
				//*/
				float propX1 = (float)rand.getInt(0, 33);
				float propX2 = (float)rand.getInt(0, 33);
				float propY1 = (float)rand.getInt(0, 33);
				float propY2 = (float)rand.getInt(0, 33);
				
				int x = bsp.x + (int)(propX1/100f*(float)bsp.w);
				int w = bsp.w - (x - bsp.x) - (int)(propX2/100f * (float)bsp.w);
				int y = bsp.y + (int)(propY1/100f*(float)bsp.h);
				int h = bsp.h - (y - bsp.y) - (int)(propY2 / 100f * (float)bsp.h);
				
				for (int i = x; i < x + w; i++)
					for (int j = y; j < y + h; j++)
					{
                        map[i, j] = false;
					}
				
				return;
			
			} 
			else
			{
				TCODBsp left = bsp.getLeft();
				TCODBsp right = bsp.getRight();
				
				GenRoom(left, ref map);
				GenRoom(right, ref map);
				
				if (bsp.horizontal)
				{
					int midx = (left.x + left.x + left.w)/2;
					int midL = (left.y+left.y+left.h)/2;
					int midR = (right.y+right.y+right.h)/2;
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
				}//*/
				return;
			}
		}
	}
}

