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

        #endregion

        #region Jobs

        public static void ConfigureJobs()
        {
            RecurringJob.AddOrUpdate<Job>(x => x.JobGenerator(), Cron.Minutely);
        }

        public void JobGenerator()
        {
            int qty = 1000;

            P1MethodJobGenerator(qty);
            P2MethodJobGenerator(qty);
            P3MethodJobGenerator(qty);
        }

        public void P1MethodJobGenerator(int n)
        {
            for (int i = 0; i < n; i++)
            {
                BackgroundJob.Enqueue<Job>(x => x.P1Method(Guid.NewGuid().ToString()));
            }
        }

        public void P2MethodJobGenerator(int n)
        {
            for (int i = 0; i < n; i++)
            {
                BackgroundJob.Enqueue<Job>(x => x.P2Method(Guid.NewGuid().ToString()));
            }
        }

        public void P3MethodJobGenerator(int n)
        {
            for (int i = 0; i < n; i++)
            {
                BackgroundJob.Enqueue<Job>(x => x.P3Method(Guid.NewGuid().ToString()));
            }
        }

        #endregion
    }
}
