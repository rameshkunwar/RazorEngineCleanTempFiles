using GenerateEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEngineMailerParserService
{
    public class MarshalByRefTypeInService:MarshalByRefObject
    {
        public string CreateMailBodyInSeparateAppDomain()
        {
           return ParseEmail.MakeEmailBody();           
        }
    }
}
