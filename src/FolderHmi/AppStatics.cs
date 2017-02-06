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
        public static Array TagList = new string[] {
            "",
            "Channel1.PLC_FOLDER.CALIBRAR_D120",
            "Channel1.PLC_FOLDER.CALIBRAR_D121",
            "Channel1.PLC_FOLDER.CALIBRAR_D122",
            "Channel1.PLC_FOLDER.CALIBRAR_D123",
            "Channel1.PLC_FOLDER.CLEAR_FAULT_DRIVES", //5
            "Channel1.PLC_FOLDER.D120_FAULT",
            "Channel1.PLC_FOLDER.D121_FAULT",
            "Channel1.PLC_FOLDER.D122_FAULT",
            "Channel1.PLC_FOLDER.D123_FAULT",
            "Channel1.PLC_FOLDER.DIST_HOJA", //10
            "Channel1.PLC_FOLDER.DIST_TOTAL",
            "Channel1.PLC_FOLDER.EN_ETHERNET_D120",
            "Channel1.PLC_FOLDER.EN_ETHERNET_D121",
            "Channel1.PLC_FOLDER.EN_ETHERNET_D122",
            "Channel1.PLC_FOLDER.EN_ETHERNET_D123", //15
            "Channel1.PLC_FOLDER.EN_HSC",
            "Channel1.PLC_FOLDER.END_BRAZO_CDR",
            "Channel1.PLC_FOLDER.END_BRAZO_GM",
            "Channel1.PLC_FOLDER.END_BRAZO_LO",
            "Channel1.PLC_FOLDER.END_BRAZO_LT", //20
            "Channel1.PLC_FOLDER.GO_NPOS_BRAZO_CDR",
            "Channel1.PLC_FOLDER.GO_NPOS_BRAZO_GM",
            "Channel1.PLC_FOLDER.GO_NPOS_BRAZO_LO",
            "Channel1.PLC_FOLDER.GO_NPOS_BRAZO_LT", 
            "Channel1.PLC_FOLDER.HOME_D121", //25
            "Channel1.PLC_FOLDER.HOME_D123",
            "Channel1.PLC_FOLDER.HSC1_STATUS",
            "Channel1.PLC_FOLDER.INI_BRAZO_CDR",
            "Channel1.PLC_FOLDER.INI_BRAZO_GM",
            "Channel1.PLC_FOLDER.INI_BRAZO_LO", //30
            "Channel1.PLC_FOLDER.INI_BRAZO_LT",
            "Channel1.PLC_FOLDER.IP_DRIVE_D120",
            "Channel1.PLC_FOLDER.IP_DRIVE_D121",
            "Channel1.PLC_FOLDER.IP_DRIVE_D122",
            "Channel1.PLC_FOLDER.IP_DRIVE_D123", //35
            "Channel1.PLC_FOLDER.NPOS_BRAZO_CDR",
            "Channel1.PLC_FOLDER.NPOS_BRAZO_GM",
            "Channel1.PLC_FOLDER.NPOS_BRAZO_LO",
            "Channel1.PLC_FOLDER.NPOS_BRAZO_LT",
            "Channel1.PLC_FOLDER.OFFSET_MAX", //40
            "Channel1.PLC_FOLDER.OFFSET_MIN",
            "Channel1.PLC_FOLDER.POS_BRAZO_CDR",
            "Channel1.PLC_FOLDER.POS_BRAZO_GM",
            "Channel1.PLC_FOLDER.POS_BRAZO_LT",
            "Channel1.PLC_FOLDER.PUL_MAX", //45
            "Channel1.PLC_FOLDER.PUL_MIN",
            "Channel1.PLC_FOLDER.REF_VAR",
            "Channel1.PLC_FOLDER.REF_VAR2",
            "Channel1.PLC_FOLDER.SYNC_HOJA",
            "Channel1.PLC_FOLDER.tu_aliviada", //50
            "Channel1.PLC_FOLDER.V_MAX",
            "Channel1.PLC_FOLDER.V_MAX_BAND",
            "Channel1.PLC_FOLDER.V_MIN",
            "Channel1.PLC_FOLDER.V_MIN_BAND",
            "Channel1.PLC_FOLDER.VALOR_CERO", //55
            "",
            "",
            "",
            "",
            "", //60
            "",
            "",
            "",
            "",
            "", //65

            "Channel1.PLC_FOLDER.C_FAULT_D110", 
            "Channel1.PLC_FOLDER.C_FAULT_D111",
            "Channel1.PLC_FOLDER.C_FAULT_D112",
            "Channel1.PLC_FOLDER.C_FAULT_D114",
            "Channel1.PLC_FOLDER.C_FAULT_D115", //70
            "Channel1.PLC_FOLDER.CALIBRAR_D110",
            "Channel1.PLC_FOLDER.CALIBRAR_D111",
            "Channel1.PLC_FOLDER.CALIBRAR_D112",
            "Channel1.PLC_FOLDER.CALIBRAR_D113",
            "Channel1.PLC_FOLDER.CALIBRAR_D114", //75
            "Channel1.PLC_FOLDER.CALIBRAR_D115",
            "Channel1.PLC_FOLDER.D110_FAULT",
            "Channel1.PLC_FOLDER.D111_FAULT",
            "Channel1.PLC_FOLDER.D112_FAULT",
            "Channel1.PLC_FOLDER.D113_FAULT", //80
            "Channel1.PLC_FOLDER.D114_FAULT",
            "Channel1.PLC_FOLDER.D115_FAULT",
            "Channel1.PLC_FOLDER.EN_ETHERNET_D110",
            "Channel1.PLC_FOLDER.EN_ETHERNET_D111",
            "Channel1.PLC_FOLDER.EN_ETHERNET_D112", //85
            "Channel1.PLC_FOLDER.EN_ETHERNET_D113",
            "Channel1.PLC_FOLDER.EN_ETHERNET_D114",
            "Channel1.PLC_FOLDER.EN_ETHERNET_D115",
            "Channel1.PLC_FOLDER.END_CUELLO_A",
            "Channel1.PLC_FOLDER.END_CUELLO_B", //90
            "Channel1.PLC_FOLDER.END_CUELLO_C",
            "Channel1.PLC_FOLDER.END_CUELLO_D",
            "Channel1.PLC_FOLDER.END_CUELLO_REG_E",
            "Channel1.PLC_FOLDER.END_CUELLO_REG_F",
            "Channel1.PLC_FOLDER.GO_NPOS_CUELLO_A", //95
            "Channel1.PLC_FOLDER.GO_NPOS_CUELLO_B",
            "Channel1.PLC_FOLDER.GO_NPOS_CUELLO_C",
            "Channel1.PLC_FOLDER.GO_NPOS_CUELLO_D",
            "Channel1.PLC_FOLDER.GO_NPOS_REG_E",
            "Channel1.PLC_FOLDER.GO_NPOS_REG_F", //100
            "Channel1.PLC_FOLDER.HOME_D110",
            "Channel1.PLC_FOLDER.HOME_D111",
            "Channel1.PLC_FOLDER.HOME_D112",
            "Channel1.PLC_FOLDER.HOME_D113",
            "Channel1.PLC_FOLDER.HOME_D114", //105
            "Channel1.PLC_FOLDER.HOME_D115",
            "Channel1.PLC_FOLDER.IN_CUELLO_REG_E",
            "Channel1.PLC_FOLDER.IN_CUELLO_REG_F",
            "Channel1.PLC_FOLDER.INI_CUELLO_A",
            "Channel1.PLC_FOLDER.INI_CUELLO_B", //110
            "Channel1.PLC_FOLDER.INI_CUELLO_C",
            "Channel1.PLC_FOLDER.INI_CUELLO_D",
            "Channel1.PLC_FOLDER.IP_DRIVE_A",
            "Channel1.PLC_FOLDER.IP_DRIVE_B",
            "Channel1.PLC_FOLDER.IP_DRIVE_C", //115
            "Channel1.PLC_FOLDER.IP_DRIVE_D",
            "Channel1.PLC_FOLDER.IP_DRIVE_E",
            "Channel1.PLC_FOLDER.IP_DRIVE_F",
            "Channel1.PLC_FOLDER.NPOS_A",
            "Channel1.PLC_FOLDER.NPOS_B", //120
            "Channel1.PLC_FOLDER.NPOS_C",
            "Channel1.PLC_FOLDER.NPOS_D",
            "Channel1.PLC_FOLDER.NPOS_REG_E",
            "Channel1.PLC_FOLDER.NPOS_REG_F",
            "Channel1.PLC_FOLDER.POS_A", //125
            "Channel1.PLC_FOLDER.POS_B",
            "Channel1.PLC_FOLDER.POS_C",
            "Channel1.PLC_FOLDER.POS_D",
            "Channel1.PLC_FOLDER.POS_REG_E",
            "Channel1.PLC_FOLDER.POS_REG_F", //130
            "Channel1.PLC_FOLDER.VEL_MAX_NPOS",
            "Channel1.PLC_FOLDER.VEL_MIN_NPOS",
            
        };
        public static int TagCount = TagList.Length - 1;
        public static Array ItemServerHandles;
        public static OPCGroup _OPCGroup;
        public static Array ItemServerErrors;
        public static int[] OPCItemIsArray = new int[TagCount];
        
        //public static Array TagList = new string[] { "Channel1.PLC_SLOTTER.PB_1", "Channel1.PLC_SLOTTER.POS_REG", "Channel1.PLC_SLOTTER.POS_REG_L" };

        public static Array ValueList = new object[TagCount + 1];
        public static Array HandleList = new int[TagCount + 1];
        public static Array CachedTags = new Tag[10];
        public static decimal ApBrazo = 10;

        public AppStatics()
        {
            for (int i = 1; i < TagList.Length; i++)
            {
                HandleList.SetValue(i, i);
            }
        }

    }
}
