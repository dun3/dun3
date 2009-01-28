using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diag = System.Diagnostics;

namespace Com.Hertkorn.Framework.Timing
{
    public class Stopwatch
    {
        static Stopwatch()
        {
            Frequency = Diag.Stopwatch.Frequency;
            IsHighResolution = Diag.Stopwatch.IsHighResolution;
        }

        public Stopwatch()
            : this("<not set>")
        {
        }

        public Stopwatch(string name)
        {
            Name = name;
            Reset();
        }

        #region Mirror System.Diagnostics.Stopwatch functions

        public static readonly long Frequency;
        public static readonly bool IsHighResolution;

        public static long GetTimestamp() { return Diag.Stopwatch.GetTimestamp(); }

        public static Stopwatch StartNew()
        {
            Stopwatch newStopwatch = new Stopwatch();
            newStopwatch.Start();

            return newStopwatch;
        }

        public void Start()
        {
            m_stopwatchState = StopwatchState.Running;
            m_stopwatch.Start();
        }
        public void Stop()
        {
            m_stopwatchState = StopwatchState.Stopped;
            m_stopwatch.Stop();
        }
        public void Reset()
        {
            OperationCount = 0;
            m_stopwatchState = StopwatchState.New;

            m_stopwatch.Reset();
        }

        public TimeSpan Elapsed { get { return m_stopwatch.Elapsed; } }
        public long ElapsedMilliseconds { get { return m_stopwatch.ElapsedMilliseconds; } }
        public long ElapsedTicks { get { return m_stopwatch.ElapsedTicks; } }
        public bool IsRunning { get { return m_stopwatch.IsRunning; } }

        #endregion

        private Diag.Stopwatch m_stopwatch = new Diag.Stopwatch();
        private StopwatchState m_stopwatchState;

        private string m_name;
        public string Name
        {
            get { return m_name; }
            set
            {
                if (value == null) throw new ArgumentNullException("Name");

                m_name = value;
            }
        }

        private OperationCountState m_operationCountState;

        private long m_operationCount;
        public long OperationCount
        {
            get { return m_operationCount; }
            set
            {
                if (value < 0) throw new ArgumentException("OperationCount cannot be set to a negative value");

                m_operationCount = value;

                if (m_operationCount > 0)
                {
                    m_operationCountState = OperationCountState.HasOperationCount;
                }
                else
                {
                    m_operationCountState = OperationCountState.NoOperationCount;
                }
            }
        }

        public static Stopwatch Time(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            action();

            stopwatch.Stop();

            return stopwatch;
        }

        public static Stopwatch Time(string name, Action action)
        {
            Stopwatch stopwatch = Time(action);
            stopwatch.Name = name;

            return stopwatch;
        }

        public static Stopwatch Time(string name, long operationCount, Action action)
        {
            Stopwatch stopwatch = Time(action);
            stopwatch.Name = name;
            stopwatch.OperationCount = operationCount;

            return stopwatch;
        }

        //public void MarkedSection(string name, Action action)
        //{
        //    throw new NotImplementedException();
        //}

        public override string ToString()
        {
            return m_operationCountState.GetTimingResult(this)
                + Environment.NewLine
                + "Is " + m_stopwatchState;
        }
    }
}
