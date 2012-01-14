using System;
using libtcod;

namespace Lights_Out
{
    public enum ItemState
    { Held, Dropped }

    public class Item
    {
        public ItemState State;
        bool isLight;
        public bool IsLight
        { get { return isLight; } }
        bool lightOn;
        public bool LightOn
        { get { return lightOn; } }
        
        Light light;
        public Light Light
        { get { return light; } }

        protected Map map;

        char tile;
        int posX, posY;
        TCODColor color;

        public int PosX
        { get { return posX; } }
        public int PosY
        { get { return posY; } }
        public char Tile
        { get { return tile; } }
        public TCODColor Color
        {
            get { return color == null ? TCODColor.white : color; }
            set { color = value; }
        }

        string name;
        public string Name
        { get { return name; } }
        bool stack;
        public bool Stack
        { get { return stack; } }

        public Item(string name, char tile,bool stack)
        {
            this.name = name;
            this.tile = tile;
            this.stack = stack;
            isLight = false;
            lightOn = false;
        }

        public void SetMap(Map map)
        {
            this.map = map;
        }

        public void SetLight(Light light)
        {
            this.light = light;
            isLight = true;
            lightOn = true;
        }

        public void SwitchLight(bool on)
        {
            if (this.isLight)
            {
                lightOn = on;
            }
        }

        public bool Drop(int x, int y, Map map)
        {
            if (x >= 0 && x < Map.MAP_WIDTH
                && y >= 0 && y < Map.MAP_HEIGHT)
            {
                posX = x;
                posY = y;
                this.map = map;
                this.State = ItemState.Dropped;
                map.AddItem(this);
                if (isLight && lightOn)
                    light.PlaceAt(x, y, map);
                return true;
            }
            return false;
        }

        public void Get()
        {
            this.State = ItemState.Held;
            map.RemoveItem(this);
            if (isLight)
                map.RemoveLight(light);
        }

        public void Draw(TCODConsole cons)
        {
            cons.putChar(posX, posY, (int)tile);
            cons.setCharForeground(posX, posY, Color);
        }

        public override string ToString()
        {
            string plus = "";
            if (isLight)
                plus = String.Format(" ({0})", light.ToString());
            return String.Format("{0}{1}", name, plus);
        }

        public virtual void Use()
        {
            return;
        }
    }
}
