﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model;
using Miotsukushi.Model.Audio;

namespace Miotsukushi.ViewModel
{
    class WindowCommandsViewModel : ViewModelBase
    {
        VolumeModel volumemodel;

        private bool _VolumeEnabled = false;
        public bool VolumeEnabled
        {
            get
            {
                return _VolumeEnabled;
            }

            set
            {
                if (_VolumeEnabled != value)
                {
                    _VolumeEnabled = value;
                    OnPropertyChanged(() => VolumeEnabled);
                }
            }
        }

        private double _Volume = 1;
        public double Volume
        {
            get
            {
                return _Volume;
            }

            set
            {
                if (_Volume != value)
                {
                    _Volume = value;
                    volumemodel.Volume = value;
                    OnPropertyChanged("Volume");
                }
            }
        }

        private bool _Mute = false;
        public bool Mute
        {
            get
            {
                return _Mute;
            }

            set
            {
                if (_Mute != value)
                {
                    _Mute = value;
                    volumemodel.Mute = value;
                    OnPropertyChanged("Mute");
                }
            }
        }


        public WindowCommandsViewModel()
        {
            volumemodel = MainModel.Current.volumeModel;
            volumemodel.GetAudioSession += volumemodel_GetAudioSession;
        }

        void volumemodel_GetAudioSession(object sender, EventArgs e)
        {
            VolumeEnabled = true;
            Volume = volumemodel.Volume;
            Mute = volumemodel.Mute;
            volumemodel.VolumeChanged += volumemodel_VolumeChanged;
        }

        void volumemodel_VolumeChanged(object sender, VolumeChangedEventArgs e)
        {
            Volume = e.newvolume;
            Mute = e.newmute;
        }
    }
}
