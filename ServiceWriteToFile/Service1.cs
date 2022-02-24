using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ServiceWriteToFile
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60 * 1000;
            timer.Enabled = true;
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            WriteToFile("Service is running " + DateTime.Now);
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }

        public void WriteToFile(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\ServiceLog_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt";
            if (!File.Exists(filePath))
            {
                using (StreamWriter streamWriter = File.CreateText(filePath))
                {
                    streamWriter.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(filePath))
                {
                    streamWriter.WriteLine(message);
                }
            }
        }
    }
}
