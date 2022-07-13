using ConsoleMusicPlayer.Frontend;
using WMPLib;

namespace ConsoleMusicPlayer.Backend
{
    internal class ConsoleMediaPlayer
    {
        private WindowsMediaPlayer _player;
        private Messages _messages;
        private FileFunctions _fileFunctions;
        private StatesDTO _stationsDTO;

        public ConsoleMediaPlayer(WindowsMediaPlayer player, Messages messages, FileFunctions fileFunctions, StatesDTO stationsDTO)
        {
            _player = player;
            _messages = messages;
            _fileFunctions = fileFunctions;
            _stationsDTO = stationsDTO;
        }

        private void TogglePlayPause()
        {
            if (_stationsDTO.getSong != "")
            {
                if (_stationsDTO.toggleStatePlayer == false)
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
            _stationsDTO.toggleStatePlayer = true;
            _messages.ClearPauseMessage();
            _messages.DisplayNowPlaying(_stationsDTO.getSong);
            _messages.RenderAsciiMenu(0);
        }

        private void TogglePause()
        {
            _player.controls.pause();
            _stationsDTO.toggleStatePlayer = false;
            _messages.DisplayNowPaused(_stationsDTO.getSong);
            _messages.RenderAsciiMenu(-1);
        }

        private void StopSong()
        {
            _player.controls.stop();
            _messages.ClearLoadMessage();
            _messages.RenderAsciiMenu(1);
            Thread.Sleep(300);
            _messages.RenderAsciiMenu();
            _stationsDTO.getSong = "";
        }

        private void SetInitialVolume()
        {
            _messages.RenderVolumeBar(_stationsDTO.volume);
            _player.settings.volume = _stationsDTO.volume;
        }

        private void ChangeVolume()
        {
            _messages.RenderAsciiMenu((int)Enums.RenderControls.OptionsCount - 2);
            _stationsDTO.volume = _messages.DisplayVolumeMenu();
            _player.settings.volume = _stationsDTO.volume;
            _messages.RenderVolumeBar(_stationsDTO.volume);
            _messages.ClearVolumeMenu();
            _messages.RenderAsciiMenu();
        }

        private void MutePlayer()
        {
            if (_stationsDTO.toggleMutePlayer == false)
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
            _stationsDTO.toggleMutePlayer = true;
            _messages.DisplayNowMuted();
            _messages.RenderAsciiMenu((int)Enums.RenderControls.OptionsCount - 3);
        }

        private void ToggleMuteOff()
        {
            _player.settings.mute = false;
            _stationsDTO.toggleMutePlayer = false;
            _messages.ClearMuteMessage();
            if (_stationsDTO.getSong != "")
            {
                _messages.DisplayNowPlaying(_stationsDTO.getSong);
            }
            _messages.RenderAsciiMenu();
        }

        private void RetrieveLoadData(string listChoice = "")
        {
            string fetchData = _fileFunctions.LoadSong(listChoice);
            if (fetchData != "")
            {
                _stationsDTO.getSong = fetchData;
                _player.settings.autoStart = false;
                _player.URL = fetchData;
                _stationsDTO.pathPresent = true;
            }
        }

        private void IncreaseCycleDisplay()
        {
            _messages.ResetRenderSongList();
            PrepareSongList(_fileFunctions.GetMusicFromFolder());
        }

        private void RetrieveMetaData(IWMPMedia media)
        {
            string[] songAttributes = new string[10];
            if (_player.currentMedia != null)
            {
                _messages.DisplayLoadingMetaData();
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
                    _messages.ClearSongSelectOptions();
                    _messages.ClearDisplayMetaData();
                    option = songs[0 + _stationsDTO.songListCycle * 5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 2:
                    _messages.ClearSongSelectOptions();
                    _messages.ClearDisplayMetaData();
                    option = songs[1 + _stationsDTO.songListCycle * 5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 3:
                    _messages.ClearSongSelectOptions();
                    _messages.ClearDisplayMetaData();
                    option = songs[2 + _stationsDTO.songListCycle * 5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 4:
                    _messages.ClearSongSelectOptions();
                    _messages.ClearDisplayMetaData();
                    option = songs[3 + _stationsDTO.songListCycle * 5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 5:
                    _messages.ClearSongSelectOptions();
                    _messages.ClearDisplayMetaData();
                    option = songs[4 + _stationsDTO.songListCycle * 5 - 5];
                    RetrieveLoadData(option);
                    break;

                default:
                    _messages.ClearSongSelectOptions();
                    _messages.ClearSongSelectMenu();
                    break;
            }
        }

        public void PrepareSongList(string[] getSongsFromMusicFolder)
        {
            int currentIndex = _stationsDTO.songListCycle * 5;
            int songListLength = getSongsFromMusicFolder.Length;
            if (Math.Ceiling(songListLength / 5.0) <= _stationsDTO.songListCycle + 1)
            {
                _stationsDTO.songListCycle = 0;
                currentIndex = _stationsDTO.songListCycle * 5;
            }
            _stationsDTO.songListCycle += 1;
            int itemsToRender = songListLength - _stationsDTO.songListCycle * 5 > 5 ? 5 : (songListLength - _stationsDTO.songListCycle * 5) % 5;
            int presentMusicFolderLength = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic).Length;
            _messages.RenderSongList(getSongsFromMusicFolder, itemsToRender, presentMusicFolderLength, currentIndex);
        }

        public void ChoicePatchTrough(int choice)
        {
            switch (choice)
            {
                case (int)UserOptions.Play_Pause:
                    if (_stationsDTO.pathPresent == true)
                    {
                        SetInitialVolume();
                        TogglePlayPause();
                        RetrieveMetaData(_player.currentMedia);
                    }
                    else
                    {
                        _messages.DisplayLoadingError();
                    }
                    break;

                case (int)UserOptions.Stop:
                    _stationsDTO.pathPresent = false;
                    StopSong();
                    break;

                case (int)UserOptions.Load:
                    _messages.RenderAsciiMenu((int)Enums.RenderControls.OptionsCount - 5);
                    RetrieveLoadData();

                    if (_stationsDTO.getSong != "")
                    {
                        _stationsDTO.toggleMutePlayer = false;
                        _stationsDTO.toggleStatePlayer = false;
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
                    if (_stationsDTO.getSong != "")
                    {
                        _stationsDTO.toggleMutePlayer = false;
                        _stationsDTO.toggleStatePlayer = false;
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