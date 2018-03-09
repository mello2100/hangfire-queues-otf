using Hangfire;
using System;
using System.Threading;

namespace JobsExample
{
    /// <summary>
    /// Sample class job.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Job()
        {
            
        }

        #region Methods

        /// <summary>
        /// Sample job to be enqued on 'p1' queue.
        /// </summary>
        /// <param name="value"></param>
        [Queue("p1")]
        public void P1Method(string value)
        {
            Console.WriteLine($"======> P1 method {value}");
            Thread.Sleep(600);
        }

        /// <summary>
        /// Sample job to be enqued on 'p2' queue.
        /// </summary>
        /// <param name="value"></param>
        [Queue("p2")]
        public void P2Method(string value)
        {
            Console.WriteLine($"======> P2 method {value}");
            Thread.Sleep(600);
        }

        /// <summary>
        /// Sample job to be enqued on 'p3' queue.
        /// </summary>
        /// <param name="value"></param>
        [Queue("p3")]
        public void P3Method(string value)
        {
            Console.WriteLine($"======> P3 method {value}");
            Thread.Sleep(600);
        }

        /// <summary>
        /// Sample job that calls the methods responsible to enqueue our jobs.
        /// </summary>
        /// <param name="jm"></param>
        public void JobGenerator(JobManager jm)
        {
            //in this sample we are enqueueing 'qty'jobs of each type.
            int qty = 1000;

            jm.P1MethodJobGenerator(qty);
            jm.P2MethodJobGenerator(qty);
            jm.P3MethodJobGenerator(qty);
        }

        #endregion

        
    }
}
