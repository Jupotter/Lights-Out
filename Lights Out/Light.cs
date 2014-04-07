using System;
using libtcod;

namespace Lights_Out
{
    public class Light
    {
        readonly int _intensity;
        readonly int _radius;
        readonly int _capacity;
        readonly float _coef;
        Map _currentMap;
        TCODMap _tcodmap;
        int _elapsed;

        bool _isOnMap;

        readonly TCODColor _color;
        public TCODColor Color
        { get { return _color; } }

        readonly DistanceFunc _distanceFunc;

        public int PosX { get; private set; }

        public int PosY { get; private set; }


        public float Used
        { get { return (float)_elapsed / _capacity; } }

        public Light(int intensity, int radius, int capacity, TCODColor color)
        {
            _color = color;
            _intensity = intensity;
            _radius = radius;
            _capacity = capacity;
            _coef = (float)(-intensity) / radius;

            _elapsed = 0;
            _distanceFunc = Math.Distance_Euclide;
        }

        public Light(int intensity, int radius, int capacity)
            : this(intensity, radius, capacity, TCODColor.white)
        {
        }

        public int IntensityAt(int x, int y)
        {
            if (!_tcodmap.isInFov(x, y))
                return 0;
            int dist = _distanceFunc(x, y, PosX, PosY);
            float mult = 1f;
            if (_capacity > 0)
                mult = 1f - (float)System.Math.Pow((float)_elapsed / _capacity, 2);
            int ret = (int)((_intensity + (int)(dist * _coef)) * mult);
            return (ret >= 0 ? ret : 0);
        }

        public bool PlaceAt(int x, int y, Map map)
        {
            if (x >= 0 && x < Map.MAP_WIDTH
                && y >= 0 && y < Map.MAP_HEIGHT)
            {
                if (!_isOnMap || map != _currentMap)
                {
                    if (_currentMap != null)
                        _currentMap.RemoveLight(this);
                    _currentMap = map;
                    map.AddLight(this);
                    _tcodmap = new TCODMap(Map.MAP_WIDTH, Map.MAP_HEIGHT);
                    _tcodmap.copy(map.TCODMap);
                }
                PosX = x;
                PosY = y;
                _tcodmap.computeFov(PosX, PosY, _radius, true);
                _isOnMap = true;

                return true;
            }
            return false;
        }

        public void Update()
        {
            if (_capacity >= 0 && !Game.InfiniteTorch)
            {
                _elapsed++;
                if (_elapsed >= _capacity)
                    _currentMap.RemoveLight(this);
            }
        }


        public void Use(int num)
        {
            _elapsed = System.Math.Min(_elapsed + num, _capacity);
        }

        public override string ToString()
        {
            return String.Format("{0}:{1}:{2}/{3}", _intensity, _radius, _elapsed, _capacity);
        }

        internal void RemoveFromMap()
        {
            _isOnMap = false;
        }
    }
}
