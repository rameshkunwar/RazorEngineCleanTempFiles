using EmailParserLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEmail
{
    public class ParseEmail
    {
        public static string MakeEmailBody()
        {
            var parserEngine = new ParserEngine(@"email");

            var model = new EmailDemoData();
            string body = parserEngine.CreateEmail("template_welcome", model);

            return body;
        }
    }
}
