using System.Collections.Generic;

namespace Lights_Out
{
    class Dungeon
    {
        const int MAX_DEPTH = 5;

        Game game;

        List<Map> mapList;
        int depth;
        int currentDepth;
        public int CurrentDepth
        {
            get { return currentDepth; }
            set { if (value <= depth) currentDepth = value; }
        }
        public Map CurrentLevel
        { get { return mapList[currentDepth - 1]; } }

        MapGen gen;

        public MapGen Gen
        {
            get { return gen; }
        }

        public Dungeon(Game game)
        {
            gen = new MapGen(245);
            this.game = game;
            mapList = new List<Map>();
            depth = 0;
            currentDepth = 0;
        }

        public bool GoToMap(int lvl, Player pl)
        {
            if (lvl > depth + 1 || (lvl == depth + 1 && !GenNewLevel()))
                return false;
            currentDepth = lvl;
            pl.PlaceAt(CurrentLevel.StartPosX, CurrentLevel.StartPosY, CurrentLevel);
            return true;
        }

        public bool GenNewLevel()
        {
            if (depth <= MAX_DEPTH)
            {
                Map map;
                gen.Generate(5, out map, game);
                mapList.Add(map);
                depth++;
                return true;
            }
            return false;
        }
    }
}
