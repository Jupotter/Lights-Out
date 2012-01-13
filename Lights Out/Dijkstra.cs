using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lights_Out
{
    public struct intCouple { public int X; public int Y;}

    public class Dijkstra
    {
        struct dijkstraStart { public int X; public int Y; public float val;}
        
        const float diag = 1.41421356f;
        Map source;
        float[,] pathMap;
        List<dijkstraStart> startPos;

        public float this[int x, int y]
        { get { return pathMap[x, y]; } }

        public Dijkstra(Map source)
        {
            this.source = source;
            this.pathMap = new float[Map.MAP_WIDTH,Map.MAP_HEIGHT];
            startPos = new List<dijkstraStart>();
            Clear();
        }

        public void AddStartPos(int X, int Y, int val)
        {
            startPos.Add(new dijkstraStart { X = X, Y = Y , val = val});
        }

        public void Clear()
        {
            startPos.Clear();
            for (int i = 0; i < Map.MAP_WIDTH; i++)
                for (int j = 0; j < Map.MAP_HEIGHT; j++)
                {
                    pathMap[i, j] = Int32.MaxValue;
                }
        }

        public void ComputeDijkstra()
        {
            Queue<intCouple> Q = new Queue<intCouple>();
            foreach (dijkstraStart start in startPos)
            {
                pathMap[start.X, start.Y] = start.val;
                Q.Enqueue(new intCouple { X = start.X, Y = start.Y });
            }

            while (Q.Count != 0)
            {
                intCouple pos = Q.Dequeue();
                float val = pathMap[pos.X, pos.Y];
                for (int i = -1; i < 2; i++)
                    for (int j = -1; j < 2; j++)
                    {
                        if (pos.X + i >= 0 && pos.X + i < Map.MAP_WIDTH && pos.Y + j >= 0 && pos.Y + j < Map.MAP_HEIGHT
                            && source.TCODMap.isWalkable(pos.X + i, pos.Y + j)
                            && pathMap[pos.X + i, pos.Y + j] > val + ((i == 0 || j == 0)?1:diag))
                        {
                            pathMap[pos.X + i, pos.Y + j] = val + ((i == 0 || j == 0)?1:diag);
                            Q.Enqueue(new intCouple { X = pos.X + i, Y = pos.Y + j });
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

        public intCouple GetNext(int X, int Y)
        {
            dijkstraStart next = new dijkstraStart { X = 0, Y = 0, val = int.MaxValue };
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    if (X + i >= 0 && X + i < Map.MAP_WIDTH && Y + j >= 0 && Y + j < Map.MAP_HEIGHT
                        && pathMap[X + i, Y + j] < next.val)
                    {
                        next = new dijkstraStart { X = X + i, Y = Y + j, val = pathMap[X + i, Y + j] };
                    }
                }
            return new intCouple { X = next.X, Y = next.Y };
        }
    }
}
