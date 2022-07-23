using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMusicPlayer.Business
{
    public class StatesDTO
    {
        public bool toggleStatePlayer { get; set; } = false;
        public bool toggleMutePlayer { get; set; } = false;
        public bool pathPresent { get; set; } = false;
        public int volume { get; set; } = 50;
        public string getSong { get; set; } = "";
        public int songListCycle { get; set; } = 0;

    }
}
