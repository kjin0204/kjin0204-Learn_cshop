using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {

            Board_linkedList boardtest = new Board_linkedList();
            boardtest.Initialize();


            Console.CursorVisible = false; //커서 안보이게 하기위함.

            const int WAIT_TICK = 1000 / 30;
            int lastTick = 0;
            int boardSize = 25;


            Board board = new Board();
            Player player = new Player();
            board.Initialize(boardSize, player);
            player.Initalize(1, 1, board);



            while (true)
            {
                #region 프레임 관리
                int currentTick = System.Environment.TickCount; //시스템이 시작된 이후 경과된 시간
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion

                //입력

                //로직
                player.Update(deltaTick);

                //랜더링
                Console.SetCursorPosition(0, 0); //콘솔 커서위치
                board.Render();

            }
        }
    }
}
