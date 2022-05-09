using System;
using System.Collections.Generic;

namespace Exercise
{
    class Graph
    {
        int[,] adj = new int[6, 6]
         {
            {0,1,0,1,0,0 },
            {1,0,1,1,0,0 },
            {0,1,0,0,0,0 },
            {1,1,0,0,1,0 },
            {0,0,0,1,0,1 },
            {0,0,0,0,1,0 }
         };

        List<int>[] adj2 = new List<int>[]
        {
            new List<int>(){1,3},
            new List<int>(){0,2,3},
            new List<int>(){1},
            new List<int>(){0,1,4},
            new List<int>(){3,5},
            new List<int>(){4}
        };


        //들어 갔다왔는지 확인하는 변수
        int[] visit = new int[6];

        //깊이 우선 탐색1
        public void DPS(int now)
        {
            Console.WriteLine(now);
            visit[now] = 1;

            for (int next = 0; next < 6; next++)
            {
                if (visit[next] == 1)
                    continue;
                if (adj[now, next] == 1)
                {
                    DPS(next);
                }
            }
        }



        //깊이 우선 탐색2
        public void DPS2(int now)
        {
            Console.WriteLine(now);
            visit[now] = 1;

            foreach (int next in adj2[now])
            {
                if (visit[next] == 1)
                    continue;
                DPS2(next);
            }
        }

        //길이 끊겨있을때 노드를 하번씩 다 가기위함.
        public void serchAll()
        {
            for (int now = 0; now < 6; now++)
            {
                if (visit[now] == 0)
                {
                    visit[now] = 1;
                    DPS(now);
                }
            }
        }


        public void BFS(int now)
        {
            int[] found = new int[6];
            int[] parent = new int[6];
            int[] distance = new int[6];

            Queue<int> que = new Queue<int>();
            found[now] = 1;
            que.Enqueue(now);
            parent[now] = now;
            distance[now] = 0;

            while (que.Count != 0)
            {
                now = que.Dequeue();
                Console.WriteLine(now);

                for (int next = 0; next < 6; next++)
                {
                    if (found[next] == 1)
                        continue;

                    if (adj[now, next] == 1)
                    {
                        //now = next;
                        found[next] = 1;
                        que.Enqueue(next);

                        parent[next] = now;
                        distance[next] = distance[now] + 1;
                    }
                }

            }

        }


        public void BFS2(int now)
        {
            int[] found = new int[6];
            int[] parent = new int[6];
            int[] distance = new int[6];


            Queue<int> que = new Queue<int>();
            found[now] = 1;
            que.Enqueue(now);
            parent[now] = now;
            distance[now] = 0;

            while (que.Count != 0)
            {
                now = que.Dequeue();
                Console.WriteLine(now);

                foreach(int next in adj2[now])
                {
                    if (found[next] == 1)
                        continue;

                    que.Enqueue(next);
                    found[next] = 1;

                    parent[next] = now;
                    distance[next] = distance[now] + 1;
                }

            }

        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Graph graph = new Graph();
            graph.BFS2(0);
        }
    }
}


