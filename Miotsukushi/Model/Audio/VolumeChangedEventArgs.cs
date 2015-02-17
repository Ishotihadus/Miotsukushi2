using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.Audio
{
    class VolumeChangedEventArgs : System.EventArgs
    {
        public double newvolume;
        public bool newmute;
        public double originvolume;
        public bool originmute;
    }
}
