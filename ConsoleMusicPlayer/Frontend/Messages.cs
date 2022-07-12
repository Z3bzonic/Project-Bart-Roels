using ConsoleMusicPlayer.Backend;
using ConsoleMusicPlayer.Enums;

namespace ConsoleMusicPlayer.Frontend
{
    internal class Messages
    {
        private SetColorCombo _colorContrast;
        private AsciiArt _asciiArt;
        private Positions _position;
        private int previousVolume = 0;
        public EasterEgg _visualizer;

        public Messages(SetColorCombo colorContrast, AsciiArt asciiArt, Positions handler, EasterEgg visualizer)
        {
            _colorContrast = colorContrast;
            _asciiArt = asciiArt;
            _position = handler;
            _visualizer = visualizer;
        }

        // ++++++++++ RENDERS ++++++++++++++
        public void RenderTitle()
        {
            _colorContrast.ColoringFullConsole();
            Console.Clear();
            int edgeDistance = (int)RenderControls.TitleLeftLimiter;
            string[] asciiBlock = _asciiArt.TitleBlock();
            _colorContrast.ColoringTitle();
            for (int i = 0; i < (int)RenderControls.TitleHeight; i++)
            {
                Console.SetCursorPosition(edgeDistance, i);
                Console.Write($"{asciiBlock[i]}");
            }
        }

        public void ResetRenderSongList()
        {
            ClearSongSelectMenu();
        }

        public void RenderSongList(string[] getSongsFromMusicFolder, int itemsToRender, int presentMusicFolderLength, int currentIndex)
        {
            RenderSongListBackground();
            _colorContrast.ColoringDisplay();
            {
                for (int i = 0; i < itemsToRender; i++)
                {
                    Console.SetCursorPosition((int)RenderControls.MusicFolderFetchLeftLimiter, (int)RenderControls.MusicFolderFetchHeight + i);
                    string temp = getSongsFromMusicFolder[currentIndex + i].Substring(presentMusicFolderLength + 1);
                    if (temp.Length > 40)
                    {
                        Console.Write(getSongsFromMusicFolder[currentIndex + i].Substring(presentMusicFolderLength + 1, 40));
                    }
                    else
                    {
                        Console.Write(getSongsFromMusicFolder[currentIndex + i].Substring(presentMusicFolderLength + 1, temp.Length));
                    }
                }
            }
        }

