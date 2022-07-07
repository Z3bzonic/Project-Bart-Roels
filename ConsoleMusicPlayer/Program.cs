// See https://aka.ms/new-console-template for more information
using ConsoleMusicPlayer;
using WMPLib;

Console.OutputEncoding = System.Text.Encoding.UTF8;
SetColorCombo colorCombo = new SetColorCombo();
AsciiArt asciiArt = new AsciiArt();
Handler handler = new Handler();
WindowsMediaPlayer player = new WindowsMediaPlayer();
Messages messages = new Messages(colorCombo,asciiArt,handler) ;
ConsoleMediaPlayer consoleMediaPlayer = new ConsoleMediaPlayer(player,messages);

messages.PrintTitle();
Console.WriteLine();
string choice = "";
while (choice != "x")
{
    messages.VolumePlaceHolder();
    choice = messages.MenuChoice();
    messages.ResetNoFileLoadedMessage();
    consoleMediaPlayer.ChoicePatchTrough(choice);
}

// original code fase 1
//Messages messages = new Messages();
//SetColorCombo allColor = new SetColorCombo();
//ConsoleMediaPlayer consoleMediaPlayer = new ConsoleMediaPlayer();
//Handler handler = new Handler();
//string keuze = "";
//allColor.Fullconsole();
//int cursorTop = messages.PrintTitle();
//Console.WriteLine();
//while (keuze != "-")
//{
//    int cursorLeft = messages.PrintAskForPath();
//    consoleMediaPlayer.LoadSong(cursorLeft, cursorTop);
//    consoleMediaPlayer.StartLoadedSong();
//    keuze = Console.ReadLine();
//    if (keuze == "-")
//    {
//        return;
//    }
//}