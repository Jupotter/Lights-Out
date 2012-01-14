using System;
using libtcod;

namespace Lights_Out
{
    public class Light
    {
        int posX, posY;
        int intensity;
        int radius;
        int capacity;
        float coef;
        Map currentMap;
        TCODMap tcodmap;
        int elapsed;

        TCODColor color;
        public TCODColor Color
        { get { return color; } }

        DistanceFunc distanceFunc;

        public int PosX
        { get { return posX; } }

        public int PosY
        { get { return posY; } }

        public Light(int intensity, int radius, int capacity, TCODColor color)
        {
            this.color = color;
            this.intensity = intensity;
            this.radius = radius;
            this.capacity = capacity;
            this.coef = (float)(-intensity) / radius;

            elapsed = 0;
            distanceFunc = Math.Distance_Euclide;
        }

        public Light(int intensity, int radius, int capacity)
            : this(intensity, radius, capacity, TCODColor.white)
        {
        }

        public int IntensityAt(int x, int y)
        {
            if (tcodmap.isInFov(x, y))
            {
                int dist = distanceFunc(x, y, posX, posY);
                float mult = 1f - (float)elapsed / capacity;
                int ret = (int)((intensity + (int)(dist * coef))*mult);
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
                    if (currentMap != null)
                        currentMap.RemoveLight(this);
                    currentMap = map;
                    map.AddLight(this);
                    tcodmap = new TCODMap(Map.MAP_WIDTH, Map.MAP_HEIGHT);
                    tcodmap.copy(map.TCODMap);
                }
                posX = x;
                posY = y;
                tcodmap.computeFov(posX, posY, radius, true);

                return true;
            }
            return false;
        }

        public void Update()
        {
            if (capacity >= 0)
            {
                elapsed = System.Math.Min(elapsed + 1, capacity);
                if (elapsed >= capacity)
                    currentMap.RemoveLight(this);
            }
            
        }

        public void Use(int num)
        {
            elapsed = System.Math.Min(elapsed + num, capacity);
        }

        public override string ToString()
        {
            return String.Format("{0}:{1}:{2}/{3}", intensity, radius, elapsed, capacity);
        }
    }
}
