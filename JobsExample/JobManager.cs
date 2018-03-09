using Hangfire;
using System;

namespace JobsExample
{
    public class JobManager
    {
        public JobManager()
        {

        }

        #region Jobs

        public void ConfigureJobs()
        {
            RecurringJob.AddOrUpdate<Job>(x => x.JobGenerator(this), Cron.Minutely);
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
