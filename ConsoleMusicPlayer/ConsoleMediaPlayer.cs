using WMPLib;

namespace ConsoleMusicPlayer
{
    internal class ConsoleMediaPlayer
    {
        private WindowsMediaPlayer _player;
        private Messages _messages;
        private FileFunctions _fileFunctions;
        private bool toggleStatePlayer = false;
        private bool toggleMutePlayer = false;
        public bool pathPresent = false;
        public int volume = 50;
        public string getSong = "";
        private int songListCycle = 0;

        public ConsoleMediaPlayer(WindowsMediaPlayer player, Messages messages, FileFunctions fileFunctions)
        {
            _player = player;
            _messages = messages;
            _fileFunctions = fileFunctions;
        }

        private void TogglePlayPause()
        {
            if (getSong != "")
            {
                if (toggleStatePlayer == false)
                {
                    _player.controls.play();
                    toggleStatePlayer = true;
                    _messages.ClearPauseMessage();
                    _messages.DisplayNowPlaying(getSong);
                    _messages.RenderAsciiMenu(0);
                }
                else
                {
                    _player.controls.pause();
                    toggleStatePlayer = false;
                    _messages.DisplayNowPaused(getSong);
                    _messages.RenderAsciiMenu(-1);
                }
            }
        }

        private void StopSong()
        {
            _player.controls.stop();
            _messages.ClearLoadMessage();
            _messages.RenderAsciiMenu(1);
            Thread.Sleep(300);
            _messages.RenderAsciiMenu();
            getSong = "";
        }

        private void SetInitialVolume()
        {
            _messages.RenderVolumeBar(volume);
            _player.settings.volume = volume;
        }

        private void ChangeVolume()
        {
            _messages.RenderAsciiMenu((int)Enums.RenderControls.OptionsCount-3);
            volume = _messages.DisplayVolumeMenu();
            _player.settings.volume = volume;
            _messages.RenderVolumeBar(volume);
            _messages.ClearVolumeMenu();
            _messages.RenderAsciiMenu();
        }

        private void MutePlayer()
        {
            if (toggleMutePlayer == false)
            {
                _player.settings.mute = true;
                toggleMutePlayer = true;
                _messages.DisplayNowMuted();
                _messages.RenderAsciiMenu((int)Enums.RenderControls.OptionsCount - 5);
            }
            else
            {
                _player.settings.mute = false;
                toggleMutePlayer = false;
                _messages.ClearMuteMessage();
                if (getSong != "")
                {
                    _messages.DisplayNowPlaying(getSong);
                }
                _messages.RenderAsciiMenu();
            }
        }

        private void RetrieveLoadData(string listChoice = "")
        {
            string fetchData = _fileFunctions.LoadSong(listChoice);
            while (fetchData == "y")
            {
                fetchData = _fileFunctions.LoadSong();
            }
            if (fetchData != "")
            {
                getSong = fetchData;
                _player.settings.autoStart = false;
                _player.URL = fetchData;
                pathPresent = true;
                _messages.ClearSongSelectMenu();
                _messages.RenderSongList(_fileFunctions.GetMusicFromFolder());
            }
        }

        private void IncreaseCycleDisplay()
        {
            _messages.RenderSongList(_fileFunctions.GetMusicFromFolder(), songListCycle);
        }

        private void RetrieveMetaData()
        {
            string[] songAttributes = new string[10];
            using (var mp3 = new Id3.Mp3(getSong))
            {
                Id3.Id3Tag tag = mp3.GetTag(Id3.Id3TagFamily.Version2X);
                var calcLenght = 0;
                bool canParse = int.TryParse((tag.Length), out calcLenght);
                int calcLenghtMin = 0;
                int calcLengtSec = 0;
                if (calcLenght != 0)
                {
                    calcLenghtMin = (calcLenght / 1000) / 60;
                    calcLengtSec = (calcLenght / 1000) % 60;
                }
                songAttributes[0] = "Title   :";
                songAttributes[1] = $" {tag.Title}";
                songAttributes[2] = "Artist  :";
                songAttributes[3] = $" {tag.Artists}";
                songAttributes[4] = "Album   :";
                songAttributes[5] = $" {tag.Album}";
                songAttributes[6] = "Genre   :";
                songAttributes[7] = $" {tag.Genre}";
                songAttributes[8] = "Lenght  :";
                songAttributes[9] = $" {calcLenghtMin}:{calcLengtSec}  min";
                _messages.DisplayMetaData(songAttributes);
            }
        }

        private void SongPicker()
        {
            string[] songs = _fileFunctions.GetMusicFromFolder();
            _messages.DisplaySongSelect();
            Console.SetCursorPosition(84, 14);
            Console.ReadLine();
            int choice = -1;
            bool testChoice = int.TryParse(Console.ReadLine(), out choice);
            if (testChoice == false) { choice -= 1; }
            string option = "";
            switch (choice)
            {
                case 0:
                    if (songListCycle > songs.Length)
                    {
                        songListCycle = 0;
                    }
                    else
                    {
                        songListCycle += 5;
                    }
                    IncreaseCycleDisplay();
                    break;

                case 1:
                    option = songs[1 + songListCycle];
                    RetrieveLoadData(option);
                    break;

                case 2:
                    option = songs[2 + songListCycle];
                    RetrieveLoadData(option);
                    break;

                case 3:
                    option = songs[3 + songListCycle];
                    RetrieveLoadData(option);
                    break;

                case 4:
                    option = songs[4 + songListCycle];
                    RetrieveLoadData(option);
                    break;

                case 5:
                    option = songs[5 + songListCycle];
                    RetrieveLoadData(option);
                    break;

                default:
                    _messages.ClearSongSelectMenu();
                    _messages.RenderSongList(_fileFunctions.GetMusicFromFolder());
                    break;
            }
        }

        public void ChoicePatchTrough(char choice)
        {
            switch (choice)
            {
                case (char)UserOptions.Play_Pause:
                    if (pathPresent == true)
                    {
                        SetInitialVolume();
                        TogglePlayPause();
                    }
                    else
                    {
                        _messages.DisplayLoadingError();
                    }
                    break;

                case (char)UserOptions.Stop:
                    pathPresent = false;
                    StopSong();
                    break;

                case (char)UserOptions.Load:
                    RetrieveLoadData();

                    if (getSong != "")
                    {
                        toggleMutePlayer = false;
                        toggleStatePlayer = false;
                        RetrieveMetaData();
                    }
                    break;

                case (char)UserOptions.Mute_Unmute:
                    MutePlayer();
                    break;

                //case (char)UserOptions.NextSong:
                //    break;

                case (char)UserOptions.LoadFromList:
                    SongPicker();
                    if (getSong != "")
                    {
                        toggleMutePlayer = false;
                        toggleStatePlayer = false;
                        RetrieveMetaData();
                    }
                    break;

                case (char)UserOptions.Volume:
                    ChangeVolume();
                    _messages.ClearVolumeMenu();
                    break;

                case (char)UserOptions.Colorshuffle:

                    break;

                case (char)UserOptions.Visualizer:

                    break;

                case (char)UserOptions.Exit:
                    break;

                default:
                    break;
            }
        }
    }
}