using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreAudio;

namespace Miotsukushi.Model.Audio
{
    class VolumeModel
    {
        MMDeviceEnumerator deviceenum;
        MMDevice activedevice;
        AudioSessionManager2 sessions;
        AudioSessionControl2 session;

        int processid;

        public bool HasAudioSession { get; private set; }

        private double _volume;
        public double Volume
        {
            get { return _volume; }
            set
            {
                if(_volume != value && HasAudioSession)
                {
                    _volume = value;
                    session.SimpleAudioVolume.MasterVolume = (float)value;
                }
            }
        }

        private bool _mute;
        public bool Mute
        {
            get { return _mute; }
            set
            {
                if (_mute != value && HasAudioSession)
                {
                    _mute = value;
                    session.SimpleAudioVolume.Mute = value;
                }
            }
        }

        public VolumeModel()
        {
            processid = System.Diagnostics.Process.GetCurrentProcess().Id;
            HasAudioSession = false;

            deviceenum = new MMDeviceEnumerator();
            activedevice = deviceenum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);

            sessions = activedevice.AudioSessionManager2;

            if(!StartupManager())
                sessions.OnSessionCreated += sessions_OnSessionCreated;
        }

        void sessions_OnSessionCreated(object sender, CoreAudio.Interfaces.IAudioSessionControl2 newSession)
        {
            uint newpid;
            newSession.GetProcessId(out newpid);
            if(newpid == processid)
            {
                // 自分のプロセスができた
                sessions.RefreshSessions();
                StartupManager();
            }
        }

        bool StartupManager()
        {
            if (HasAudioSession)
                return false;

            for (int i = 0; i < sessions.Sessions.Count; i++)
            {
                if (sessions.Sessions[i].GetProcessID == processid)
                {
                    HasAudioSession = true;
                    session = sessions.Sessions[i];
                    break;
                }
            }
            if (HasAudioSession)
            {
                session.OnSimpleVolumeChanged += session_OnSimpleVolumeChanged;
                _volume = session.SimpleAudioVolume.MasterVolume;
                _mute = session.SimpleAudioVolume.Mute;
                OnGetAudioSession(new EventArgs());
            }

            return HasAudioSession;
        }

        void session_OnSimpleVolumeChanged(object sender, float newVolume, bool newMute)
        {
            if (newVolume == _volume && newMute == _mute)
                return; // 値が変わらなかった際はイベントを発行しない
            OnVolumeChanged(new VolumeChangedEventArgs() { newvolume = newVolume, newmute = newMute, originvolume = _volume, originmute = _mute });
            _volume = newVolume;
            _mute = newMute;
        }

        /// <summary>
        /// オーディオのセッションが得られた際に呼び出されます
        /// </summary>
        public event EventHandler GetAudioSession;
        protected virtual void OnGetAudioSession(EventArgs e) { if (GetAudioSession != null) { GetAudioSession(this, e); } }

        /// <summary>
        /// ボリュームが変更された際に呼び出されます
        /// </summary>
        public event VolumeChangedEventHandler VolumeChanged;
        public delegate void VolumeChangedEventHandler(object sender, VolumeChangedEventArgs e);
        protected virtual void OnVolumeChanged(VolumeChangedEventArgs e) { if (VolumeChanged != null) { VolumeChanged(this, e); } }
    }
}
