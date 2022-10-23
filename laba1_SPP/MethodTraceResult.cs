using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1_SPP
{
    public class MethodTraceResult
    {
        public string ClassName { get; private set; }
        public string MethodName { get; private set; }
        public TimeSpan Time { get; private set; }
        public List<MethodTraceResult> lInnerMethodTracerResults;

        public static MethodTraceResult GetTraceResult(MethodTracer methodTracer)
        {
            MethodTraceResult methodTracerResult = new MethodTraceResult();
            methodTracerResult.ClassName = methodTracer.ClassName;
            methodTracerResult.MethodName = methodTracer.MethodName;
            methodTracerResult.Time = methodTracer.Time;
            methodTracerResult.lInnerMethodTracerResults = new List<MethodTraceResult>();

            foreach (var innerMethodTracer in methodTracer.lInnerMethodTracers)
            {
                methodTracerResult.lInnerMethodTracerResults.Add(MethodTraceResult.GetTraceResult(innerMethodTracer));
            }

            return methodTracerResult;
        }
    }
}
