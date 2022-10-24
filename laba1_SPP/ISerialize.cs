using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1_SPP
{
    internal interface ISerialize
    {
        void SaveTraceResult(TextWriter textWriter, TraceResult traceResult);
    }
}
