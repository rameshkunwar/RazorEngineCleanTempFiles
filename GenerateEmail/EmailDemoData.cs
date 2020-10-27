using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEmail
{
    public class EmailDemoData
    {

        public EmailDemoData()
        {
            MyPerson = new MyPerson { Id = 123, FirstName = "John", LastName = "Wayne" };
        }
        public MyPerson MyPerson { get; set; }

    }

    public class MyPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
