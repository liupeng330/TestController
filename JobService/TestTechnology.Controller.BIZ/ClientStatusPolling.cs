using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TestTechnology.DAL;
using Timer = System.Timers.Timer;

namespace TestTechnology.Controller.BIZ
{
    public static class ClientStatusPolling
    {
        private readonly static Timer _timer = new Timer(1000);
        private readonly static IJobDataRepository _JobDataRepository = new JobDataRepository();

        static ClientStatusPolling()
        {
            _timer.Elapsed += _timer_Elapsed;
        }

        static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO: Check client status table's update time, to make a client to be 'Down' status
            Console.WriteLine("Timer:" + Thread.CurrentThread.ManagedThreadId + "\n" +_JobDataRepository.GetJobGroup(1).StartTime );
        }

        public static void StartTimer()
        {
            _timer.Start();
        }

        public static void StopTimer()
        {
            _timer.Stop();
        }
    }
}
