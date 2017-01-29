using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderHmi
{
    class Tag
    {
        public decimal Value { get; set; }
        public Control Actual { get; set; }
        public Control Objetivo { get; set; }
        public Control MoverA { get; set; }

        public Tag()
        {

        }

        public Tag(decimal value, Control act, Control obj, Control mova)
        {
            Value = value;
            Actual = act;
            Objetivo = obj;
            MoverA = mova;
        }
    }
}
