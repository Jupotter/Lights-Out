using System.Collections.Generic;

namespace Lights_Out
{
    class Dungeon
    {
        const int MAX_DEPTH = 10;

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
            gen = new MapGen();
            this.game = game;
            mapList = new List<Map>();
            depth = 0;
            currentDepth = 0;
        }

        public bool GoToMap(int lvl, Player pl)
        {
            if (lvl > depth + 1 || (lvl == depth + 1 && !GenNewLevel()))
                return false;
            if (lvl > currentDepth)
            {
                currentDepth = lvl;
                pl.PlaceAt(CurrentLevel.StartPosX, CurrentLevel.StartPosY, CurrentLevel);
            }
            else
            {
                currentDepth = lvl;
                pl.PlaceAt(CurrentLevel.Stair.PosX, CurrentLevel.Stair.PosY, CurrentLevel);
            }
            return true;
        }

        public bool GenNewLevel()
        {
            if (depth <= MAX_DEPTH)
            {
                Map map;
                gen.Generate(5, out map, game);
                map.maxMonster = 3 * depth + 5;
                mapList.Add(map);
                depth++;
                return true;
            }
            return false;
        }
    }
}
