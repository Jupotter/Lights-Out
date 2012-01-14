using libtcod;

namespace Lights_Out
{
    public class Creature
    {
        public int posX;
        public int posY;
        char tile;
        TCODColor color;
        public Map currentMap;

        public Creature(char tile, TCODColor color)
        {
            this.tile = tile;
            this.color = color;
            //PlaceAt(map.StartPosX, map.StartPosY);
        }

        public virtual bool PlaceAt(int x, int y, Map map)
        {
            if (x >= 0 && x < Map.MAP_WIDTH
                && y >= 0 && y < Map.MAP_HEIGHT)
            {
                if (map != currentMap)
                {
                    currentMap = map;
                }
                posX = x;
                posY = y;
                return true;
            }
            return false;
        }

        public virtual bool Move(int dX, int dY)
        {
            int newX = posX + dX;
            int newY = posY + dY;
            if (!currentMap[newX, newY])
            {
                posX = newX;
                posY = newY;
                return true;
            }
            return false;
        }

        public void Draw(TCODConsole cons)
        {
            cons.putChar(posX, posY, (int)tile);
            cons.setCharForeground(posX, posY, color);
        }
    }
}
