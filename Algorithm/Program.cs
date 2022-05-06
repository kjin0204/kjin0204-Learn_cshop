using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {

            Board_linkedList board = new Board_linkedList();
            board.Initialize();

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;
            const char CIRCLE = '\u25cf'; // 원
            int lastTick = 0;

            while(true)
            {
                #region 프레임 관리
                int currentTick = System.Environment.TickCount; //시스템이 시작된 이후 경과된 시간
                if (currentTick - lastTick < WAIT_TICK)
                    continue;

                lastTick = currentTick;
                #endregion


                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Green;

                for(int i = 0; i< 25; i++)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        Console.Write(CIRCLE);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
