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
        public Pos(int posY, int posX) { this.PosY = posY; this.PosX = posX; }
        public int PosY { get; set; }
        public int PosX { get; set; }

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
            BFS();
        }


        //너비 우선 탐색(길찾기)
        void BFS()
        {
            int[] foundY = new int[4] { -1, 0, 1, 0 };
            int[] foundX = new int[4] { 0, -1, 0, 1 };

            int[,] found = new int[_board.Size, _board.Size]; //길을 한번 찾았는지 확인 하는변수
            int[,] found2 = new int[_board.Size, _board.Size];
            int[,] distance = new int[_board.Size, _board.Size]; //목적지 깊이 저장
            Pos[,] parent = new Pos[_board.Size, _board.Size]; //현재 위치의 전 위치를 저장
            Pos[,] parent2 = new Pos[_board.Size, _board.Size];

            Queue<Pos> que = new Queue<Pos>();
            Queue<Pos> que2 = new Queue<Pos>();
            List<Pos> list = new List<Pos>();

            int nowY = PosY;
            int nowX = PosX;

            int nowY2 = PosY;
            int nowX2 = PosX;

            parent[nowY, nowX] = new Pos(nowY, nowX);
            parent2[nowY, nowX] = new Pos(nowY, nowX);
            distance[nowY, nowX] = 0;
            que.Enqueue(new Pos(nowY, nowX));
            que2.Enqueue(new Pos(nowY, nowX));
            found[nowY, nowX] = 1;
            found2[nowY, nowX] = 1;

            parent[nowY, nowX] = new Pos(nowY, nowX);

            int count = 0;
            int count2 = 0;
            while (_board.DestY != nowY || _board.DestY != nowX)
            {
                Pos temp = que.Dequeue();
                nowY = temp.PosY;
                nowX = temp.PosX;

                for (int i = 0; i < 4; i++)
                {
                    Pos tempPos = new Pos(nowY + foundY[i], nowX + foundX[i]);
                    if (_board.Tile[tempPos.PosY, tempPos.PosX] != Board.TileType.Empty)
                        continue;
                    if (found[tempPos.PosY, tempPos.PosX] == 1)
                        continue;

                    que.Enqueue(new Pos(tempPos.PosY, tempPos.PosX));
                    parent[tempPos.PosY, tempPos.PosX] = temp;
                    found[tempPos.PosY, tempPos.PosX] = 1;
                    distance[tempPos.PosY, tempPos.PosX] = distance[nowY, nowX] + 1;

                    //Debug.WriteLine($"현재 Y :{nowY.ToString()},X{nowX.ToString()} :"  + $"다음 Y :{tempPos.PosY.ToString()},X{tempPos.PosX.ToString()}");
                }
                count++;
            }

            while (que2.Count!= 0)
            {
                Pos temp = que2.Dequeue();
                nowY2 = temp.PosY;
                nowX2 = temp.PosX;

                for (int i = 0; i < 4; i++)
                {
                    Pos tempPos = new Pos(nowY2 + foundY[i], nowX2 + foundX[i]);
                    if (_board.Tile[tempPos.PosY, tempPos.PosX] != Board.TileType.Empty)
                        continue;
                    if (found2[tempPos.PosY, tempPos.PosX] == 1)
                        continue;

                    que2.Enqueue(new Pos(tempPos.PosY, tempPos.PosX));
                    parent2[tempPos.PosY, tempPos.PosX] = temp;
                    found2[tempPos.PosY, tempPos.PosX] = 1;
                    //distance[tempPos.PosY, tempPos.PosX] = distance[PosY, PosX] + 1;

                    //Debug.WriteLine($"현재 Y :{nowY.ToString()},X{nowX.ToString()} :" + $"다음 Y :{tempPos.PosY.ToString()},X{tempPos.PosX.ToString()}");
                }
                count2++;
            }

            Debug.WriteLine(count);
            Debug.WriteLine(count2);

            _points.Add(new Pos(nowY, nowX));

            while (nowY != PosY || nowX != PosX)
            {
                Pos temp = parent[nowY, nowX];
                nowY = temp.PosY;
                nowX = temp.PosX;

                _points.Add(new Pos(nowY, nowX));
            }
            _points.Reverse();

            //int y = _board.DestY;
            //int x = _board.DestX;

            //while (parent2[y,x].PosY != y || parent2[y, x].PosX != x)
            //{
            //    _points.Add(new Pos(y, x));

            //    Pos temp = parent2[y, x];
            //    y = temp.PosY;
            //    x = temp.PosX;

            //}

            //_points.Add(new Pos(y,x));
            //_points.Reverse();
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
                nowY = temp.PosY;
                nowX = temp.PosX;

                for (int i = 0; i < 4; i++)
                {
                    Pos tempPos = new Pos(nowY + foundY[i], nowX + foundX[i]);
                    if (_board.Tile[tempPos.PosY, tempPos.PosX] != Board.TileType.Empty)
                        continue;
                    if (found[tempPos.PosY, tempPos.PosX] == 1)
                        continue;

                    que.Enqueue(new Pos(tempPos.PosY, tempPos.PosX));
                    parent[tempPos.PosY, tempPos.PosX] = temp;
                    found[tempPos.PosY, tempPos.PosX] = 1;
                    distance[tempPos.PosY, tempPos.PosX] = distance[PosY, PosX] + 1;

                    //Debug.WriteLine($"현재 Y :{nowY.ToString()},X{nowX.ToString()} :" + $"다음 Y :{tempPos.PosY.ToString()},X{tempPos.PosX.ToString()}");
                }
            }



            int y = _board.DestY;
            int x = _board.DestX;

            while (parent[y, x].PosY != y || parent[y, x].PosX != x)
            {
                _points.Add(new Pos(y, x));

                Pos temp = parent[y, x];
                y = temp.PosY;
                x = temp.PosX;

            }

            _points.Add(new Pos(y, x));
            _points.Reverse();
        }

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

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        int indexCount = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                if (indexCount >= _points.Count)
                    return;

                PosY = _points[indexCount].PosY;
                PosX = _points[indexCount].PosX;
                //switch (randod.Next(0, 5))
                //{
                //    case 1: //상
                //        if (PosY - 1 > 0 && _board.Tile[PosY - 1, PosX] == Board.TileType.Empty)
                //        {
                //            PosY -= 1;
                //        }
                //        break;
                //    case 2: //하
                //        if (PosY + 1 <= _board.Size && _board.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                //        {
                //            PosY += 1;
                //        }
                //        break;
                //    case 3: //좌
                //        if (PosX - 1 > 0 && _board.Tile[PosY, PosX - 1] == Board.TileType.Empty)
                //        {
                //            PosX -= 1;
                //        }
                //        break;
                //    case 4: //우
                //        if (PosX + 1 <= _board.Size && _board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                //        {
                //            PosX += 1;
                //        }
                //        break;
                //}
                indexCount++;
            }
        }
    }
}
