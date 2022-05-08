using System;
using System.Collections.Generic;
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

            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] RightY = new int[] { 0, -1, 0, 1 };
            int[] RightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            while (board.DestY != PosY || board.DestX != PosX)
            {
                // 1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인
                if (board.Tile[PosY + RightY[_dir], PosX + RightX[_dir]] == Board.TileType.Empty)
                {
                    //오른쪽 방향으로 90도 회전
                    _dir = (_dir - 1 + 4) % 4;
                    //앞으로 한보 전진
                    PosY += frontY[_dir];
                    PosX += frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                //2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 확인
                else if (board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
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

        const int MOVE_TICK = 10;
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
