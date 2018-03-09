using Hangfire;
using System;

namespace JobsExample
{
    /// <summary>
    /// This sample class implements the IJobManager interface.
    /// </summary>
    public class JobManager : IJobManager
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public JobManager()
        {

        }

        #region Jobs

        /// <summary>
        /// The entry point to configure the jobs.
        /// </summary>
        public void ConfigureJobs()
        {
            // in this sample, we are configuring a recurring job (located on jobs class) that is scheduled to enqueue our jobs.
            RecurringJob.AddOrUpdate<Job>(x => x.JobGenerator(this), Cron.Minutely);
        }

        /// <summary>
        /// This sample method is called from JobGenerator method (a Recurring job) to enqueue 'n' P1Method jobs.
        /// </summary>
        /// <param name="n"></param>
        public void P1MethodJobGenerator(int n)
        {
            for (int i = 0; i < n; i++)
            {
                BackgroundJob.Enqueue<Job>(x => x.P1Method(Guid.NewGuid().ToString()));
            }
        }

        /// <summary>
        /// This sample method is called from JobGenerator method (a Recurring job) to enqueue 'n' P2Method jobs.
        /// </summary>
        /// <param name="n"></param>
        public void P2MethodJobGenerator(int n)
        {
            for (int i = 0; i < n; i++)
            {
                BackgroundJob.Enqueue<Job>(x => x.P2Method(Guid.NewGuid().ToString()));
            }
        }

        /// <summary>
        /// This sample method is called from JobGenerator method (a Recurring job) to enqueue 'n' P3Method jobs.
        /// </summary>
        /// <param name="n"></param>
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
