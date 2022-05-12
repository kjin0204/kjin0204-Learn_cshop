using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{

    class Pos
    {
        public Pos(int posY, int posX) { this.Y = posY; this.X = posX; }
        public int Y { get; set; }
        public int X { get; set; }

    }

    class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        Board _board;
        Random randod = new Random();

        enum Dir
        {
            Up,
            Left,
            Down,
            Right
        }


        int _dir = (int)Dir.Up;
        List<Pos> _points = new List<Pos>();

        public void Initalize(int posX, int posY, Board board)
        {
            this.PosX = posX;
            this.PosY = posY;
            _board = board;

            //rightHandle();
            AStar();
        }

        class PQNode : IComparable<PQNode>
        {
            public Pos pos;
            public int F;
            public int G;
            public int X;
            public int Y;

            public int CompareTo(PQNode other)
            {
                if (F == other.F)
                    return 0;
                return F < other.F ? 1 : -1;
            }
        }

        //A*길찾기
        void AStar()
        {
            //점수 매기기(가산점)
            // F = G + H
            // F = 최종 점수 (작을 수록 좋음, 경로에 따라 달라짐)
            // G = 시작점에서 해당 좌표까지 이동하는데 드는 비용(작을수록 좋음, 경로에 따라 달라짐)
            // H = 목적지에서 얼마나 가까운지( 작을수록 좋음, 고정)

            // (Y,X) 이미 방문했는지 여부(방문 closed 상태)
            bool[,] closed = new bool[_board.Size, _board.Size];

            //(Y,X)  한번이라도 발견했는지
            //발견x => MaxValue
            //발견O => F = G + H;
            int[,] open = new int[_board.Size, _board.Size];
            for (int y = 0; y < _board.Size; y++)
                for (int x = 0; x < _board.Size; x++)
                    open[y, x] = int.MaxValue;

            //시작점 발견(예약 진행)
            open[PosY, PosX] = Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX);
            PriorityQueue<PQNode> q = new PriorityQueue<PQNode>();

            q.Push(new PQNode() {F = open[PosY, PosX],G = 0,Y = PosY,X = PosX });

            Pos[,] parent = new Pos[_board.Size, _board.Size];
            parent[PosY, PosX] = new Pos(PosY, PosX);

            int[] foundY = new int[4] { -1, 0, 1, 0 };
            int[] foundX = new int[4] { 0, -1, 0, 1 };
            int[] cost = new int[4] { 1, 1, 1, 1 };

            while (q.Count > 0)
            {
                PQNode node = new PQNode();
                //제일좋은 후보를 찾는다
                node = q.Pop();

                //동일한 좌표를 여러 경로로 찾아서, 더 빠른 경로로 인해서 이미 방문된 경우 스킵.
                if (closed[node.Y, node.X])
                    continue;

                closed[node.Y, node.X] = true;

                if (_board.DestY == node.Y && _board.DestX == node.X)
                    break;


                int g = 0;
                int h = 0;
                int f = 0;

                for (int i = 0; i < foundY.Length; i++)
                {
                    int nextY = node.Y + foundY[i];
                    int nextX = node.X + foundX[i];

                    //유효범위를 벗어났을때
                    if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
                        continue;
                    //벽일때
                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;
                    //이미 방문했을때
                    if (closed[nextY, nextX])
                        continue;

                        g = node.G + cost[i];
                        h = Math.Abs(_board.DestY - nextY) + Math.Abs(_board.DestX - nextX);
                        f = g + h;

                        // 다른 경로에서 더 빠른 길 이미 찾았으면 스킵
                        if (open[nextY, nextX] < f)
                            continue;

                        //예약진행
                        open[nextY, nextX] = f;
                        q.Push(new PQNode() { F = f, G = g, X = nextX, Y = nextY });
                        parent[nextY, nextX] = new Pos(node.Y, node.X);

                    Debug.WriteLine($"현재: nodeY: {node.Y},nodeX: {node.X},다음 : nextY: {nextY},nextX: {nextX}, g: {g}, h:{h}, f{f}");
                }
            }

            CalcPathFromParent(parent);
        }
        void CalcPathFromParent(Pos[,] parent)
        {
            int y = _board.DestY;
            int x = _board.DestX;
            while (parent[y, x].Y != y || parent[y, x].X != x)
            {
                _points.Add(new Pos(y, x));
                Pos pos = parent[y, x];
                y = pos.Y;
                x = pos.X;
            }

            _points.Add(new Pos(y, x));
            _points.Reverse();
        }

        #region BFS 너비 우선 탐색
        //너비 우선 탐색(길찾기)
        void BFS()
        {
            int[] foundY = new int[4] { -1, 0, 1, 0 };
            int[] foundX = new int[4] { 0, -1, 0, 1 };

            int[,] found = new int[_board.Size, _board.Size]; //길을 한번 찾았는지 확인 하는변수
            int[,] distance = new int[_board.Size, _board.Size]; //목적지 깊이 저장
            Pos[,] parent = new Pos[_board.Size, _board.Size]; //현재 위치의 전 위치를 저장

            Queue<Pos> que = new Queue<Pos>();
            List<Pos> list = new List<Pos>();

            int nowY = PosY;
            int nowX = PosX;

            parent[nowY, nowX] = new Pos(nowY, nowX);
            distance[nowY, nowX] = 0;
            que.Enqueue(new Pos(nowY, nowX));
            found[nowY, nowX] = 1;

            parent[nowY, nowX] = new Pos(nowY, nowX);

            int count = 0;
            int count2 = 0;
            while (_board.DestY != nowY || _board.DestY != nowX)
            {
                Pos temp = que.Dequeue();
                nowY = temp.Y;
                nowX = temp.X;

                for (int i = 0; i < 4; i++)
                {
                    Pos tempPos = new Pos(nowY + foundY[i], nowX + foundX[i]);
                    if (_board.Tile[tempPos.Y, tempPos.X] != Board.TileType.Empty)
                        continue;
                    if (found[tempPos.Y, tempPos.X] == 1)
                        continue;

                    que.Enqueue(new Pos(tempPos.Y, tempPos.X));
                    parent[tempPos.Y, tempPos.X] = temp;
                    found[tempPos.Y, tempPos.X] = 1;
                    distance[tempPos.Y, tempPos.X] = distance[nowY, nowX] + 1;

                    //Debug.WriteLine($"현재 Y :{nowY.ToString()},X{nowX.ToString()} :"  + $"다음 Y :{tempPos.PosY.ToString()},X{tempPos.PosX.ToString()}");
                }
                count++;
            }

            Debug.WriteLine(count);
            Debug.WriteLine(count2);

            _points.Add(new Pos(nowY, nowX));

            while (nowY != PosY || nowX != PosX)
            {
                Pos temp = parent[nowY, nowX];
                nowY = temp.Y;
                nowX = temp.X;

                _points.Add(new Pos(nowY, nowX));
            }
            _points.Reverse();
        }


        //너비 우선 탐색(길찾기)
        void BFS2()
        {
            int[] foundY = new int[4] { -1, 0, 1, 0 };
            int[] foundX = new int[4] { 0, -1, 0, 1 };

            int[,] found = new int[_board.Size, _board.Size];
            int[,] distance = new int[_board.Size, _board.Size];
            Pos[,] parent = new Pos[_board.Size, _board.Size];

            Queue<Pos> que = new Queue<Pos>();
            List<Pos> list = new List<Pos>();

            int nowY = PosY;
            int nowX = PosX;

            parent[nowY, nowX] = new Pos(nowY, nowX);
            distance[nowY, nowX] = 0;
            que.Enqueue(new Pos(nowY, nowX));
            found[nowY, nowX] = 1;

            parent[nowY, nowX] = new Pos(nowY, nowX);



            while (que.Count != 0)
            {
                Pos temp = que.Dequeue();
                nowY = temp.Y;
                nowX = temp.X;

                for (int i = 0; i < 4; i++)
                {
                    Pos tempPos = new Pos(nowY + foundY[i], nowX + foundX[i]);
                    if (_board.Tile[tempPos.Y, tempPos.X] != Board.TileType.Empty)
                        continue;
                    if (found[tempPos.Y, tempPos.X] == 1)
                        continue;

                    que.Enqueue(new Pos(tempPos.Y, tempPos.X));
                    parent[tempPos.Y, tempPos.X] = temp;
                    found[tempPos.Y, tempPos.X] = 1;
                    distance[tempPos.Y, tempPos.X] = distance[PosY, PosX] + 1;

                    //Debug.WriteLine($"현재 Y :{nowY.ToString()},X{nowX.ToString()} :" + $"다음 Y :{tempPos.PosY.ToString()},X{tempPos.PosX.ToString()}");
                }
            }



            int y = _board.DestY;
            int x = _board.DestX;

            while (parent[y, x].Y != y || parent[y, x].X != x)
            {
                _points.Add(new Pos(y, x));

                Pos temp = parent[y, x];
                y = temp.Y;
                x = temp.X;

            }

            _points.Add(new Pos(y, x));
            _points.Reverse();
        }
        #endregion

        #region 우수법칙
        //우수 법칙
        void rightHandle()
        {
            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] RightY = new int[] { 0, -1, 0, 1 };
            int[] RightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            while (_board.DestY != PosY || _board.DestX != PosX)
            {
                // 1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인
                if (_board.Tile[PosY + RightY[_dir], PosX + RightX[_dir]] == Board.TileType.Empty)
                {
                    //오른쪽 방향으로 90도 회전
                    _dir = (_dir - 1 + 4) % 4;
                    //앞으로 한보 전진
                    PosY += frontY[_dir];
                    PosX += frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                //2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 확인
                else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
                {
                    //앞으로 한 보 전진
                    PosY += frontY[_dir];
                    PosX += frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                else
                {
                    // 왼쪽 방향으로 90도 회전
                    _dir = (_dir + 1 + 4) % 4;
                }

            }
        }
        #endregion

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        //int indexCount = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
            {
                _lastIndex = 0;
                _points.Clear();
                _board.Initialize(_board.Size, this);
                Initalize(1, 1, _board);
            }

            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
            }
        }
    }
}
