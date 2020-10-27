using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEmail
{
    public class MarshalByRefType : MarshalByRefObject
    {
        public string CreateMailBodyInSeparateAppDomain()
        {
            var body = ParseEmail.MakeEmailBody();
            return body;
        }
    }
}
