using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    public class Light
    {
        public static readonly TCODColor LIGHT_COLOR;

        static Light()
        {
            LIGHT_COLOR = TCODColor.amber;
        }

        int posX, posY;
        int intensity;
        Map currentMap;
        TCODMap tcodmap;

        DistanceFunc distanceFunc;

        public int PosX
        { get { return posX; } }

        public int PosY
        { get { return posY; } }

        public Light(int intensity)
        {
            this.intensity = intensity;
            distanceFunc = Math.Distance_Euclide;
        }

        public int IntensityAt(int x, int y)
        {
            if (tcodmap.isInFov(x, y))
            {
                int dist = distanceFunc(x, y, posX, posY);
                int ret = intensity - (dist*2);
                return (ret >= 0 ? ret : 0);
            }
            return 0;
        }

        public bool PlaceAt(int x, int y, Map map)
        {
            if (x >= 0 && x < Map.MAP_WIDTH
                && y >= 0 && y < Map.MAP_HEIGHT)
            {
                if (map != currentMap)
                {
                    currentMap = map;
                    map.AddLight(this);
                    tcodmap = new TCODMap(Map.MAP_WIDTH, Map.MAP_HEIGHT);
                    tcodmap.copy(map.TCODMap);
                    tcodmap.computeFov(posX, posY, intensity / 2, true);
                }
                posX = x;
                posY = y;
                tcodmap.computeFov(posX, posY, intensity/2, true);

                return true;
            }
            return false;
        }
    }
}
