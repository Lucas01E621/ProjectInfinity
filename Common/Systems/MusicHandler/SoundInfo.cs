using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectInfinity.Common.Systems.MusicHandler
{
    public struct SoundInfo
    {
        public int duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = (int)(value / speed);
            }
        }
        public float speed
        {
            get
            {
                return speed;
            }
            set
            {
                if (value == 0.25f || value == 0.5f || value == 0.75f || value == 1 || value == 1.25f || value == 1.5f || value == 1.75f || value == 2)
                    speed = value;
            }
        }
        public int volume
        {
            get
            {
                return volume;
            }
            set
            {
                if(value < 100 && value > 0)
                {
                    volume = value;
                }
            }
        }
        public string path { get; set; }
    }
}
