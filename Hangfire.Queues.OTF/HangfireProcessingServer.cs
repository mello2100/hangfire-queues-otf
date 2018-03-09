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

        public HangfireProcessingServer()
        {
            _options.ShutdownTimeout = new TimeSpan(0, 1, 0);   //1 minute

            WorkerList = new Dictionary<string, int>();

            GlobalConfiguration.Configuration
                .UseColouredConsoleLogProvider()
                .UseSqlServerStorage(@"Server=.\sqlexpress;Database=HangfireTest;User Id=sa;Password=senhadosa");
        }

        public void Reload()
        {
            Console.WriteLine("Reloading...");

            //TODO: Will retrieve from another way
            List<IBackgroundProcess> processes = GetWorkers();
            
            _ct.Cancel();
            Task t = new Task(() => { HangfireServer(processes, _ct.Token); });
            _ct = new CancellationTokenSource();
            t.Start();
        }

        private void HangfireServer(List<IBackgroundProcess> processes, CancellationToken c)
        {
            using (var server = new BackgroundProcessingServer(JobStorage.Current, processes, new Dictionary<string, object>(), _options))
            {
                while (c.IsCancellationRequested == false)
                {
                };
            }
        }

        private List<IBackgroundProcess> GetWorkers()
        {
            var processes = new List<IBackgroundProcess>
            {
                new Worker("default"),
                new RecurringJobScheduler()
            };

            foreach (var item in WorkerList)
            {
                processes.AddRange(WorkersFactory.Create(item.Key, item.Value)); 
            }            

            return processes;
        }
    }
}
