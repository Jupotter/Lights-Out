using System;
using libtcod;

namespace Lights_Out
{
    public enum ItemState
    { Held, Dropped }

    public class Item
    {
        public ItemState State;
        public bool IsLight { get; protected set; }

        public bool LightOn { get; private set; }

        public Light Light { get; private set; }

        protected Map Map;

        readonly char _tile;
        TCODColor _color;

        public int PosX { get; private set; }

        public int PosY { get; private set; }

        public char Tile
        { get { return _tile; } }
        public TCODColor Color
        {
            get { return _color ?? TCODColor.white; }
            set { _color = value; }
        }

        readonly string _name;
        public string Name
        { get { return _name; } }

        readonly bool _stack;
        public bool Stack
        { get { return _stack; } }

        public Item(string name, char tile,bool stack)
        {
            _name = name;
            _tile = tile;
            _stack = stack;
            IsLight = false;
            LightOn = false;
        }

        public void SetMap(Map map)
        {
            Map = map;
        }

        public void SetLight(Light light)
        {
            Light = light;
            IsLight = true;
            LightOn = true;
        }

        public virtual void SwitchLight(bool on)
        {
            if (IsLight)
            {
                LightOn = on;
            }
        }

        public virtual bool Drop(int x, int y, Map map)
        {
            if (x >= 0 && x < Map.MAP_WIDTH
                && y >= 0 && y < Map.MAP_HEIGHT)
            {
                PosX = x;
                PosY = y;
                Map = map;
                State = ItemState.Dropped;
                map.AddItem(this);
                if (IsLight && LightOn)
                    Light.PlaceAt(x, y, map);
                return true;
            }
            return false;
        }

        public virtual void Get()
        {
            State = ItemState.Held;
            Map.RemoveItem(this);
            if (IsLight)
                Map.RemoveLight(Light);
        }

        public void Draw(TCODConsole cons)
        {
            cons.putChar(PosX, PosY, _tile);
            cons.setCharForeground(PosX, PosY, Color);
        }

        public override string ToString()
        {
            string plus = "";
            if (IsLight)
                plus = String.Format(" ({0})", Light);
            return String.Format("{0}{1}", _name, plus);
        }

        public virtual void Use()
        {
        }
    }
}
