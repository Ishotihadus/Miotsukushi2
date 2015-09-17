using System;
using System.Timers;

namespace Miotsukushi.Model
{
    internal class TimerModel
    {
        private double _timerinterval = 500.0;
        private readonly Timer _timer;

        public TimerModel()
        {
            _timer = new Timer(Timerinterval);
            _timer.Elapsed += timer_Elapsed;
            _timer.Start();
        }

        public double Timerinterval
        {
            get { return _timerinterval; }
            set
            {
                if (_timerinterval != value)
                {
                    _timerinterval = value;
                    if (_timer != null)
                    {
                        _timer.Interval = value;
                    }
                }
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnTimerElapsed(new EventArgs());
        }

        public event EventHandler TimerElapsed;

        protected virtual void OnTimerElapsed(EventArgs e)
        {
            if (TimerElapsed != null)
            {
                TimerElapsed(this, e);
            }
        }
    }
}