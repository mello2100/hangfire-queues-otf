using Hangfire.Server;
using System.Collections.Generic;

namespace Hangfire.Queues.OTF
{
    public static class WorkersFactory
    {
        public static List<IBackgroundProcess> Create(string name, int qty)
        {
            var processes = new List<IBackgroundProcess>();

            for (int i = 0; i < qty; i++)
            {
                processes.Add(new Worker(name));
            }

            return processes;
        }
    }
}
