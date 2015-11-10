using System;
using CoreAudio;

namespace Miotsukushi.Model.Audio
{
    class VolumeModel
    {
        readonly AudioSessionManager2 _sessions;
        AudioSessionControl2 _session;

        readonly int _processid;

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
                    _session.SimpleAudioVolume.MasterVolume = (float)value;
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
                    _session.SimpleAudioVolume.Mute = value;
                }
            }
        }

        public VolumeModel()
        {
            _processid = System.Diagnostics.Process.GetCurrentProcess().Id;
            HasAudioSession = false;

            var deviceenum = new MMDeviceEnumerator();
            var activedevice = deviceenum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);

            _sessions = activedevice.AudioSessionManager2;

            if(!StartupManager())
                _sessions.OnSessionCreated += sessions_OnSessionCreated;
        }

        void sessions_OnSessionCreated(object sender, CoreAudio.Interfaces.IAudioSessionControl2 newSession)
        {
            uint newpid;
            newSession.GetProcessId(out newpid);
            if(newpid == _processid)
            {
                // 自分のプロセスができた
                _sessions.RefreshSessions();
                StartupManager();
            }
        }

        bool StartupManager()
        {
            if (HasAudioSession)
                return false;

            for (var i = 0; i < _sessions.Sessions.Count; i++)
            {
                if (_sessions.Sessions[i].GetProcessID == _processid)
                {
                    HasAudioSession = true;
                    _session = _sessions.Sessions[i];
                    break;
                }
            }
            if (HasAudioSession)
            {
                _session.OnSimpleVolumeChanged += session_OnSimpleVolumeChanged;
                _volume = _session.SimpleAudioVolume.MasterVolume;
                _mute = _session.SimpleAudioVolume.Mute;
                OnGetAudioSession(new EventArgs());
            }

            return HasAudioSession;
        }

        void session_OnSimpleVolumeChanged(object sender, float newVolume, bool newMute)
        {
            if (newVolume == _volume && newMute == _mute)
                return; // 値が変わらなかった際はイベントを発行しない
            OnVolumeChanged(new VolumeChangedEventArgs() { NewVolume = newVolume, NewMute = newMute, OriginVolume = _volume, OriginMute = _mute });
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
