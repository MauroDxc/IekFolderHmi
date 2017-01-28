using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderHmi.Objects
{
    public class OpcItemEventArgs
    {
        public int ItemHandle { get; set; }
        public double ItemValue { get; set; }
    }
}
