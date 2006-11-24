using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            MyProjectInstaller h = new MyProjectInstaller(this.ServiceName, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName));
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            while (true)
            {
                //Thread.Sleep(200);
                MessageBox.Show(DateTime.Now.ToLongTimeString());
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
