using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.Timing
{
    internal abstract class OperationCountState
    {
        public abstract string GetTimingResult(Stopwatch stopwatch);

        protected StringBuilder GetHeader(Stopwatch stopwatch)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(stopwatch.Name);
            sb.Append(": ");
            sb.Append(stopwatch.Elapsed);
            sb.Append(" (");
            sb.Append(stopwatch.ElapsedMilliseconds);
            sb.AppendLine(")");

            return sb;
        }

        public static OperationCountState HasOperationCount = new HasOperationCountState();
        public static OperationCountState NoOperationCount = new NoOperationCountState();

        private class HasOperationCountState : OperationCountState
        {
            public override string GetTimingResult(Stopwatch stopwatch)
            {
                StringBuilder sb = GetHeader(stopwatch);

                if (stopwatch.ElapsedMilliseconds > 0)
                {
                    sb.Append("  ");
                    sb.Append(stopwatch.OperationCount);
                    sb.AppendLine(" Ops");
                    sb.Append("  ");
                    sb.Append(stopwatch.ElapsedMilliseconds / stopwatch.OperationCount);
                    sb.AppendLine(" ms/Op");
                    sb.Append("  ");
                    sb.Append(stopwatch.OperationCount / stopwatch.ElapsedMilliseconds);
                    sb.AppendLine(" Ops/ms");
                }

                return sb.ToString();
            }
        }

        private class NoOperationCountState : OperationCountState
        {
            public override string GetTimingResult(Stopwatch stopwatch)
            {
                return GetHeader(stopwatch).ToString();
            }
        }
    }
}
