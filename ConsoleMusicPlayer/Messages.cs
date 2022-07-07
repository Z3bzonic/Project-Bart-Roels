namespace ConsoleMusicPlayer
{
    internal class Messages
    {

        private SetColorCombo _colorContrast;
        private AsciiArt _asciiArt;
        private Handler _handler;
        private int previousVolume = 0;
        public Messages(SetColorCombo colorContrast, AsciiArt asciiArt, Handler handler)
        {
            _colorContrast = colorContrast;
            _asciiArt = asciiArt;
            _handler = handler;
        }

        public void PrintTitle()
        {
            _colorContrast.Fullconsole();
            Console.Clear();
            int edgeDistance = 25;
            string[] asciiBlock = _asciiArt.TitleBlock();
            _colorContrast.DarkYellowAndBleu();
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(edgeDistance, i);
                Console.Write($"{asciiBlock[i]}");
            }
        }

        public string PrintAskForPath()
        {
            _handler.PositionMp3Message();
            _colorContrast.RedAndDarkMagenta();
            Console.Write("§.mp3 added by default§");
            _colorContrast.DarkYellowAndBleu();
            _handler.PositionAskForPath();
            string folderMessage = $"Please enter a path in your Musicfolder now in default: {Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)}\\";
            string musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            Console.WriteLine(folderMessage);
            Console.SetCursorPosition(folderMessage.Length, 5);

            Console.Write(new string(' ', Console.WindowWidth - folderMessage.Length));
            Console.SetCursorPosition(folderMessage.Length, 5);
            string songName = $"\\{Console.ReadLine()}.mp3";
            string fullPath = musicFolder + songName;
            return fullPath;
        }

        public void VolumeBarControl(int volume)
        {
            if (volume < 1)
            {
                volume = 1;
            }
            else if (volume > 100)
            {
                volume = 100;
            }
            _colorContrast.VolumeBar();
            _handler.VolumeBarPosition();
            Console.Write("VOL:==");
            string volumeBar = _asciiArt.VolumeBar();
            for (int i = 0; i < volume; i++)
            {
                Console.Write(volumeBar);
            }
            Console.Write("++");
            if (previousVolume > volume)
            {
                _colorContrast.Fullconsole();
                Console.Write(new string(' ', Console.WindowWidth));
            }
            previousVolume = volume;
        }

        public void VolumePlaceHolder()
        {
            _colorContrast.VolumeBar();
            Console.SetCursorPosition(0, 17);
            Console.Write(_asciiArt.VolumePlaceHolder());
        }

        public int VolumeMenu()
        {
            bool choiceTest = false;
            string message = "[ ] <- set volume from 1 - 100";
            int choice = 0;
            while (choiceTest == false)
            {
                _colorContrast.RedAndDarkMagenta();
                _handler.VolumeMenuPosition();
                Console.WriteLine(message);
                Console.SetCursorPosition(1, 18);
                choiceTest = int.TryParse(Console.ReadLine(), out choice);
                if (choiceTest == false)
                {
                    _colorContrast.NowMuted();
                    _handler.VolumeMenuErrorPosition(message);
                    Console.Write("Foute invoer - opnieuw");
                }
            }
            RemoveVolumeMenuError(message);
            _colorContrast.DarkYellowAndBleu();
            return choice;
        }

        public void NowPlaying(string songName)
        {
            _handler.PositionNowPlaying();
            _colorContrast.NowPlaying();
            Console.WriteLine($"Now Playing {songName}");
            _colorContrast.DarkYellowAndBleu();
        }

        public void NowPaused(string songName)
        {
            _handler.PositionNowPaused(songName.Length + 10);
            _colorContrast.NowPaused();
            Console.Write(" - Now paused");
            _colorContrast.DarkYellowAndBleu();
        }

        public void NowMuted()
        {
            _handler.PositionNowmuted();
            _colorContrast.NowMuted();
            Console.WriteLine("-----Now Muted------");
            _colorContrast.DarkYellowAndBleu();
        }

        public string MenuChoice()
        {
            string choice = "";
            _handler.PositionControlMenu();
            _colorContrast.RedAndDarkMagenta();
            string[] menuBlock = _asciiArt.MenuBlock();
            for (int i = 0; i < menuBlock.Length; i++)
            {
                Console.WriteLine(menuBlock[i]);
            }
            Console.SetCursorPosition(0, 18);
            Console.WriteLine("[ ]");
            Console.SetCursorPosition(1, 18);
            choice = Console.ReadLine();

            _colorContrast.DarkYellowAndBleu();
            return choice;
        }

        public void MetaBackdrop() 
        {
            _handler.MetadataBackdropPosition ();
            _colorContrast.MetaBackdropColor ();

            for (int i = 0; i < 4; i++)
            {

                Console.Write($"{new string(' ', 50)}\n");
                Console.CursorLeft = 2;
            }
        }

        public void PrintMetaData(string[] songAttributes) 
        {
            _handler.MetadataPoistion();
            _colorContrast.MetaMessageColor();
            for (int i = 0; i < songAttributes.Length; i++)
            {
                Console.CursorLeft += 4;
                Console.CursorTop += 1;
                Console.Write(songAttributes[i]);
                
            }
        }

        public void ErrorFileNotFound()
        {
            _colorContrast.RedAndDarkMagenta();
            Console.SetCursorPosition(0, 8);
            Console.Write(_asciiArt.ErrorNoSong());
            _colorContrast.DarkYellowAndBleu();
        }

        public void LoadingError()
        {
            _handler.PositionLoadError();
            Console.Write(new string(' ', Console.WindowWidth));
            _handler.PositionLoadError();
            Console.WriteLine("No song loaded yet, nice try though!");
        }

        public void ResetErrorMessage()
        {
            _colorContrast.Fullconsole();
            Console.SetCursorPosition(0, 9);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 10);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void ResetNoFileLoadedMessage()
        {
            _colorContrast.Fullconsole();
            _handler.PositionLoadError();
            Console.Write(new string(' ', Console.WindowWidth));
            _colorContrast.DarkYellowAndBleu();
        }

        public void RemoveMuteMessage()
        {
            _colorContrast.Fullconsole();
            _handler.PositionNowmuted();
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void RemovePauseMessage()
        {
            Console.SetCursorPosition(0, 6);
            _colorContrast.Fullconsole();
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void RemoveVolumeMenu()
        {
            _handler.VolumeMenuPosition();
            _colorContrast.Fullconsole();
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void RemoveVolumeMenuError(string message)
        {
            _handler.VolumeMenuErrorPosition(message);
            _colorContrast.Fullconsole();
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}