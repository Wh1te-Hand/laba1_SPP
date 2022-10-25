//using BankAccountNS;
using TracerLibrary;
namespace testForLab1
{
    [TestClass]
    public class TracerTests
    {
        public Tracer tracer = new Tracer();

        void Method1()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        void Method2()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        void MethodWithInnerMethodCall()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            Method1();
            tracer.StopTrace();
        }

        void TwoThreadsCallOneMethod()
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 2; i++)
            {
                Thread thread = new Thread(Method1);
                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        [TestMethod]
        public void ShouldTrace_SingleThread_SingleMethodOnTheFirstLvl()
        {
            Method1();

            TraceResult tracerResult = tracer.GetTraceResult();

            ThreadTraceResult[] threadTracersResults = new ThreadTraceResult[tracer.GetTraceResult().dThreadTracerResults.Count];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual(1, threadTracersResults.Length); 
            Assert.AreEqual(1, threadTracersResults[0].lFirstLvlMethodTracersResult.Count); 

            string methodNameFromTraceResult = threadTracersResults[0].lFirstLvlMethodTracersResult[0].MethodName;
            string classNameFromTraceResult = threadTracersResults[0].lFirstLvlMethodTracersResult[0].ClassName;
            Assert.AreEqual("Method1", methodNameFromTraceResult);
            Assert.AreEqual("TracerTests", classNameFromTraceResult);
        }


        [TestMethod]
        public void ShouldTrace_SingleThread_TwoMethodsOnTheFirstLvl()
        {
            Method1();
            Method2();

            TraceResult tracerResult = tracer.GetTraceResult();

            ThreadTraceResult[] threadTracersResults = new ThreadTraceResult[tracer.GetTraceResult().dThreadTracerResults.Count];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual(1, threadTracersResults.Length); 
            Assert.AreEqual(2, threadTracersResults[0].lFirstLvlMethodTracersResult.Count); 

        
            string methodNameFromTraceResult1 = threadTracersResults[0].lFirstLvlMethodTracersResult[0].MethodName;
            string classNameFromTraceResult1 = threadTracersResults[0].lFirstLvlMethodTracersResult[0].ClassName;
            Assert.AreEqual("Method1", methodNameFromTraceResult1);
            Assert.AreEqual("TracerTests", classNameFromTraceResult1);

            string methodNameFromTraceResult2 = threadTracersResults[0].lFirstLvlMethodTracersResult[1].MethodName;
            string classNameFromTraceResult2 = threadTracersResults[0].lFirstLvlMethodTracersResult[1].ClassName;
            Assert.AreEqual("Method2", methodNameFromTraceResult2);
            Assert.AreEqual("TracerTests", classNameFromTraceResult2);
        }

        [TestMethod]
        public void ShouldTrace_SingleThread_SingleMethodOnTheFirstLvl_with_InnerMethodCall()
        {
            MethodWithInnerMethodCall();

            TraceResult tracerResult = tracer.GetTraceResult();

            ThreadTraceResult[] threadTracersResults = new ThreadTraceResult[tracer.GetTraceResult().dThreadTracerResults.Count];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual(1, threadTracersResults.Length); 
            Assert.AreEqual(1, threadTracersResults[0].lFirstLvlMethodTracersResult.Count); 


            string methodNameFromTraceResult1 = threadTracersResults[0].lFirstLvlMethodTracersResult[0].MethodName;
            string classNameFromTraceResult1 = threadTracersResults[0].lFirstLvlMethodTracersResult[0].ClassName;
            Assert.AreEqual("MethodWithInnerMethodCall", methodNameFromTraceResult1);
            Assert.AreEqual("TracerTests", classNameFromTraceResult1);
          
            Assert.AreEqual(1, threadTracersResults[0].lFirstLvlMethodTracersResult[0].lInnerMethodTracerResults.Count);

            Assert.AreEqual("Method1", threadTracersResults[0].lFirstLvlMethodTracersResult[0].lInnerMethodTracerResults[0].MethodName);
            Assert.AreEqual("TracerTests", threadTracersResults[0].lFirstLvlMethodTracersResult[0].lInnerMethodTracerResults[0].ClassName);
        }

        [TestMethod]
        public void ShouldTrace_TwoThreads_each_with_SingleMethod()
        {
            TwoThreadsCallOneMethod();

            TraceResult tracerResult = tracer.GetTraceResult();

            ThreadTraceResult[] threadTracersResults = new ThreadTraceResult[tracer.GetTraceResult().dThreadTracerResults.Count];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual(2, threadTracersResults.Length); 
            Assert.AreEqual(1, threadTracersResults[0].lFirstLvlMethodTracersResult.Count); 
            Assert.AreEqual(1, threadTracersResults[1].lFirstLvlMethodTracersResult.Count); 

            string methodNameFromTraceResult1 = threadTracersResults[0].lFirstLvlMethodTracersResult[0].MethodName;
            string classNameFromTraceResult1 = threadTracersResults[0].lFirstLvlMethodTracersResult[0].ClassName;
            Assert.AreEqual("Method1", methodNameFromTraceResult1);
            Assert.AreEqual("TracerTests", classNameFromTraceResult1);

            string methodNameFromTraceResult2 = threadTracersResults[1].lFirstLvlMethodTracersResult[0].MethodName;
            string classNameFromTraceResult2 = threadTracersResults[1].lFirstLvlMethodTracersResult[0].ClassName;
            Assert.AreEqual("Method1", methodNameFromTraceResult1);
            Assert.AreEqual("TracerTests", classNameFromTraceResult1);
        }
    }
    /*  [TestMethod]
      public void Debit_WithValidAmount_UpdatesBalance()
      {
          // Arrange
          double beginningBalance = 11.99;
          double debitAmount = 4.55;
          double expected = 7.44;
          BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

          // Act
          account.Debit(debitAmount);

          // Assert
          double actual = account.Balance;
          Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
      }

      [TestMethod]
      public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
      {
          // Arrange
          double beginningBalance = 11.99;
          double debitAmount = 20.0;
          BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

          // Act
          try
          {
              account.Debit(debitAmount);
          }
          catch (System.ArgumentOutOfRangeException e)
          {
              // Assert
              StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
              return;
          }

          Assert.Fail("The expected exception was not thrown.");
      }*/
}
