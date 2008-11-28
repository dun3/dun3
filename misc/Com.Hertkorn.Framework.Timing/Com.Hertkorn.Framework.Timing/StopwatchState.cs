using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.Timing
{
    internal abstract class StopwatchState
    {
        public override abstract string ToString();

        public static readonly StopwatchState New = new NewState();
        public static readonly StopwatchState Running = new RunningState();
        public static readonly StopwatchState Stopped = new StoppedState();

        private class NewState : StopwatchState
        {
            public override string ToString()
            {
                return "new";
            }
        }

        private class RunningState : StopwatchState
        {
            public override string ToString()
            {
                return "running";
            }
        }

        private class StoppedState : StopwatchState
        {
            public override string ToString()
            {
                return "stopped";
            }
        }
    }
}
