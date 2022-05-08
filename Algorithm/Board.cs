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
        public int Size { get; private set; }
        public TileType[,] Tile { get; private set; }

        public int DestY { get; private set; }
        public int DestX { get; private set; }
        Player _player;

        public enum TileType
        {
            Empty,
            Wall
        }

        public void Initialize(int size, Player player)
        {
            if (size % 2 == 0)
                return;

            this.Size = size;
            Tile = new TileType[Size, size];

            DestX = size - 2;
            DestY = size - 2;

            _player = player;

            //GenerateByBinaryTree();
            GenerateBySiedWinder();
        }

        // SideWider Algorithm
        void GenerateBySiedWinder()
        {

            //길을 막아버림
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    if (x == 0 | x == Size - 1 | y == 0 | y == Size - 1)
                        Tile[y, x] = TileType.Wall;

                    if (x % 2 == 0 | y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (x == Size - 2)
                        Tile[y + 1, x] = TileType.Empty;

                    if (y == Size - 2)
                        Tile[y, x + 1] = TileType.Empty;

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        Tile[y + 1, x - rand.Next(0, count) * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }

        // Binary Tree Algorithm
        void GenerateByBinaryTree()
        {
            //길을 막아버림
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            //랜덤으로 위 혹은 오른쪼긍로 길을 뚫음
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x == 0 || x == Size - 1 || y == 0 || y == Size - 1)
                        Tile[y, x] = TileType.Wall;

                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        Tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (_player.PosX == x && _player.PosY == y)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (DestX == x && DestY == y)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);
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
