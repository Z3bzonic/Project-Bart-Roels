using WMPLib;

namespace ConsoleMusicPlayer
{
    internal class ConsoleMediaPlayer
    {
        private WindowsMediaPlayer _player;
        private Messages _messages;

        private bool toggleStatePlayer = false;
        private bool toggleMutePlayer = false;
        private bool pathPresent = false;
        public int volume = 50;
        private string getSong = "";
        public ConsoleMediaPlayer(WindowsMediaPlayer player, Messages messages)
        {
            _player = player;
            _messages = messages;
        }
        //Handler errorHandling = new Handler();

        public void LoadSong()
        {
            getSong = _messages.PrintAskForPath();
            FileInfo fileInfo = new FileInfo(getSong);

            if (fileInfo.Exists)
            {
                _messages.ResetErrorMessage();
                _player.settings.autoStart = false;
                _player.URL = getSong;
                pathPresent = true;
            }
            else
            {
                _messages.ErrorFileNotFound();
                LoadSong();
            }
        }

        //public void StartLoadedSong()
        //{
        //    //                player.MediaError +=
        //    //    new WMPLib._WMPOCXEvents_MediaErrorEventHandler(PlayerError); //tried making main block in program.cs a method but you not invoke from PlayerError as error handling Method
        //    player.controls.play();
        //}

        public void TogglePlayPause()
        {
            if (toggleStatePlayer == false)
            {
                _player.controls.play();
                toggleStatePlayer = true;
                _messages.RemovePauseMessage();
                _messages.NowPlaying(getSong);
            }
            else
            {
                _player.controls.pause();
                toggleStatePlayer = false;
                _messages.NowPaused(getSong);
            }
        }

        public void StopSong()
        {
            _player.controls.stop();
        }

        public void SetInitialVolume() 
        {
            _messages.VolumeBarControl(volume);
            _player.settings.volume = volume;
        }
        public void ChangeVolume() 
        {
            volume = _messages.VolumeMenu();
            _player.settings.volume = volume;
            _messages.VolumeBarControl(volume);
            _messages.RemoveVolumeMenu();
        }

        public void MutePlayer()
        {
            if (toggleMutePlayer == false)
            {
                _player.settings.mute = true;
                toggleMutePlayer = true;
                _messages.NowMuted();
            }
            else
            {
                _player.settings.mute = false;
                toggleMutePlayer = false;
                _messages.RemoveMuteMessage();
                
                _messages.NowPlaying(getSong);
            }
        }

        public void RetrieveMetaData() 
        {
            string[] songAttributes = { _player.currentMedia.name, _player.currentMedia.durationString };
            _messages.MetaBackdrop();
            //messages.PrintMetaData(songAttributes);

        }

        public void ChoicePatchTrough(string choice)
        {
            switch (choice)
            {
                case "p":
                    if (pathPresent == true)
                    {
                        SetInitialVolume();
                        TogglePlayPause();
                    }
                    else
                    {
                        _messages.LoadingError();
                    }
                    break;

                case "s":
                    pathPresent = false;
                    StopSong();
                    break;

                case "l":
                    LoadSong();
                    toggleMutePlayer = false;
                    toggleStatePlayer = false;
                    RetrieveMetaData();
                    break;

                case "m":
                    MutePlayer();
                    break;

                case "n":
                    break;

                case "v":ChangeVolume();
                    _messages.RemoveVolumeMenu();
                    break;

                case "x":
                    break;

                default:
                    break;
            }
        }
    }
}