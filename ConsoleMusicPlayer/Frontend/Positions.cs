namespace ConsoleMusicPlayer.Frontend
{
    internal class Positions // started as error handling attempt, now more of a settings file
    {
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
            Console.SetCursorPosition(3, 19);
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

        public void VolumeUnfilledBar()
        {
            Console.SetCursorPosition(0, 17);
        }

        public void VolumeMenuErrorPosition(string message)
        {
            Console.SetCursorPosition(message.Length, 18);
        }

        public void MetadataBackdropPosition()
        {
            Console.SetCursorPosition(3, 8);
        }

        public void MetaDataPosition()
        {
            Console.SetCursorPosition(5, 9);
        }

        public void RetrySongInputPosition()
        {
            Console.SetCursorPosition(60, 4);
        }

        public void GetMusicBackdropPosition()
        {
            Console.SetCursorPosition(65, 8);
        }

        public void SongSelectPosition()
        {
            Console.SetCursorPosition(64, 9);
        }

        public void DisplayLoadingMetaPosition()
        {
            Console.SetCursorPosition(3, 12);
        }
    }
}