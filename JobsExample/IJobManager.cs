namespace JobsExample
{
    /// <summary>
    /// Interface to expose a commom method to configure the jobs.
    /// </summary>
    public interface IJobManager
    {
        /// <summary>
        /// This method is the entry point of the interface.
        /// When called, this method should be used to queue the jobs on hangfire. 
        /// </summary>
        void ConfigureJobs();
    }
}