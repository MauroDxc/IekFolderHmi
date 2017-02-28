using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderHmi
{
    public class Tag
    {
        public object Value { get; set; }
        public int Handle { get; set; }
        public int FormId { get; set; }
        public int Corr { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }

        public Tag()
        {

        }

        public Tag(int handle, string name, int formid, int corr)
        {
            Handle = handle;
            Name = name;
            FormId = formid;
            Corr = corr;
            Timestamp = DateTime.Now;
        }
    }
}
