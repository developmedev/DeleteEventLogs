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

namespace DeleteEventLogs
{
    public partial class DeleteEventLogService : ServiceBase
    {
        Timer timer = new Timer();
        public DeleteEventLogService()
        {
            InitializeComponent();
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 3600000; //number in milisecinds  
            timer.Enabled = true;
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Windows\Temp\");
            FileInfo[] files = di.GetFiles("*.evtx")
                                 .Where(p => p.Extension == ".evtx").ToArray();
            foreach (FileInfo file in files)
            {
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch
                {

                }
            }
        }
        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
