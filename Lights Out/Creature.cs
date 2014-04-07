using libtcod;

namespace Lights_Out
{
    public class Creature
    {
        public int PosX;
        public int PosY;
        readonly char _tile;
        readonly TCODColor _color;
        public Map CurrentMap;

        public Creature(char tile, TCODColor color)
        {
            _tile = tile;
            _color = color;
            //PlaceAt(map.StartPosX, map.StartPosY);
        }

        public virtual bool PlaceAt(int x, int y, Map map)
        {
            if (x >= 0 && x < Map.MAP_WIDTH
                && y >= 0 && y < Map.MAP_HEIGHT)
            {
                if (CurrentMap == null || map != CurrentMap)
                {
                    CurrentMap = map;
                }
                PosX = x;
                PosY = y;
                return true;
            }
            return false;
        }

        public virtual bool Move(int dX, int dY)
        {
            int newX = PosX + dX;
            int newY = PosY + dY;
            if (!CurrentMap[newX, newY])
            {
                PosX = newX;
                PosY = newY;
                return true;
            }
            return false;
        }

        public void Draw(TCODConsole cons)
        {
            cons.putChar(PosX, PosY, _tile);
            cons.setCharForeground(PosX, PosY, _color);
        }
    }
}
