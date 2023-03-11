using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity
{
    internal abstract class MusicHandler
    {
        public int BPM;
        public int Duration;
        public string MusicBase;

        public MusicHandler(int bpm, int duration, string musicBase)
        {
            BPM = bpm;
            Duration = duration;
            MusicBase = musicBase;
        }

    }
}

