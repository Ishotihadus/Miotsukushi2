using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.Audio
{
    class VolumeChangedEventArgs : System.EventArgs
    {
        public double NewVolume;
        public bool NewMute;
        public double OriginVolume;
        public bool OriginMute;
    }
}
