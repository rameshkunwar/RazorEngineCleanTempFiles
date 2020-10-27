using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("parsing email");

            Console.WriteLine("now parsing email on another app domain");
            Console.WriteLine("\tCurrent AppDomain: " + AppDomain.CurrentDomain.FriendlyName);

            AppDomainSetup domainSetup = new AppDomainSetup();
            var executeAssembly = Assembly.GetExecutingAssembly().FullName;
            domainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            domainSetup.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            string proxyAppDomainName = $"RazorEngineProxyDomain-{Guid.NewGuid()}";

            //Create wrapper AppDomain
            AppDomain wrapperAppDomain = AppDomain.CreateDomain(proxyAppDomainName, null, domainSetup);
            Console.WriteLine("wrapper app domain name: " + wrapperAppDomain.FriendlyName);
            //MarshbyRefType proxy
            MarshalByRefType byRefType = (MarshalByRefType)wrapperAppDomain.CreateInstanceAndUnwrap(executeAssembly, typeof(MarshalByRefType).FullName);

          
            var emailbody = byRefType.CreateMailBodyInSeparateAppDomain();

            //var emailBody = ParseEmail.MakeEmailBody();
            Console.WriteLine("email parsed");

            //no let's unload the domain
            AppDomain.Unload(wrapperAppDomain);
            Console.WriteLine("proxy domain unloaded");
            Console.Read();
        }
    }
}
