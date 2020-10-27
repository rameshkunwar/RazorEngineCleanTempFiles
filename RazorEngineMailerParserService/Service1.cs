using GenerateEmail;
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
using System.Threading.Tasks;
using System.Timers;

namespace RazorEngineMailerParserService
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteOperationInfoToFile("RazorEngine service started at: " + DateTime.Now);
           timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

          

            timer.Interval = 5000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteOperationInfoToFile("RazorEngine service STOPPED at: " + DateTime.Now);
        }
        private void OnElapsedTime(Object source, ElapsedEventArgs args)
        {
            WriteOperationInfoToFile("RazorEngine service recalled at: " + DateTime.Now);
            try
            {
                //var body = ParseEmail.MakeEmailBody();
                //now generating email body in a proxy domain
                AppDomainSetup domainSetup = new AppDomainSetup();
                var executeAssembly = Assembly.GetExecutingAssembly().FullName;
                domainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
                domainSetup.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;             

                string proxyAppDomainName = $"RazorEngineProxyDomain-{Guid.NewGuid()}";
                //Create wrapper AppDomain
                AppDomain wrapperAppDomain = AppDomain.CreateDomain(proxyAppDomainName, null, domainSetup);

                WriteOperationInfoToFile("RazorEngine service. WrapperAppDomain name: "+wrapperAppDomain.FriendlyName);

                WriteOperationInfoToFile("RazorEngine service, now referencing proxy domain object");
                //MarshbyRefType proxy
                MarshalByRefTypeInService byRefType = (MarshalByRefTypeInService)wrapperAppDomain.CreateInstanceAndUnwrap(executeAssembly, typeof(MarshalByRefTypeInService).FullName);

                WriteOperationInfoToFile("RazorEngine service, parsing email now on a proxy app domain: " + proxyAppDomainName);
                var body = byRefType.CreateMailBodyInSeparateAppDomain();

                if (!string.IsNullOrEmpty(body))
                    WriteOperationInfoToFile("RazorEngine service email body generated");
                else
                    WriteOperationInfoToFile("RazorEngine service FAILED to create email body");

                //no let's unload the domain
                AppDomain.Unload(wrapperAppDomain);

                WriteOperationInfoToFile($"{proxyAppDomainName} unloaded! Temp file in /windows/temp folder deleted");
            }
            catch (Exception e)
            {

                WriteOperationInfoToFile("RazorEngine parser EXCEPTION: " + e.Message);
            }
        }

        public void WriteOperationInfoToFile(string msg)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\RazorServiceLogs";
            if (!File.Exists(path))
            {
                using(StreamWriter streamWriter = File.CreateText(path))
                {
                    streamWriter.WriteLine(msg);
                }
            }
            else
            {
                using(StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine(msg);
                }
            }
        }
    }
}
