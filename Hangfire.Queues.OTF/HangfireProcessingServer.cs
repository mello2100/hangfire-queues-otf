using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

//OTF means on the fly
//https://idioms.thefreedictionary.com/on+the+fly
//Definition: while something or someone is operating or moving.
//Example: I'll try to capture the data on the fly. Please try to buy some aspirin somewhere on the fly today.
namespace Hangfire.Queues.OTF
{
    /// <summary>
    /// The main class of our project.
    /// </summary>
    public class HangfireProcessingServer
    {
        #region Properties

        public TimeSpan ShutdownTimeout
        {
            get
            {
                return _options.ShutdownTimeout;
            }
            set
            {
                _options.ShutdownTimeout = value;
            }
        }

        public Dictionary<string, int> WorkerList { get; set; }

        #endregion

        #region Fields

        private BackgroundProcessingServerOptions _options = new BackgroundProcessingServerOptions();

        private CancellationTokenSource _ct = new CancellationTokenSource();

        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public HangfireProcessingServer()
        {
            //default shutdown timeout
            _options.ShutdownTimeout = new TimeSpan(0, 1, 0);   //1 minute

            //initializes an empty worker list
            WorkerList = new Dictionary<string, int>();            
        }

        /// <summary>
        /// When called, this method loads the new worker list configuration and reloads the hangfire server.
        /// </summary>
        public void Reload()
        {
            var processes = GetWorkers();
            
            _ct.Cancel();
            Task t = new Task(() => { HangfireServer(processes, _ct.Token); });
            _ct = new CancellationTokenSource();
            t.Start();
        }

        /// <summary>
        /// This method is actually a hangfire server.
        /// </summary>
        /// <param name="processes"></param>
        /// <param name="c"></param>
        private void HangfireServer(List<IBackgroundProcess> processes, CancellationToken c)
        {
            using (var server = new BackgroundProcessingServer(JobStorage.Current, processes, new Dictionary<string, object>(), _options))
            {
                while (c.IsCancellationRequested == false)
                {
                    //TODO: should we put a thread sleep here?
                };
            }
        }

        /// <summary>
        /// Method that converts the worker list into an IBackgroundProcess list
        /// </summary>
        /// <returns></returns>
        private List<IBackgroundProcess> GetWorkers()
        {
            //we must creat the list with some 'dafault' workers
            var processes = new List<IBackgroundProcess>
            {
                new Worker("default"),
                new RecurringJobScheduler()
            };

            //iterates on worker list to insert on IBackgrounfProcess list
            foreach (var item in WorkerList)
            {
                processes.AddRange(WorkersFactory.Create(item.Key, item.Value)); 
            }            

            return processes;
        }
    }
}
