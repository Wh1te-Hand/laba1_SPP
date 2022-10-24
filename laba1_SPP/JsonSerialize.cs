using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace laba1_SPP
{
    public class JsonSerialize:ISerialize
    {
        public void SaveTraceResult(TextWriter textWriter, TraceResult traceResult)
        {
            var jtokens = from threadTracerResult in traceResult.dThreadTracerResults.Values
            select SaveThreads(threadTracerResult);
            JObject traceResultJSON = new JObject { { "thread", new JArray(jtokens) } };

            using (var jsonWriter = new JsonTextWriter(textWriter))
            {
                jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented;
                traceResultJSON.WriteTo(jsonWriter);
            }
        }

        private JToken SaveThreads(ThreadTraceResult threadTracerResult)
        {
            var lFirstLvlMethods = from methodTracerResult in threadTracerResult.lFirstLvlMethodTracersResult
                                   select SaveMethods(methodTracerResult);
            return new JObject
            {
                { "id", threadTracerResult.Id },
                { "time",threadTracerResult.Time.Seconds+"."+ threadTracerResult.Time.Milliseconds + "ms"},
                { "methods", new JArray(lFirstLvlMethods) }
            };
        }

        private JToken SaveMethods(MethodTraceResult methodTracerResult)
        {
            var savedTracedMetod = new JObject
            {
                { "name", methodTracerResult.MethodName },
                { "class", methodTracerResult.ClassName },
                { "time", methodTracerResult.Time.Milliseconds + "ms" }
            };

            if (methodTracerResult.lInnerMethodTracerResults.Any())
                savedTracedMetod.Add("methods", new JArray(from mt in methodTracerResult.lInnerMethodTracerResults
                                                           select SaveMethods(mt)));
            return savedTracedMetod;
        }

    }
}
