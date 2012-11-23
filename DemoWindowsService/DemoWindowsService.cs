using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace DemoWindowsService
{
    public partial class DemoWindowsService : ServiceBase
    {
        public DemoWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //DebugLaunch();
            MainTimer.Enabled = true;
        }

        protected override void OnStop()
        {
            MainTimer.Enabled = false;
        }

        public void InteractiveStart(string [] args)
        {
            OnStart(args);
        }

        public void InteractiveStop()
        {
            OnStop();
        }

        private void MainTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //DebugBreak();
            var path = Assembly.GetExecutingAssembly().Location;
            var dir = Path.GetDirectoryName(path);
            var text = string.Format("Tick: {0:yyyy-MM-dd HH:mm:ss.fff}{1}", DateTime.Now, Environment.NewLine);
            File.AppendAllText(dir + "\\output.log", text);
            Console.Write(text);
        }

        [Conditional("DEBUG")]
        public void DebugBreak()
        {
            Debugger.Break();
        }

        [Conditional("DEBUG")]
        public void DebugLaunch()
        {
            Debugger.Launch();
        }   
    }
}
