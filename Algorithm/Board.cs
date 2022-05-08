using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class Board
    {

        const char CIRCLE = '\u25cf'; // 원
        int _size = 0;
        TileType[,] _tile = null;

        public enum TileType
        {
            Empty,
            Wall
        }

        public void Initialize(int size)
        {
            if (size % 2 == 0)
                return;

            this._size = size;
            _tile = new TileType[_size, size];

            //GenerateByBinaryTree();
            GenerateBySiedWinder();
        }

        // SideWider Algorithm
        void GenerateBySiedWinder()
        {

            //길을 막아버림
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }

            Random rand = new Random();
            for (int y = 0; y < _size; y++)
            {
                int count = 1;
                for (int x = 0; x < _size; x++)
                {
                    if (x == 0 | x == _size - 1 | y == 0 | y == _size - 1)
                        _tile[y, x] = TileType.Wall;

                    if (x % 2 == 0 | y % 2 == 0)
                        continue;

                    if (y == _size - 2 && x == _size - 2)
                        continue;

                    if (x == _size - 2)
                        _tile[y + 1, x] = TileType.Empty;

                    if (y == _size - 2)
                        _tile[y, x + 1] = TileType.Empty;

                    if (rand.Next(0,2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        _tile[y + 1 , x - rand.Next(0, count) * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }

        // Binary Tree Algorithm
        void GenerateByBinaryTree()
        {
            //길을 막아버림
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }

            //랜덤으로 위 혹은 오른쪼긍로 길을 뚫음
            Random rand = new Random();
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x == 0 || x == _size - 1 || y == 0 || y == _size - 1)
                        _tile[y, x] = TileType.Wall;

                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == _size - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        _tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    Console.ForegroundColor = GetTileColor(_tile[y, x]);
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
