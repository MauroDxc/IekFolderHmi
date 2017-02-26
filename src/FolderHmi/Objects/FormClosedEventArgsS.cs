using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderHmi.Objects
{
    public class FormClosedEventArgsS : FormClosedEventArgs
    {
        public object CloseArguments { get; set; }

        public FormClosedEventArgsS(CloseReason closeReason) : base(closeReason)
        {
            
        }

        public FormClosedEventArgsS(CloseReason closeReason, object args) : base(closeReason)
        {
            CloseArguments = args;
        }
    }
}
