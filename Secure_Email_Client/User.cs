using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secure_Email_Client
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string ImapServer { get; set; }
        public int ImapPort { get; set; }
        public bool Ssl { get; set; }
    }
}
