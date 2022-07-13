namespace ConsoleMusicPlayer
{
    internal class EasterEgg
    {
        private const int RENDERARRAY_Y = 28;
        private int RenderArray_X = Console.BufferWidth;
        private int offset = 5; //value to determing skew MIN 5 MAX 8
        private int sleep = 2;

        public EasterEgg()
        {
        }

        public void RenderVisualizer()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.CursorVisible = false;
            for (int y = 0; y < 4; y++)
            {
                for (int i = 0; i < 4; i++)
                {
                    RandomColorPicker();
                    DrawFromCenterToLeftPartA();
                    DrawFromCenterToLeftPartB();
                    DrawFromCenterToRightPartA();
                    DrawFromCenterToRightPartB();
                    offset++;
                }
                for (int i = 0; i < 4; i++)
                {
                    offset--;
                    RandomColorPicker();
                    DrawFromCenterToLeftPartB();
                    DrawFromCenterToLeftPartA();
                    DrawFromCenterToRightPartB();
                    DrawFromCenterToRightPartA();
                }
            }
        }

        private void DrawFromCenterToLeftPartA()
        {
            int vertical = RENDERARRAY_Y / 2;
            int horizontal = RenderArray_X / 2;
            for (int y = 0; y < ((RenderArray_X / 2) / offset) / 2; y++)
            {
                for (int i = 0; i < offset; i++)
                {
                    Thread.Sleep(sleep);
                    SetCursorPosition(horizontal, vertical);
                    Console.Write("*");
                    horizontal--;
                    vertical++;
                }
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(sleep);
                    SetCursorPosition(horizontal, vertical);
                    Console.Write("*");
                    horizontal--;
                    vertical--;
                }
            }
        }

        private void DrawFromCenterToLeftPartB()
        {
            int vertical = RENDERARRAY_Y / 2;
            int horizontal = RenderArray_X / 2;
            for (int y = 0; y < ((RenderArray_X / 2) / offset) / 2; y++)
            {
                for (int i = 0; i < offset; i++)
                {
                    Thread.Sleep(sleep);
                    SetCursorPosition(horizontal, vertical);
                    Console.Write("*");
                    horizontal--;
                    vertical--;
                }
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(sleep);
                    SetCursorPosition(horizontal, vertical);
                    Console.Write("*");
                    horizontal--;
                    vertical++;
                }
            }
        }

        private void DrawFromCenterToRightPartA()
        {
            int vertical = RENDERARRAY_Y / 2;
            int horizontal = RenderArray_X / 2;
            for (int y = 0; y < ((RenderArray_X / 2) / offset) / 2; y++)
            {
                for (int i = 0; i < offset; i++)
                {
                    Thread.Sleep(sleep);
                    SetCursorPosition(horizontal, vertical);
                    Console.Write("*");
                    horizontal++;
                    vertical++;
                }
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(sleep);
                    SetCursorPosition(horizontal, vertical);
                    Console.Write("*");
                    horizontal++;
                    vertical--;
                }
            }
        }

        private void DrawFromCenterToRightPartB()
        {
            int vertical = RENDERARRAY_Y / 2;
            int horizontal = RenderArray_X / 2;
            for (int y = 0; y < ((RenderArray_X / 2) / offset) / 2; y++)
            {
                for (int i = 0; i < offset; i++)
                {
                    Thread.Sleep(sleep);
                    SetCursorPosition(horizontal, vertical);
                    Console.Write("*");
                    horizontal++;
                    vertical--;
                }
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(sleep);
                    SetCursorPosition(horizontal, vertical);
                    Console.Write("*");
                    horizontal++;
                    vertical++;
                }
            }
        }

        private void SetCursorPosition(int hor, int ver)
        {
            Console.SetCursorPosition(hor, ver);
        }

        private void RandomColorPicker()
        {
            Random random = new Random();
            int choice = random.Next(0, 10);
            switch (choice)
            {
                case 0: Console.ForegroundColor = ConsoleColor.Red; break;
                case 1: Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case 2: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case 3: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case 4: Console.ForegroundColor = ConsoleColor.Blue; break;
                case 5: Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                case 6: Console.ForegroundColor = ConsoleColor.Green; break;
                case 7: Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                case 8: Console.ForegroundColor = ConsoleColor.Cyan; break;
                case 9: Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                default:
                    break;
            }
        }

        //private void RandomPositionPicker()
        //{
        //    Random random = new Random();
        //    Console.SetCursorPosition(random.Next(0, 80), random.Next(0, 40));
        //}

        //private void RandomSpeedPicker()
        //{
        //    Random random = new Random();
        //    int choice = random.Next(0, 80);
        //    Thread.Sleep(random.Next(10, 200));
        //}
    }
}