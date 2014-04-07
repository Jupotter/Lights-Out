using System;
using System.Collections.Generic;

namespace Lights_Out
{
    public struct IntCouple { public int X; public int Y;}

    public class Dijkstra
    {
        struct DijkstraStart { public int X; public int Y; public float Val;}
        
        const float Diag = 1.41421356f;
        readonly Map _source;
        readonly float[,] _pathMap;
        readonly List<DijkstraStart> _startPos;

        public float this[int x, int y]
        { get { return _pathMap[x, y]; } }

        public Dijkstra(Map source)
        {
            _source = source;
            _pathMap = new float[Map.MAP_WIDTH,Map.MAP_HEIGHT];
            _startPos = new List<DijkstraStart>();
            Clear();
        }

        public void AddStartPos(int x, int y, int val)
        {
            if (x >= 0 && x < Map.MAP_WIDTH && y >= 0 && y < Map.MAP_HEIGHT
                && _source.TCODMap.isWalkable(x, y))
                _startPos.Add(new DijkstraStart { X = x, Y = y, Val = val });
        }

        public void Clear()
        {
            _startPos.Clear();
            for (int i = 0; i < Map.MAP_WIDTH; i++)
                for (int j = 0; j < Map.MAP_HEIGHT; j++)
                {
                    _pathMap[i, j] = Int32.MaxValue;
                }
        }

        public void ComputeDijkstra()
        {
            var q = new Queue<IntCouple>();
            foreach (DijkstraStart start in _startPos)
            {
                _pathMap[start.X, start.Y] = start.Val;
                q.Enqueue(new IntCouple { X = start.X, Y = start.Y });
            }

            while (q.Count != 0)
            {
                IntCouple pos = q.Dequeue();
                float val = _pathMap[pos.X, pos.Y];
                for (int i = -1; i < 2; i++)
                    for (int j = -1; j < 2; j++)
                    {
                        if (pos.X + i >= 0 && pos.X + i < Map.MAP_WIDTH && pos.Y + j >= 0 && pos.Y + j < Map.MAP_HEIGHT
                            && _source.TCODMap.isWalkable(pos.X + i, pos.Y + j)
                            && _pathMap[pos.X + i, pos.Y + j] > val + ((i == 0 || j == 0)?1:Diag))
                        {
                            _pathMap[pos.X + i, pos.Y + j] = val + ((i == 0 || j == 0)?1:Diag);
                            q.Enqueue(new IntCouple { X = pos.X + i, Y = pos.Y + j });
                            //Console.WriteLine(Q.Count);
                        }
                    }
            }

            /*for (int i = 0; i < Map.MAP_WIDTH; i++)
            {
                for (int j = 0; j < Map.MAP_HEIGHT; j++)
                {
                    Console.Write("{0,-10},", pathMap[i, j]);
                }
                Console.WriteLine();
            }//*/
        }

        public IntCouple GetNext(int x, int y)
        {
            var next = new DijkstraStart { X = 0, Y = 0, Val = int.MaxValue };
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    if (x + i >= 0 && x + i < Map.MAP_WIDTH && y + j >= 0 && y + j < Map.MAP_HEIGHT
                        && _pathMap[x + i, y + j] < next.Val)
                    {
                        next = new DijkstraStart { X = x + i, Y = y + j, Val = _pathMap[x + i, y + j] };
                    }
                }
            return new IntCouple { X = next.X, Y = next.Y };
        }
    }
}
