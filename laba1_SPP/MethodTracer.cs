﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1_SPP
{
    public class MethodTracer
    {
        public string MethodName { get; private set; }
        public string ClassName { get; private set; }
        public TimeSpan Time { get; private set; }
        private Stopwatch StopWatch;
        public List<MethodTracer> lInnerMethodTracers { get; private set; }

        public MethodTracer()
        {
            StackFrame sf = new StackFrame(3);
            MethodName = sf.GetMethod().Name;
            ClassName = sf.GetMethod().DeclaringType.Name;

            Time = new TimeSpan();
            StopWatch = new Stopwatch();
            lInnerMethodTracers = new List<MethodTracer>();
        }

        public void StartTrace()
        {
            StopWatch.Start();
        }

        public void StopTrace()
        {
            StopWatch.Stop();
            Time = StopWatch.Elapsed;
        }
    }
}
