using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secure_Email_Client
{
    public class StringClass
    {
        private string str;

        public StringClass(string str)
        {
            Str = str;
        }

        public string Str
        {
            set { str = value; }
            get { return str; }
        }
    }
}
