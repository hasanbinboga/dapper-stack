using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame.Common.Utils.FileManagement
{
    public class SaveFileArg
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
    }
}
