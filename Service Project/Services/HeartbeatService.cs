using System;
using System.IO;
using System.Timers;
using Topshelf;

namespace Service_Project.Services
{
    public class HeartbeatService
    {
        private readonly Timer _timer;

        public HeartbeatService()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        public static TopshelfExitCode GetHostFactory()
        {
            return HostFactory.Run(runner => {
                runner.Service<HeartbeatService>(service =>
                {
                    service.ConstructUsing(constructor => new HeartbeatService());
                    service.WhenStarted(starting => starting.Start());
                    service.WhenStopped(stopping => stopping.Stop());
                });

                runner.RunAsLocalSystem();

                runner.SetServiceName("HeartbeatService");
                runner.SetDisplayName("Heartbeat Service");
                runner.SetDescription("This is the sample heartbeat service");
            });
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            string[] lines = new string[] { DateTime.Now.ToString() };
            Directory.CreateDirectory(@"C:\temp\Demos");
            File.AppendAllLines(@"C:\temp\Demos\Heartbeat.txt", lines);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
