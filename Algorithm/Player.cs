using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        Board _board;
        Random randod = new Random();

        public void Initalize(int posX,int posY, int destX, int destY , Board board)
        {
            this.PosX = posX;
            this.PosY = posY;

            _board = board;
        }

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                switch (randod.Next(0, 5))
                {
                    case 1: //상
                        if (PosY - 1 > 0 && _board.Tile[PosY - 1, PosX] == Board.TileType.Empty)
                        {
                            PosY -= 1;
                        }
                        break;
                    case 2: //하
                        if (PosY + 1 <= _board.Size && _board.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                        {
                            PosY += 1;
                        }
                        break;
                    case 3: //좌
                        if (PosX - 1 > 0 && _board.Tile[PosY, PosX - 1] == Board.TileType.Empty)
                        {
                            PosX -= 1;
                        }
                        break;
                    case 4: //우
                        if (PosX + 1 <= _board.Size && _board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                        {
                            PosX += 1;
                        }
                        break;
                }
            }
        }
    }
}
