using Hangfire.Queues.OTF;
using System;
using System.Collections.Generic;
using JobsExample;
using Hangfire;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //sets the hangfire configuration
            GlobalConfiguration.Configuration
                .UseColouredConsoleLogProvider()
                .UseSqlServerStorage(@"Server=.\sqlexpress;Database=HangfireTest;User Id=sa;Password=senhadosa");

            var server = new HangfireProcessingServer();
            var jm = new JobManager();
            jm.ConfigureJobs();

            int i = 0;

            var cmd = "";
            while (cmd.ToLower() != "stop")
            {
                if(i++ % 2 == 0)
                    server.WorkerList = GetWorkerList();
                else
                    server.WorkerList = GetWorkerList2();

                server.Reload();
                cmd = Console.ReadLine();
            }
        }

        private static Dictionary<string, int> GetWorkerList()
        {
            Console.WriteLine("============================> Conf 1");
            return new Dictionary<string, int>
            {
                {"p1",1},
                {"p2",0},
                {"p3",1}
            };
        }

        private static Dictionary<string, int> GetWorkerList2()
        {
            Console.WriteLine("============================> Conf 2");
            return new Dictionary<string, int>
            {
                {"p1",0},
                {"p2",1},
                {"p3",0}
            };
        }

        
    }
}
