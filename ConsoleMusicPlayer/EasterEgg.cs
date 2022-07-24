namespace ConsoleMusicPlayer
{
    public class EasterEgg
    {
        private const int RENDERARRAY_Y = 28;
        private const int SLEEP = 5;
        private const int DRAWLENGTH = 5;

        private readonly int RenderArray_X = Console.BufferWidth;
        private int offset = 5; //value to determing skew MIN 5 MAX 8

        private int VerticalCenter { get => RENDERARRAY_Y / 2; }
        private int HorizontalCenter { get => RenderArray_X / 2; }

        public void RenderVisualizer()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.CursorVisible = false;

            for (var y = 0; y < 4; y++)
            {
                for (var i = 0; i < 4; i++)
                {
                    RandomColorPicker();
                    DrawFromCenterToLeftPartA();
                    DrawFromCenterToLeftPartB();
                    DrawFromCenterToRightPartA();
                    DrawFromCenterToRightPartB();
                    offset++;
                }
                for (var i = 0; i < 4; i++)
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

        private enum Operation
        {
            Increment,
            Decrement
        }

        private void DrawCycle(Operation horiz, Operation vert, ref int horizontal, ref int vertical)
        {
            Thread.Sleep(SLEEP);
            SetCursorPosition(horizontal, vertical);
            Console.Write("*");

            horizontal = horiz == Operation.Increment ? horizontal + 1 : horizontal - 1;
            vertical = vert == Operation.Increment ? vertical + 1 : vertical - 1;
        }

        private void DrawFromCenterToLeftPartA()
        {
            var vertical = VerticalCenter;
            var horizontal = HorizontalCenter;
            for (var y = 0; y < ((RenderArray_X / 2) / offset) / 2; y++)
            {
                for (var i = 0; i < offset; i++)
                {
                    DrawCycle(Operation.Decrement, Operation.Increment, ref horizontal, ref vertical);
                }
                for (var i = 0; i < DRAWLENGTH; i++)
                {
                    DrawCycle(Operation.Decrement, Operation.Decrement, ref horizontal, ref vertical);
                }
            }
        }

        private void DrawFromCenterToLeftPartB()
        {
            var vertical = VerticalCenter;
            var horizontal = HorizontalCenter;
            for (var y = 0; y < ((RenderArray_X / 2) / offset) / 2; y++)
            {
                for (var i = 0; i < offset; i++)
                {
                    DrawCycle(Operation.Decrement, Operation.Decrement, ref horizontal, ref vertical);
                }
                for (var i = 0; i < DRAWLENGTH; i++)
                {
                    DrawCycle(Operation.Decrement, Operation.Increment, ref horizontal, ref vertical);
                }
            }
        }

        private void DrawFromCenterToRightPartA()
        {
            var vertical = VerticalCenter;
            var horizontal = HorizontalCenter;
            for (var y = 0; y < ((RenderArray_X / 2) / offset) / 2; y++)
            {
                for (var i = 0; i < offset; i++)
                {
                    DrawCycle(Operation.Increment, Operation.Increment, ref horizontal, ref vertical);
                }
                for (var i = 0; i < DRAWLENGTH; i++)
                {
                    DrawCycle(Operation.Increment, Operation.Decrement, ref horizontal, ref vertical);
                }
            }
        }

        private void DrawFromCenterToRightPartB()
        {
            var vertical = VerticalCenter;
            var horizontal = HorizontalCenter;
            for (var y = 0; y < ((RenderArray_X / 2) / offset) / 2; y++)
            {
                for (var i = 0; i < offset; i++)
                {
                    DrawCycle(Operation.Increment, Operation.Decrement, ref horizontal, ref vertical);
                }
                for (var i = 0; i < DRAWLENGTH; i++)
                {
                    DrawCycle(Operation.Increment, Operation.Increment, ref horizontal, ref vertical);
                }
            }
        }

        private static void SetCursorPosition(int hor, int ver)
        {
            Console.SetCursorPosition(hor, ver);
        }

        protected virtual void RandomColorPicker()
        {
            var random = new Random();
            var choice = random.Next(0, 10);
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