        private void RenderMetaBackground(int length)
        {
            _colorContrast.ColoringFullConsole();
            int top = 8;
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{new string(' ', 52)}");
                Console.SetCursorPosition(3, top);
                top += 1;
            }
            top = 8;
            if (length > 39)
            {
                length = 39;
            }
            _position.MetadataBackdropPosition();
            _colorContrast.ColoringBackdrops();

            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{new string(' ', length + 13)}");
                Console.SetCursorPosition(3, top);
                top += 1;
            }
        }

        private void RenderSongListBackground()
        {
            _position.GetMusicBackdropPosition();
            _colorContrast.ColoringBackdrops();

            for (int i = 0; i < (int)RenderControls.MusicBackDropHeight; i++)
            {
                Console.Write($"{new string(' ', (int)RenderControls.MusicBackDropWidth)}\n");
                Console.CursorLeft = (int)RenderControls.MusicBackDropLeftLimiter;
            }
        }

        public void RenderAsciiMenu(int activeColor = -1)
        {
            _colorContrast.ColoringMenu();
            int currentBlock = 0;
            int menuBlockPosition = 0;
            int topCursor = (int)RenderControls.OptionsVerticalPlacement;
            int leftCursor = (int)RenderControls.OptionsLeftLimiter;
            List<string> menuBlock = _asciiArt.MenuBlock();

            for (int i = 0; i < (int)RenderControls.OptionsCount; i++)
            {
                RenderAsciiMenuColorPicker(currentBlock, activeColor);
                for (int y = 0; y < (int)RenderControls.OptionsHeight; y++)
                {
                    Console.SetCursorPosition(leftCursor, topCursor + y);
                    Console.Write($"{menuBlock[menuBlockPosition + y]}");
                }
                leftCursor += 11;
                menuBlockPosition += 6;
                currentBlock += 1;
                _colorContrast.ColoringMenu();
            }
        }

        private void RenderAsciiMenuColorPicker(int currentBlock, int activeColor)
        {
            if (currentBlock == activeColor && activeColor != -1)
            {
                _colorContrast.ColoringMenuEnabledItem();
            }
            else
            {
                _colorContrast.ColoringMenu();
            }
        }

        public void RenderVolumeBar(int volume)
        {
            volume = volume < 1 ? volume = 1 : volume > 100 ? volume = 100 : volume;
            _colorContrast.ColoringVolumeBar();
            _position.VolumeBarPosition();
            int top = 16;
            int left = 7;
            Console.Write(" VOL:==\n       ");
            string volumeBar = _asciiArt.VolumeBar();
            _position.VolumeUnfilledBar();
            string volumeBarSupport = _asciiArt.VolumePlaceHolder();
            for (int i = 0; i < volume; i++)
            {
                Console.SetCursorPosition(left, top);
                Console.Write(volumeBar);
                Console.SetCursorPosition(left, top + 1);
                Console.Write(volumeBarSupport);
                left++;
            }
            Console.Write("++");
            Console.SetCursorPosition(left, top);
            Console.Write("++");
            ClearPreviousBar(volume, top, left);
        }

        public void RenderVisualArt()
        {
            _visualizer.RenderVisualizer();
            Console.CursorVisible = true;
            Console.Clear();
        }

        // ++++++++++ PROMPTS ++++++++++++++

        public void DisplaySongSelect()
        {
            _colorContrast.ColoringInformative();
            _position.SongSelectPosition();
            int top = 9;
            int left = 64;
            for (int i = 0; i < 5; i++)
            {
                Console.Write(i + 1);
                top += 1;
                Console.SetCursorPosition(left, top);
            }
            Console.SetCursorPosition(left - 5, top);
            Console.Write("Please Select an option [ ] (0) for cycle trough");
        }

        public void DisplayLoadManually()
        {
            _position.PositionMp3Message();
            _colorContrast.ColoringMenu();
            Console.Write("§.mp3 added by default§");
            _colorContrast.ColoringTitle();
            _position.PositionAskForPath();
            string folderMessage = $"Please enter a path in your Musicfolder now in default: {Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)}\\";
            Console.WriteLine(folderMessage);
            _position.PositionAskForPath();
            Console.CursorLeft += folderMessage.Length;
        }

        public int DisplayVolumeMenu()
        {
            bool choiceTest = false;
            string message = "[ ] <- set volume from 1 - 100";
            int choice = 0;
            while (choiceTest == false)
            {
                _colorContrast.ColoringInformative();
                _position.VolumeMenuPosition();
                Console.WriteLine(message);

                Console.SetCursorPosition(1, 18);
                choiceTest = int.TryParse(Console.ReadLine(), out choice);
                if (choiceTest == false)
                {
                    _colorContrast.ColoringDisplay();
                    _position.VolumeMenuErrorPosition(message);
                    Console.Write("Foute invoer - opnieuw");
                }
            }
            ClearVolumeMenuError(message);
            _colorContrast.ColoringTitle();
            return choice;
        }

        // ++++++++++ INFODISPLAY ++++++++++++++
        public void DisplayMetaData(string[] metadata)
        {
            int max = FindMaxItemLenght(metadata);
            RenderMetaBackground(max);
            _position.MetaDataPosition();
            int top = 9;
            int left = 5;
            for (int i = 0; i < metadata.Length; i += 2)
            {
                int lengthParam = metadata[i].Length;
                int lengthValeu = metadata[i + 1].Length;
                if (lengthValeu > 38)
                {
                    lengthValeu = 38;
                }
                Console.SetCursorPosition(left, top);

                if (!string.IsNullOrWhiteSpace(metadata[i + 1]))
                {
                    Console.Write($"{metadata[i].Substring(0, lengthParam)}{metadata[i + 1].Substring(0, lengthValeu)}");
                }
                else
                {
                    Console.Write($"{metadata[i].Substring(0, lengthParam)} onbekend");
                }
                top += 1;
            }
        }

        private int FindMaxItemLenght(string[] metadata)
        {
            int max = 0;
            for (int i = 0; i < metadata.Length; i++)
            {
                if (metadata[i] != null)
                {
                    string tempValue = metadata[i];
                    if (tempValue.Length > max)
                    {
                        max = tempValue.Length;
                    }
                }
            }
            return max;
        }

        public void DisplayNowPlaying(string songName)
        {
            _position.PositionNowPlaying();
            _colorContrast.ColoringInformative();
            Console.WriteLine($"Now Playing {songName}");
            _colorContrast.ColoringTitle();
        }

        public void DisplayNowPaused(string songName)
        {
            _position.PositionNowPaused(songName.Length + 10);
            _colorContrast.ColoringToggleButton();
            Console.Write(" - Now paused");
        }

        public void DisplayNowMuted()
        {
            _position.PositionNowmuted();
            _colorContrast.ColoringToggleButton();
            Console.WriteLine("-----Now Muted------");
        }

        public void DisplayMenuChoice()
        {
            _colorContrast.ColoringMenu();
            char choice = ' ';
            Console.SetCursorPosition(0, 18);
            Console.Write("[ ]");
            _colorContrast.ColoringFullConsole();
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(1, 18);
        }

        public char DisplayRetrySongInput()
        {
            _position.RetrySongInputPosition();
            _colorContrast.ColoringDisplay();
            Console.Write("Try again? y/n");
            char choice = (char)Console.Read();
            return choice;
        }

        // ++++++++++ ERRORS ++++++++++++++
        public void DisplayFileNotFoundError()
        {
            _colorContrast.ColoringMenu();
            Console.SetCursorPosition(0, 8);
            Console.Write(_asciiArt.ErrorNoSong());
            _colorContrast.ColoringTitle();
        }

        public void DisplayLoadingError()
        {
            _colorContrast.ColoringInformative();
            _position.PositionLoadError();
            Console.Write(new string(' ', 50));
            _position.PositionLoadError();
            Console.WriteLine("No song loaded yet, nice try though!");
        }

        // ++++++++++ CLEAR ERRORS ++++++++++++++
        public void ClearErrorMessage()
        {
            _colorContrast.ColoringFullConsole();
            Console.SetCursorPosition(0, 9);
            Console.Write(new string(' ', 64));
            Console.SetCursorPosition(0, 10);
            Console.Write(new string(' ', 64));
        }

        public void ClearNoFileLoadedMessage()
        {
            _colorContrast.ColoringFullConsole();
            _position.PositionLoadError();
            Console.Write(new string(' ', Console.WindowWidth));
            _colorContrast.ColoringTitle();
        }

        private void ClearVolumeMenuError(string message)
        {
            _position.VolumeMenuErrorPosition(message);
            _colorContrast.ColoringFullConsole();
            Console.Write(new string(' ', Console.WindowWidth));
        }

        // ++++++++++ CLEAR INFODISPLAY ++++++++++++++
        public void ClearDisplayMetaData() 
        {
            _colorContrast.ColoringFullConsole();
            int top = 8;
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{new string(' ', 52)}");
                Console.SetCursorPosition(3, top);
                top += 1;
            }
        }
        private void ClearPreviousBar(int volume, int top, int left)
        {
            if (previousVolume > volume)
            {
                _colorContrast.ColoringFullConsole();
                Console.SetCursorPosition(left, top);
                Console.Write(new string(' ', previousVolume));
                Console.SetCursorPosition(left, top + 1);
                Console.Write(new string(' ', previousVolume));
            }
            previousVolume = volume;
        }

        public void ClearMuteMessage()
        {
            _colorContrast.ColoringFullConsole();
            _position.PositionNowmuted();
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void ClearPauseMessage()
        {
            Console.SetCursorPosition((int)RenderControls.PauseMessageLeftLimiter, (int)RenderControls.PauseMessageHeight);
            _colorContrast.ColoringFullConsole();
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void ClearVolumeMenu()
        {
            _position.VolumeMenuPosition();
            _colorContrast.ColoringFullConsole();
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void ClearRetryInputMessage()
        {
            _colorContrast.ColoringFullConsole();

            _position.RetrySongInputPosition();
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 5);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void ClearLoadMessage()
        {
            _colorContrast.ColoringFullConsole();
            _position.PositionAskForPath();
            Console.Write(new string(' ', Console.WindowWidth));
            _position.PositionMp3Message();
            Console.Write(new string(' ', Console.WindowWidth));
            _position.PositionNowPlaying();
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void ClearSongSelectMenu()
        {
            _colorContrast.ColoringDisplay();
            Console.SetCursorPosition((int)RenderControls.MusicFolderFetchLeftLimiter, (int)RenderControls.MusicFolderFetchHeight);
            int topCursor = (int)RenderControls.MusicFolderFetchHeight;
            int leftCursor = (int)RenderControls.MusicFolderFetchLeftLimiter - 2;
            for (int i = 0; i < (int)RenderControls.NumberOfSongsInDisplay; i++)
            {
                _colorContrast.ColoringFullConsole();
                Console.Write(' ');
                _colorContrast.ColoringDisplay();

                Console.Write(new string(' ', 40));
                Console.SetCursorPosition(leftCursor, topCursor + i);
            }
            Console.SetCursorPosition(leftCursor - 5, topCursor + 5);
            _colorContrast.ColoringFullConsole();
            Console.Write(new string(' ', 6));
            _colorContrast.ColoringDisplay();
            Console.Write(new string(' ', 41));
        }
    }
}