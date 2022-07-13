using ConsoleMusicPlayer.Frontend;
using WMPLib;

namespace ConsoleMusicPlayer.Backend
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
                    TogglePlay();
                }
                else
                {
                    TogglePause();
                }
            }
        }

        private void TogglePlay() 
        {
            _player.controls.play();
            toggleStatePlayer = true;
            _messages.ClearPauseMessage();
            _messages.DisplayNowPlaying(getSong);
            _messages.RenderAsciiMenu(0);
        }

        private void TogglePause()
        {
            _player.controls.pause();
            toggleStatePlayer = false;
            _messages.DisplayNowPaused(getSong);
            _messages.RenderAsciiMenu(-1);
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
            _messages.RenderAsciiMenu((int)Enums.RenderControls.OptionsCount - 2);
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
                ToggleMuteOn();
            }
            else
            {
                ToggleMuteOff();
            }
        }

        private void ToggleMuteOn() 
        {
            _player.settings.mute = true;
            toggleMutePlayer = true;
            _messages.DisplayNowMuted();
            _messages.RenderAsciiMenu((int)Enums.RenderControls.OptionsCount - 3);
        }

        private void ToggleMuteOff()
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
            }
        }

        private void IncreaseCycleDisplay()
        {
            _messages.ResetRenderSongList();
            PrepareSongList(_fileFunctions.GetMusicFromFolder());
        }

        private void RetrieveMetaData(IWMPMedia media)
        {
            _player.controls.play();
            _player.settings.volume = 0;
            Thread.Sleep(100);          
            _player.controls.pause();
            string[] songAttributes = new string[10];
            if (_player.currentMedia != null)
            {
                Thread.Sleep(1000);
                songAttributes[0] = "Title   :";
                songAttributes[1] = $" {media.getItemInfo("Title")}";
                songAttributes[2] = "Artist  :";
                songAttributes[3] = $" {media.getItemInfo("Author")}";
                songAttributes[4] = "Album   :";
                songAttributes[5] = $" {media.getItemInfo("Album")}";
                songAttributes[6] = "Genre   :";
                songAttributes[7] = $" {media.getItemInfo("WM/Genre")}";
                songAttributes[8] = "Lengte  : ";
                songAttributes[9] = media.durationString;
                _messages.DisplayMetaData(songAttributes);
            }
        }


        private void SongPicker()
        {
            string[] songs = _fileFunctions.GetMusicFromFolder();
            _messages.DisplaySongSelect();
            Console.SetCursorPosition(84, 14);
            int choice = -1;
            bool testChoice = int.TryParse(Console.ReadLine(), out choice);
            if (testChoice == false) 
            { 
                choice -= 1; 
            }

            string option = "";
            switch (choice)
            {
                case 0:
                    IncreaseCycleDisplay();
                    break;

                case 1:
                    _messages.ClearDisplayMetaData();
                    option = songs[0 + songListCycle*5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 2:
                    _messages.ClearDisplayMetaData();
                    option = songs[1 + songListCycle*5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 3:
                    _messages.ClearDisplayMetaData();
                    option = songs[2 + songListCycle*5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 4:
                    _messages.ClearDisplayMetaData();
                    option = songs[3 + songListCycle*5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 5:
                    _messages.ClearDisplayMetaData();
                    option = songs[4 + songListCycle*5 - 5];
                    RetrieveLoadData(option);
                    break;

                default:
                    _messages.ClearSongSelectMenu();
                    break;
            }
        }

        public void PrepareSongList(string[] getSongsFromMusicFolder)
        {

            int currentIndex = songListCycle * 5;
            int songListLength = getSongsFromMusicFolder.Length;
            if (Math.Ceiling(songListLength / 5.0) <= songListCycle+1)
            {

                songListCycle = 0;
                currentIndex = songListCycle * 5;
            }
                songListCycle += 1;
            int itemsToRender = songListLength - songListCycle * 5 > 5 ? 5 : (songListLength - songListCycle * 5) % 5;
            int presentMusicFolderLength = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic).Length;
            _messages.RenderSongList(getSongsFromMusicFolder, itemsToRender, presentMusicFolderLength, currentIndex);

        }

        public void ChoicePatchTrough(int choice)
        {
            switch (choice)
            {
                case (int)UserOptions.Play_Pause:
                    if (pathPresent == true)
                    {
                        RetrieveMetaData(_player.currentMedia);
                        SetInitialVolume();
                        TogglePlayPause();
                    }
                    else
                    {
                        _messages.DisplayLoadingError();
                    }
                    break;

                case (int)UserOptions.Stop:
                    pathPresent = false;
                    StopSong();
                    break;

                case (int)UserOptions.Load:
                    _messages.RenderAsciiMenu((int)Enums.RenderControls.OptionsCount - 5);
                    RetrieveLoadData();

                    if (getSong != "")
                    {
                        toggleMutePlayer = false;
                        toggleStatePlayer = false;
                    }
                    _messages.ClearLoadMessage();
                    _messages.RenderAsciiMenu();
                    break;

                case (int)UserOptions.Mute_Unmute:
                    MutePlayer();
                    break;

                case (int)UserOptions.LoadFromList:
                    _messages.RenderAsciiMenu((int)Enums.RenderControls.OptionsCount - 4);
                    SongPicker();
                    if (getSong != "")
                    {
                        toggleMutePlayer = false;
                        toggleStatePlayer = false;
                    }
                    _messages.RenderAsciiMenu();
                    break;

                case (int)UserOptions.Volume:
                    ChangeVolume();
                    _messages.ClearVolumeMenu();
                    break;

                case (int)UserOptions.Visualizer: //Easteregg option 8 : Do not run while sensitive to rapid changing colors / epilepsy
                    _messages.RenderVisualArt();
                    _messages.RenderTitle();
                    _messages.RenderAsciiMenu();
                    PrepareSongList(_fileFunctions.GetMusicFromFolder());
                    break;

                case (int)UserOptions.Exit:
                    return;
                default:
                    break;
            }
        }
    }
}