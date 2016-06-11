using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAutodialerService
{
    public partial class HospitalAutodialerService : ServiceBase
    {
        private WorkClass MiWorkClass;
        public HospitalAutodialerService()
        {
            InitializeComponent();
            MiWorkClass = new WorkClass();
        }

        protected override void OnStart(string[] args)
        {
            MiWorkClass.Start();
        }

        protected override void OnStop()
        {
            MiWorkClass.Stop();
        }
    }
}
