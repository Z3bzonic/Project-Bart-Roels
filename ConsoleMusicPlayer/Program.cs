using ConsoleMusicPlayer;
using ConsoleMusicPlayer.Backend;
using ConsoleMusicPlayer.Frontend;
using WMPLib;

Console.OutputEncoding = System.Text.Encoding.UTF8;
SetColorCombo colorCombo = new SetColorCombo();
AsciiArt asciiArt = new AsciiArt();
Positions position = new Positions();
WindowsMediaPlayer player = new WindowsMediaPlayer();
EasterEgg visualizer = new EasterEgg();
Messages messages = new Messages(colorCombo, asciiArt, position, visualizer);
FileFunctions fileFunctions = new FileFunctions(messages);
ConsoleMediaPlayer consoleMediaPlayer = new ConsoleMediaPlayer(player, messages, fileFunctions);

int choice = 0;
bool choiceTest=false;
messages.RenderTitle();
messages.RenderAsciiMenu();
consoleMediaPlayer.PrepareSongList(fileFunctions.GetMusicFromFolder());

while (choice != 9)
{
    messages.DisplayMenuChoice();
    choiceTest = int.TryParse(Console.ReadLine(), out choice);
    if (choiceTest)
    {
        messages.ClearNoFileLoadedMessage();
        consoleMediaPlayer.ChoicePatchTrough(choice);
    }
}