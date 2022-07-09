namespace ConsoleMusicPlayer
{
    internal class FileFunctions
    {
        private Messages _messages;

        public FileFunctions(Messages messages)
        {
            _messages = messages;
        }

        public string LoadSong(string loadedSong = "")
        {
            if (loadedSong == "")
            {
                _messages.RenderAsciiMenu((int)Enums.RenderControls.OptionsCount - 6);
                _messages.DisplayLoadManually();
                string musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                string songName = $"\\{Console.ReadLine()}.mp3";
                string fullPath = musicFolder + songName;
                fullPath = CheckForFileExist(fullPath);
                return fullPath;
            }
            else
            {
                return loadedSong;
            }
        }

        private string CheckForFileExist(string pathCheck)
        {
            FileInfo fileInfo = new FileInfo(pathCheck);
            if (fileInfo.Exists)
            {
                _messages.ClearErrorMessage();
                _messages.ClearLoadMessage();
                return pathCheck;
            }
            else
            {
                _messages.DisplayFileNotFoundError();
                char choice = (char)_messages.DisplayRetrySongInput();
                if (choice == 'y')
                {
                    return "y";
                }
                else
                {
                    _messages.ClearRetryInputMessage();
                    _messages.ClearErrorMessage();
                    return "";
                }
            }
        }

        public string[] GetMusicFromFolder()
        {
            string presentMusicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            string[] getSongsFromMusicFolder = Directory.GetFiles(presentMusicFolder, "*.mp3", SearchOption.TopDirectoryOnly);
            return getSongsFromMusicFolder;
        }
    }
}