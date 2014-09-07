using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model
{
    class TimerModel
    {
        System.Timers.Timer timer;
        private double _timerinterval = 500.0;
        public double timerinterval
        {
            get
            {
                return _timerinterval;
            }
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

        public TimerModel()
        {
            timer = new System.Timers.Timer(timerinterval);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            OnTimerElapsed(new EventArgs());
        }

        public event EventHandler TimerElapsed;
        protected virtual void OnTimerElapsed(EventArgs e) { if (TimerElapsed != null) { TimerElapsed(this, e); } }
    }
}
