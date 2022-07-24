using ConsoleMusicPlayer.Common.Enums;
using ConsoleMusicPlayer.Common.Interfaces;
using WMPLib;

namespace ConsoleMusicPlayer.Business
{
    public class ConsoleMediaPlayer
    {
        private readonly WindowsMediaPlayer _player;
        private readonly IMessages _messages;
        private readonly StatesDTO _statesDTO;

        public ConsoleMediaPlayer(IMessages messages, StatesDTO statesDTO)
        {
            _player = new WindowsMediaPlayer();
            _messages = messages;
            _statesDTO = statesDTO;
        }

        private void TogglePlayPause()
        {
            if (!string.IsNullOrEmpty(_statesDTO.SongName))
            {
                if (_statesDTO.ToggleStatePlayer)
                    TogglePause();
                else
                    TogglePlay();
            }
        }

        private void TogglePlay()
        {
            _player.controls.play();
            _statesDTO.ToggleStatePlayer = true;
            _messages.ClearPauseMessage();
            _messages.DisplayNowPlaying(_statesDTO.SongName);
            _messages.RenderAsciiMenu(0);
        }

        private void TogglePause()
        {
            _player.controls.pause();
            _statesDTO.ToggleStatePlayer = false;
            _messages.DisplayNowPaused(_statesDTO.SongName);
            _messages.RenderAsciiMenu(-1);
        }

        private void StopSong()
        {
            _player.controls.stop();
            _messages.ClearLoadMessage();
            _messages.RenderAsciiMenu(1);
            Thread.Sleep(300);
            _messages.RenderAsciiMenu();
            _statesDTO.SongName = "";
        }

        private void SetInitialVolume()
        {
            _messages.RenderVolumeBar(_statesDTO.Volume);
            _player.settings.volume = _statesDTO.Volume;
        }

        private void ChangeVolume()
        {
            _messages.RenderAsciiMenu((int)RenderControls.OptionsCount - 2);
            _statesDTO.Volume = _messages.DisplayVolumeMenu();
            _player.settings.volume = _statesDTO.Volume;
            _messages.RenderVolumeBar(_statesDTO.Volume);
            _messages.ClearVolumeMenu();
            _messages.RenderAsciiMenu();
        }

        private void MutePlayer()
        {
            if (_statesDTO.ToggleMutePlayer == false)
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
            _statesDTO.ToggleMutePlayer = true;
            _messages.DisplayNowMuted();
            _messages.RenderAsciiMenu((int)RenderControls.OptionsCount - 3);
        }

        private void ToggleMuteOff()
        {
            _player.settings.mute = false;
            _statesDTO.ToggleMutePlayer = false;
            _messages.ClearMuteMessage();
            if (_statesDTO.SongName != "")
            {
                _messages.DisplayNowPlaying(_statesDTO.SongName);
            }
            _messages.RenderAsciiMenu();
        }

        private string LoadSong(string loadedSong = "")
        {
            if (loadedSong == "")
            {
                _messages.DisplayLoadManually();
                
                var fileName = Console.ReadLine();

                string fullPath = FileFunctions.GetFullFilePath(fileName);
                bool exists = FileFunctions.FileExists(fileName);
                fullPath = exists ? fullPath : LoadSong();
            }
            return loadedSong;
        }

        private void RetrieveLoadData(string listChoice = "")
        {
            //string fetchData = _fileFunctions.LoadSong(listChoice);
            string fetchData = LoadSong(listChoice);
            if (fetchData != "")
            {
                _statesDTO.SongName = fetchData;
                _player.settings.autoStart = false;
                _player.URL = fetchData;
                _statesDTO.PathPresent = true;
            }
        }

