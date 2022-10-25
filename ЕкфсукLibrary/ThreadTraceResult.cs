using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLibrary
{
    public class ThreadTraceResult
    {
        public IList<MethodTraceResult> lFirstLvlMethodTracersResult { get; private set; }
        public int Id { get; private set; }
        public TimeSpan Time { get; private set; }

        public static ThreadTraceResult GetTraceResult(ThreadTracer threadTracer)
        {
            ThreadTraceResult threadTracerResult = new ThreadTraceResult();
            threadTracerResult.lFirstLvlMethodTracersResult = new List<MethodTraceResult>();
            threadTracerResult.Id = threadTracer.Id;
            threadTracerResult.Time = threadTracer.Time;

            foreach (var firstLvlMethodTracer in threadTracer.lFirstLvlMethodTracers)
            {
                threadTracerResult.lFirstLvlMethodTracersResult.Add(MethodTraceResult.GetTraceResult(firstLvlMethodTracer));
            }

            return threadTracerResult;
        }
    }
}
