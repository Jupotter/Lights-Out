using System.Collections.Generic;

namespace Lights_Out
{
    class Dungeon
    {
        const int MAX_DEPTH = 10;

        readonly Game _game;

        readonly List<Map> _mapList;
        int _depth;
        int _currentDepth;
        public int CurrentDepth
        {
            get { return _currentDepth; }
            set { if (value <= _depth) _currentDepth = value; }
        }
        public Map CurrentLevel
        { get { return _mapList[_currentDepth - 1]; } }

        readonly MapGen _gen;

        public MapGen Gen
        {
            get { return _gen; }
        }

        public Dungeon(Game game)
        {
            _gen = new MapGen();
            _game = game;
            _mapList = new List<Map>();
            _depth = 0;
            _currentDepth = 0;
        }

        public bool GoToMap(int lvl, Player pl)
        {
            if (lvl > _depth + 1 || (lvl == _depth + 1 && !GenNewLevel()))
                return false;
            if (lvl > _currentDepth)
            {
                _currentDepth = lvl;
                pl.PlaceAt(CurrentLevel.StartPosX, CurrentLevel.StartPosY, CurrentLevel);
            }
            else
            {
                _currentDepth = lvl;
                pl.PlaceAt(CurrentLevel.Stair.PosX, CurrentLevel.Stair.PosY, CurrentLevel);
            }
            return true;
        }

        public bool GenNewLevel()
        {
            if (_depth <= MAX_DEPTH)
            {
                Map map;
                _gen.Generate(5, out map, _game);
                map.MaxMonster = 3 * _depth + 5;
                _mapList.Add(map);
                _depth++;
                return true;
            }
            return false;
        }
    }
}