        private void IncreaseCycleDisplay()
        {
            _messages.ResetRenderSongList();
            PrepareSongList(FileFunctions.GetMusicFromFolder());
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
            var songs = FileFunctions.GetMusicFromFolder();
            _messages.DisplaySongSelect();
            Console.SetCursorPosition(84, 14);

            if (!int.TryParse(Console.ReadLine(), out int choice))
                choice -= 1;

            string option;
            switch (choice)
            {
                case 0:
                    IncreaseCycleDisplay();
                    break;

                case 1:
                    _messages.ClearSongSelectOptions();
                    _messages.ClearDisplayMetaData();
                    option = songs[0 + _statesDTO.SongListCycle * 5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 2:
                    _messages.ClearSongSelectOptions();
                    _messages.ClearDisplayMetaData();
                    option = songs[1 + _statesDTO.SongListCycle * 5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 3:
                    _messages.ClearSongSelectOptions();
                    _messages.ClearDisplayMetaData();
                    option = songs[2 + _statesDTO.SongListCycle * 5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 4:
                    _messages.ClearSongSelectOptions();
                    _messages.ClearDisplayMetaData();
                    option = songs[3 + _statesDTO.SongListCycle * 5 - 5];
                    RetrieveLoadData(option);
                    break;

                case 5:
                    _messages.ClearSongSelectOptions();
                    _messages.ClearDisplayMetaData();
                    option = songs[4 + _statesDTO.SongListCycle * 5 - 5];
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
            int currentIndex = _statesDTO.SongListCycle * 5;
            int songListLength = getSongsFromMusicFolder.Length;
            if (Math.Ceiling(songListLength / 5.0) <= _statesDTO.SongListCycle + 1)
            {
                _statesDTO.SongListCycle = 0;
                currentIndex = _statesDTO.SongListCycle * 5;
            }
            _statesDTO.SongListCycle++;
            int itemsToRender = songListLength - _statesDTO.SongListCycle * 5 > 5 ? 5 : (songListLength - _statesDTO.SongListCycle * 5) % 5;
            int presentMusicFolderLength = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic).Length;
            _messages.RenderSongList(getSongsFromMusicFolder, itemsToRender, presentMusicFolderLength, currentIndex);
        }

        public void ChoicePatchTrough(UserOptions userOption)
        {
            //switch (userOption)
            //{
            //    case UserOptions.None:
            //        break;
            //    case UserOptions.Play_Pause:
            //        break;
            //    case UserOptions.Stop:
            //        break;
            //    case UserOptions.Load:
            //        break;
            //    case UserOptions.Mute_Unmute:
            //        break;
            //    case UserOptions.LoadFromList:
            //        break;
            //    case UserOptions.Volume:
            //        break;
            //    case UserOptions.Exit:
            //        break;
            //    case UserOptions.Visualizer:
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException(nameof(choice));
            //}

            //try
            //{
            //    //enter fileename

            //    //file exists?
            //    throw new BusinessException();

            //    //open file

            //    //play file
            //    //WMP
                
            //}
            //catch (BusinessException ex)
            //{
            //    //log exception
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    //log exception
            //    throw;
            //}
            //finally
            //{
            //    //dispose WMP lib
            //}

            switch (userOption)
            {
                case UserOptions.Play_Pause:
                    if (_statesDTO.PathPresent)
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
                case UserOptions.Stop:
                    _statesDTO.PathPresent = false;
                    StopSong();
                    break;
                case UserOptions.Load:
                    _messages.RenderAsciiMenu((int)RenderControls.OptionsCount - 5);
                    RetrieveLoadData();

                    if (!string.IsNullOrEmpty(_statesDTO.SongName))
                    {
                        _statesDTO.ToggleMutePlayer = false;
                        _statesDTO.ToggleStatePlayer = false;
                    }
                    _messages.ClearLoadMessage();
                    _messages.RenderAsciiMenu();
                    break;
                case UserOptions.Mute_Unmute:
                    MutePlayer();
                    break;
                case UserOptions.LoadFromList:
                    _messages.RenderAsciiMenu((int)RenderControls.OptionsCount - 4);
                    SongPicker();
                    if (!string.IsNullOrEmpty(_statesDTO.SongName))
                    {
                        _statesDTO.ToggleMutePlayer = false;
                        _statesDTO.ToggleStatePlayer = false;
                    }
                    _messages.RenderAsciiMenu();
                    break;
                case UserOptions.Volume:
                    ChangeVolume();
                    _messages.ClearVolumeMenu();
                    break;
                case UserOptions.Visualizer: //Easteregg option 8 : Do not run while sensitive to rapid changing colors / epilepsy
                    _messages.RenderVisualArt();
                    _messages.RenderTitle();
                    _messages.RenderAsciiMenu();
                    PrepareSongList(FileFunctions.GetMusicFromFolder());
                    break;
                case UserOptions.Exit:
                    return;
                default:
                    break;
            }
        }
    }

    public class BusinessException : Exception
    {
    }
}