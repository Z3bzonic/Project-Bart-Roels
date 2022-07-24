namespace ConsoleMusicPlayer.Common.Interfaces
{
    public interface IMessages
    {
        void ClearDisplayMetaData();
        void ClearErrorMessage();
        void ClearLoadMessage();
        void ClearMuteMessage();
        void ClearNoFileLoadedMessage();
        void ClearPauseMessage();
        void ClearRetryInputMessage();
        void ClearSongSelectMenu();
        void ClearSongSelectOptions();
        void ClearVolumeMenu();
        void DisplayFileNotFoundError();
        void DisplayLoadingError();
        void DisplayLoadingMetaData();
        void DisplayLoadManually();
        void DisplayMenuChoice();
        void DisplayMetaData(string[] metadata);
        void DisplayNowMuted();
        void DisplayNowPaused(string songName);
        void DisplayNowPlaying(string songName);
        char DisplayRetrySongInput();
        void DisplaySongSelect();
        int DisplayVolumeMenu();
        void RenderAsciiMenu(int activeColor = -1);
        void RenderSongList(string[] getSongsFromMusicFolder, int itemsToRender, int presentMusicFolderLength, int currentIndex);
        void RenderTitle();
        void RenderVisualArt();
        void RenderVolumeBar(int volume);
        void ResetRenderSongList();
    }
}