using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace laba1_SPP
{
    public class XmlSerialize:ISerialize
    {
        public void SaveTraceResult(TextWriter textWriter, TraceResult traceResult)
        {
            XDocument doc = new XDocument(
                new XElement("root", from threadTracerResult in traceResult.dThreadTracerResults.Values
                                     select SaveThread(threadTracerResult)
                ));

            using (XmlTextWriter xmlWriter = new XmlTextWriter(textWriter))
            {
                xmlWriter.Formatting = Formatting.Indented;
                doc.WriteTo(xmlWriter);
            }
        }
        private XElement SaveThread(ThreadTraceResult threadTracer)
        {
            return new XElement("thread",
                new XAttribute("id", threadTracer.Id),
                new XAttribute("time", threadTracer.Time.Seconds+ "." + threadTracer.Time.Milliseconds+ "ms"),
                from methodTracerResult in threadTracer.lFirstLvlMethodTracersResult
                select SaveMethod(methodTracerResult)
                );
        }

        private XElement SaveMethod(MethodTraceResult methodTracer)
        {
            XElement savedTracedMetod = new XElement("method",
                new XAttribute("name", methodTracer.MethodName),
                new XAttribute("time", methodTracer.Time.Milliseconds + "ms"),
                new XAttribute("class", methodTracer.ClassName));

            if (methodTracer.lInnerMethodTracerResults.Any())
                savedTracedMetod.Add(from innerMethod in methodTracer.lInnerMethodTracerResults
                                     select SaveMethod(innerMethod));
            return savedTracedMetod;
        }
    }
}
