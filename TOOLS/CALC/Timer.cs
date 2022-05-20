using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Katty
{
    public class myTimer
    {

        public TimeElapsed Elapsed;

        internal Stopwatch Clock = new Stopwatch();

        public myTimer()
        {
            Elapsed = new TimeElapsed(this);
        }

        public void Start() => Clock.Start();
        public void Stop() => Clock.Stop();

    }
    public class TimeElapsed
    {

        private myTimer Timer;

        public double seconds => milliseconds / 1000;
        public long milliseconds => Timer.Clock.ElapsedMilliseconds;

        public TimeElapsed(myTimer prmCronos)
        {
            Timer = prmCronos;
        }
    }
}
