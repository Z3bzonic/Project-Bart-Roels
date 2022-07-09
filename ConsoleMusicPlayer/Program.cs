using ConsoleMusicPlayer;
using WMPLib;

Console.OutputEncoding = System.Text.Encoding.UTF8;
SetColorCombo colorCombo = new SetColorCombo();
AsciiArt asciiArt = new AsciiArt();
Positions position = new Positions();
WindowsMediaPlayer player = new WindowsMediaPlayer();
Messages messages = new Messages(colorCombo, asciiArt, position);
FileFunctions fileFunctions = new FileFunctions(messages);
ConsoleMediaPlayer consoleMediaPlayer = new ConsoleMediaPlayer(player, messages, fileFunctions);

char choice = ' ';

messages.RenderTitle();
messages.RenderAsciiMenu();
messages.RenderSongList(fileFunctions.GetMusicFromFolder());

while (choice != 'x')
{
    messages.DisplayMenuChoice();
    choice = (char)Console.Read();
    messages.ClearNoFileLoadedMessage();
    consoleMediaPlayer.ChoicePatchTrough(choice);
}