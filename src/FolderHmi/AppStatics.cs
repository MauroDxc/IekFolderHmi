using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderHmi
{
    class AppStatics
    {
        public static string OPCServerName = "KEPware.KEPServerEx.V5";
        public static int TagCount = 2;
        public static List<Objects.Text> OPCTextList;
        public static Array ItemServerHandles;
        public static OPCGroup _OPCGroup;
        public static Array ItemServerErrors;
        public static Array TagList = new string[] { "Channel1.PLC_SLOTTER.PB_1", "Channel1.PLC_SLOTTER.POS_REG", "Channel1.PLC_SLOTTER.POS_REG_L" };
        public static Array HandleList = new int[] { 1, 2, 3 };
    }
}
