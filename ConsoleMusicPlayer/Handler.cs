namespace ConsoleMusicPlayer
{
    internal class Handler // started as error handling attempt, now more of a settings file
    {
        //public void ExitKeyPressed()
        //{
        //    if (Console.KeyAvailable == false)
        //    {
        //        if (Console.ReadKey(true).Key == ConsoleKey.Subtract)
        //        {
        //            Environment.Exit(0);
        //        }
        //    }
        //}

        public void PositionAskForPath()
        {
            Console.SetCursorPosition(0, 5);
        }

        public void PositionMp3Message()
        {
            Console.SetCursorPosition(60, 4);
        }

        public void PositionLoadError()
        {
            Console.SetCursorPosition(0, 19);
        }

        public void PositionControlMenu()
        {
            Console.SetCursorPosition(0, 20);
        }

        public void PositionNowPlaying()
        {
            Console.SetCursorPosition(0, 6);
        }

        public void PositionNowPaused(int length)
        {
            Console.SetCursorPosition(length, 6);
        }

        public void PositionNowmuted()
        {
            Console.SetCursorPosition(0, 6);
        }

        public void VolumeBarPosition() 
        {
            Console.SetCursorPosition(0, 16);

        }

        public void VolumeMenuPosition() 
        {
            Console.SetCursorPosition(0, 18);
        }

        public void VolumeMenuErrorPosition(string message) 
        {
            Console.SetCursorPosition(message.Length, 18);
        }

        public void MetadataPoistion() 
        {
            Console.SetCursorPosition(4, 8);
        }

        public void MetadataBackdropPosition() 
        {
            Console.SetCursorPosition(2, 7);
        }

    }
}