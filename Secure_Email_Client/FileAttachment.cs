using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secure_Email_Client
{
    public class FileAttachment
    {
        public string FileName { get; set; }
        public string FilePathName { get; set; }

        public FileAttachment(string filename, string filepathname)
        {
            FileName = filename;
            FilePathName = filepathname;
        }
    }
}
