
using laba1_SPP;
using System;
using System.Diagnostics;


namespace MainProgramTests
{
    
    public class MainProgram
    {
        const int size = 2000;

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
            WriteTimeJson();

            Console.ReadLine();
        }
        public void TestMethod_1()
        {
            this.Tracer.StartTrace();
   //         Thread.Sleep(250);
            int[,] mass = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Random random = new Random();
                    mass[i, j] = random.Next(0, 2);
                }
            }
            TestMethod_1_1(mass);
            this.Tracer.StopTrace();
        }
        public void TestMethod_1_1(int[,] mass)
        {
            this.Tracer.StartTrace();
            int sum = 0;
            for (int i = 0; i < mass.GetLength(0); i++)
            {
                for (int j=0;j<mass.GetLength(1);j++)
                {
                    sum+=mass[i,j];
                }
            }
            Console.WriteLine(sum);
            this.Tracer.StopTrace();
        }

        public void TestMethod_2()
        {
            this.Tracer.StartTrace();
            int[,] mass = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Random random = new Random();
                    mass[i, j] = random.Next(0, 2);
                }
            }
            TestMethod_2_1(mass);
            this.Tracer.StopTrace();
        }
        public void TestMethod_2_1(int[,] mass)
        {
            this.Tracer.StartTrace();
            int sum = 0;
            for (int i = 0; i < mass.GetLength(0); i++)
            {
                for (int j = 0; j < mass.GetLength(1); j++)
                {
                    sum += mass[j, i];
                }
            }
            Console.WriteLine(sum);
            this.Tracer.StopTrace();

        }

        public void WriteTimeXml()
        {
            TraceResult tracerResult = this.Tracer.GetTraceResult();
            XmlSerialize xmlSerializer = new XmlSerialize();

            WriteTimeXmlFile(tracerResult, xmlSerializer);
            WriteTimeXmlConsole(tracerResult, xmlSerializer);

        }

        public void WriteTimeXmlConsole(TraceResult tracerResult, XmlSerialize xmlSerializer)
        {
            xmlSerializer.SaveTraceResult(Console.Out, tracerResult);
            Console.WriteLine();
            Console.WriteLine();
        }

        public void WriteTimeXmlFile(TraceResult tracerResult, XmlSerialize xmlSerializer)
        {
            string pathToSave = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\TraceResults\\XmlTraseResult.xml");
            FileStream fileStream = new FileStream(pathToSave, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            xmlSerializer.SaveTraceResult(streamWriter, tracerResult);
        }

        public void WriteTimeJson()
        {
            TraceResult tracerResult = this.Tracer.GetTraceResult();
            JsonSerialize jsonSerializer = new JsonSerialize();

            WriteTimeJsonFile(tracerResult, jsonSerializer);
            WriteTimeJsonConsole(tracerResult, jsonSerializer);

        }

        public void WriteTimeJsonConsole(TraceResult tracerResult, JsonSerialize jsonSerializer)
        {
            jsonSerializer.SaveTraceResult(Console.Out, tracerResult);
            Console.WriteLine();
            Console.WriteLine();
        }

        public void WriteTimeJsonFile(TraceResult tracerResult, JsonSerialize jsonSerializer)
        {
            string pathToSave = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\TraceResults\\XmlTraseResult.xml");
            FileStream fileStream = new FileStream(pathToSave, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            jsonSerializer.SaveTraceResult(streamWriter, tracerResult);
        }



    }
}