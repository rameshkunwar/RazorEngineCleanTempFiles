using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailParserLibrary
{
    public class ParserEngine
    {
        private readonly IRazorEngineService _service;

        public ParserEngine(string templatePath)
        {
           
            var templatePathForWebApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", templatePath);

            var templatePathForConsoleApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templatePath);

            var config = new TemplateServiceConfiguration
            {
                TemplateManager = new ResolvePathTemplateManager(new[] { templatePathForWebApp, templatePathForConsoleApp })
            };
            _service = RazorEngineService.Create(config);
        }

        public virtual string CreateEmail<T>(string templateName, T model)
        {
            try
            {
                var key = _service.GetKey(templateName);
                return _service.RunCompile(key, typeof(T), model);
            }
            finally
            {
                _service.Dispose();
            }
        }
    }
}
