
using laba1_SPP;
using System;
using System.Diagnostics;


namespace MainProgramTests
{
    
    public class MainProgram
    {

        public Tracer Tracer { get; set; }

        static void Main(string[] args)
        {
            MainProgram program = new MainProgram();
            program.Perform();
        }
        public void Perform()
        {
            this.Tracer = new Tracer();

            TestMethod_1();
            TestMethod_2();

            WriteTimeXml();

            Console.ReadLine();
        }
        public void TestMethod_1()
        {
            this.Tracer.StartTrace();
            Thread.Sleep(100);
            this.Tracer.StopTrace();
        }
        public void TestMethod_2()
        {
            this.Tracer.StartTrace();
            Thread.Sleep(200);
            this.Tracer.StopTrace();
        }

        public void WriteTimeXml()
        {
            TraceResult tracerResult = this.Tracer.GetTraceResult();
            XmlOutput xmlSerializer = new XmlOutput();

            WriteTimeXmlFile(tracerResult, xmlSerializer);
            WriteTimeXmlConsole(tracerResult, xmlSerializer);

        }

        public void WriteTimeXmlConsole(TraceResult tracerResult, XmlOutput xmlSerializer)
        {
            xmlSerializer.SaveTraceResult(Console.Out, tracerResult);
            Console.WriteLine();
            Console.WriteLine();
        }

        public void WriteTimeXmlFile(TraceResult tracerResult, XmlOutput xmlSerializer)
        {
            string pathToSave = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\TraceResults\\XmlTraseResult.xml");
            FileStream fileStream = new FileStream(pathToSave, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            xmlSerializer.SaveTraceResult(streamWriter, tracerResult);
        }

    }
}