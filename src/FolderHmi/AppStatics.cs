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
        public static int TagCount = 108;
        public static Array ItemServerHandles;
        public static OPCGroup _OPCGroup;
        public static Array ItemServerErrors;
        public static int[] OPCItemIsArray = new int[TagCount];
        public static Array TagList = new string[] {
            "",
            "Channel1.PLC_FOLDER.CALIBRAR_D121", "Channel1.PLC_FOLDER.CALIBRAR_D122", "Channel1.PLC_FOLDER.CALIBRAR_D123",
            "Channel1.PLC_FOLDER.CLEAR_FAULT_DRIVES","Channel1.PLC_FOLDER.D120_FAULT","Channel1.PLC_FOLDER.D121_FAULT",
            "Channel1.PLC_FOLDER.D122_FAULT","Channel1.PLC_FOLDER.D123_FAULT","Channel1.PLC_FOLDER.DIST_HOJA",
            "Channel1.PLC_FOLDER.DIST_TOTAL","Channel1.PLC_FOLDER.EN_ETHERNET_D120","Channel1.PLC_FOLDER.EN_ETHERNET_D121",
            "Channel1.PLC_FOLDER.EN_ETHERNET_D122","Channel1.PLC_FOLDER.EN_ETHERNET_D123","Channel1.PLC_FOLDER.EN_HSC",
            "Channel1.PLC_FOLDER.END_BRAZO_CDR","Channel1.PLC_FOLDER.END_BRAZO_GM","Channel1.PLC_FOLDER.END_BRAZO_LO",
            "Channel1.PLC_FOLDER.END_BRAZO_LT","Channel1.PLC_FOLDER.GO_NPOS_BRAZO_CDR","Channel1.PLC_FOLDER.GO_NPOS_BRAZO_GM",
            "Channel1.PLC_FOLDER.GO_NPOS_BRAZO_LO","Channel1.PLC_FOLDER.GO_NPOS_BRAZO_LT","Channel1.PLC_FOLDER.HOME_D121",
            "Channel1.PLC_FOLDER.HOME_D123","Channel1.PLC_FOLDER.HSC1_STATUS","Channel1.PLC_FOLDER.INI_BRAZO_CDR",
            "Channel1.PLC_FOLDER.INI_BRAZO_GM","Channel1.PLC_FOLDER.INI_BRAZO_LO","Channel1.PLC_FOLDER.INI_BRAZO_LT",
            "Channel1.PLC_FOLDER.IP_DRIVE_D120","Channel1.PLC_FOLDER.IP_DRIVE_D121","Channel1.PLC_FOLDER.IP_DRIVE_D122",
            "Channel1.PLC_FOLDER.IP_DRIVE_D123","Channel1.PLC_FOLDER.NPOS_BRAZO_CDR","Channel1.PLC_FOLDER.NPOS_BRAZO_GM",
            "Channel1.PLC_FOLDER.NPOS_BRAZO_LO","Channel1.PLC_FOLDER.NPOS_BRAZO_LT","Channel1.PLC_FOLDER.OFFSET_MAX",
            "Channel1.PLC_FOLDER.OFFSET_MIN","Channel1.PLC_FOLDER.POS_BRAZO_CDR","Channel1.PLC_FOLDER.POS_BRAZO_GM",
            "Channel1.PLC_FOLDER.POS_BRAZO_LT","Channel1.PLC_FOLDER.PUL_MAX","Channel1.PLC_FOLDER.PUL_MIN",
            "Channel1.PLC_FOLDER.REF_VAR","Channel1.PLC_FOLDER.REF_VAR2","Channel1.PLC_FOLDER.SYNC_HOJA",
            "Channel1.PLC_FOLDER.tu_aliviada","Channel1.PLC_FOLDER.V_MAX","Channel1.PLC_FOLDER.V_MAX_BAND",
            "Channel1.PLC_FOLDER.V_MIN","Channel1.PLC_FOLDER.V_MIN_BAND","Channel1.PLC_FOLDER.VALOR_CERO",
        };
        //public static Array TagList = new string[] { "Channel1.PLC_SLOTTER.PB_1", "Channel1.PLC_SLOTTER.POS_REG", "Channel1.PLC_SLOTTER.POS_REG_L" };

        public static Array ValueList = new object[108];
        public static Array HandleList = new int[108];
        public static Array OrderValueList = new Tag[10];
        public static decimal ApBrazo = 10;

        public AppStatics()
        {
        }

    }
}
