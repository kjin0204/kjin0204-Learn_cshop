using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Exercise
{
    class Graph
    {
        int[,] adj = new int[6, 6]
         {
            {-1,15,-1,35,-1,-1 },
            {15,-1,05,10,-1,-1 },
            {-1,05,-1,-1,-1,-1 },
            {35,10,-1,-1,05,-1 },
            {-1,-1,-1,05,-1,05 },
            {-1,-1,-1,-1,05,-1 }
         };


        int[,] adj3 = new int[6, 6]
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


        public void Dijikstra(int start)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];
            int[] parent = new int[6];
            Array.Fill(distance, Int32.MaxValue); //배열의 모든값 변경
            distance[start] = 0;
            parent[start] = start;

            while (true)
            {
                // 제일 좋은 후보를 찾는다( 가장 가까이에 있는 점)

                //가장 유력한 후보의 거리와 번호를 저장
                int closest = Int32.MaxValue;
                int now = -1;

                for (int i = 0; i < 6; i++)
                {
                    // 이미 방문한 정점은 스킵
                    if (visited[i])
                        continue;
                    // 아직 발견된 적이 없거나 기존후보보다 멀리 있으면 스킵
                    if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                        continue;
                    // 여태껏 발견한 가장 후보라는 의미니까 정보를 갱신
                    closest = distance[i];
                    now = i;
                }

                //다음 후보가 하나도 없다.
                if (now == -1)
                    break;

                //제일 좋은 후보를 찾았으니까 방문한다.
                visited[now] = true;
                Console.WriteLine(now);

                //방문한 정점과 인접한 정점들을 조사해서 상황에 따라 발견한 최단거리를 갱신
                for (int next = 0; next < 6; next++)
                {
                    //연결되어있지않으면 스킵
                    if (adj[now, next] == -1)
                        continue;
                    //이미 방문한 정점은 스킵
                    if (visited[next])
                        continue;

                    //새로 조사된 정점의 최단거리를 계산한다.
                    int nextDist = distance[now] + adj[now, next];

                    if (distance[next] >= nextDist)
                    {
                        distance[next] = nextDist;
                        parent[next] = now;
                    }
                }
            }
        }

        bool[] visitt = new bool[6];
        public void test(int now)
        {
            //DPS
            //Console.WriteLine(now);
            //visitt[now] = true;

            //for(int i = 0; i < 6; i++)
            //{
            //    if (visitt[i])
            //        continue;
            //    if (adj3[now, i] != 1)
            //        continue;

            //    test(i);
            //}


            //BFS

            bool[] found = new bool[6];
            Queue<int> q = new Queue<int>();

            q.Enqueue(now);
            int[] distance = new int[6];
            int[] parent = new int[6];

            distance[now] = 0;
            distance[now] = now;
            found[now] = true;

            while (q.Count != 0)
            {
                now = q.Dequeue();
                Console.WriteLine(now);

                for (int next = 0; next < 6; next++)
                {
                    if (found[next])
                        continue;
                    if (adj3[now, next] == 0)
                        continue;

                    q.Enqueue(next);
                    distance[next] = distance[now] + 1;
                    parent[next] = now;
                    found[next] = true;

                }
            }




        }


        //들어 갔다왔는지 확인하는 변수
        int[] visit = new int[6];

        #region 깊이우선 탐색DPS
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
        #endregion

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

        #region 너비우선 탐색 BFS
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

                foreach (int next in adj2[now])
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
        #endregion

    }


    class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> heap = new List<T>();

        public void Push(T data)
        {
            heap.Add(data);
            int _now = heap.Count - 1;

            while (_now > 0)
            {
                int _next = (_now - 1) / 2;

                if (heap[_next].CompareTo(heap[_now]) > 0)
                    break;

                T temp = heap[_next];
                heap[_next] = heap[_now];
                heap[_now] = temp;

                _now = _next;
            }
        }

        public T Pop()
        {
            if (heap.Count == 0)
                throw new Exception("꺼낼 데이터가 없습니다.");

            //반환할 데이터
            T _returnValue = heap[0];

            //마지막 데이터를 루트로 이동
            int _lastIndex = heap.Count - 1;
            heap[0] = heap[_lastIndex];
            heap.RemoveAt(_lastIndex); //마지막 데이터 삭제

            _lastIndex--;

            int _now = 0;
            while (true)
            {
                int _left = (_now * 2) + 1;
                int _right = (_now * 2) + 2;
                int _next = _now;
                //왼쪽값이 현재값보다 크면, 왼쪽으로 이동
                if (_left <= _lastIndex && heap[_left].CompareTo(heap[_now]) > 0)
                    _next = _left;

                //오른값이 현재값(왼쪽 이동 포함) 보다 크면, 오른쪽으로 이동
                if (_right <= _lastIndex && heap[_right].CompareTo(heap[_next]) > 0)
                    _next = _right;

                if (_next == _now)
                    break;

                T temp = heap[_next];
                heap[_next] = heap[_now];
                heap[_now] = temp;

                _now = _next;
            }

            return _returnValue;


        }

        public int Count()
        {
            return heap.Count;
        }
    }

    class Knight : IComparable<Knight>
    {
        public int id { get; set; }

        public int CompareTo(Knight other)
        {
            if (id == other.id)
                return 0;
            return id > other.id ? 1 : -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Graph graph = new Graph();
            //graph.Dijikstra(0);


            PriorityQueue<Knight> q = new PriorityQueue<Knight>();
            q.Push(new Knight() { id = 20 });
            q.Push(new Knight() { id = 30 });
            q.Push(new Knight() { id = 40 });
            q.Push(new Knight() { id = 10 });
            q.Push(new Knight() { id = 05 });

            while (q.Count() != 0)
            {
                Console.WriteLine(q.Pop().id);
            }
            //int value = q.Pop();
        }
    }
}


