using Hangfire;
using System;
using System.Threading;

namespace JobsExample
{

  
    public class Job
    {
        public Job()
        {
            
        }

        #region Methods

        [Queue("p1")]
        public void P1Method(string value)
        {
            Console.WriteLine($"======> P1 method {value}");
            Thread.Sleep(600);
        }

        [Queue("p2")]
        //[Recurrent(10, 10)]
        public void P2Method(string value)
        {
            Console.WriteLine($"======> P2 method {value}");
            Thread.Sleep(600);
        }

        [Queue("p3")]
        public void P3Method(string value)
        {
            Console.WriteLine($"======> P3 method {value}");
            Thread.Sleep(600);
        }

        public void JobGenerator(JobManager jm)
        {
            int qty = 1000;

            jm.P1MethodJobGenerator(qty);
            jm.P2MethodJobGenerator(qty);
            jm.P3MethodJobGenerator(qty);
        }

        #endregion

        
    }
}
