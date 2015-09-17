using System;
using System.Timers;

namespace Miotsukushi.Model
{
    internal class TimerModel
    {
        private double _timerinterval = 500.0;
        private readonly Timer timer;

        public TimerModel()
        {
            timer = new Timer(timerinterval);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        public double timerinterval
        {
            get { return _timerinterval; }
            set
            {
                if (_timerinterval != value)
                {
                    _timerinterval = value;
                    if (timer != null)
                    {
                        timer.Interval = value;
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