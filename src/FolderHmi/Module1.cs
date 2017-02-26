using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderHmi
{
    static class Module1
    {
        public static string OPCServerName = "KEPware.KEPServerEx.V5";
        public static Array TagList = new string[159];
        public static int TagCount = TagList.Length - 1;
        public static Array ItemServerHandles;
        public static OPCGroup _OPCGroup;
        public static Array ItemServerErrors;
        public static int[] OPCItemIsArray = new int[TagCount];

        //public static Array TagList = new string[] { "Channel1.PLC_SLOTTER.PB_1", "Channel1.PLC_SLOTTER.POS_REG", "Channel1.PLC_SLOTTER.POS_REG_L" };

        public static Array ValueList = new object[TagCount + 1];
        public static Array HandleList = new int[TagCount + 1];
        public static Array CachedTags = new Tag[10];
        public static List<KeyValuePair<int, int>> FormHandle = new List<KeyValuePair<int, int>>();
        public static decimal ApBrazo = 10;

        static Module1()
        {
            DataTable dt = DbManager.GetDataTable("SELECT handle,name,formid FROM tags ORDER BY handle");
            TagList = new string[dt.Rows.Count];
            int j = 0;
            foreach (DataRow dr in dt.Rows)
            {
                FormHandle.Add(new KeyValuePair<int, int>(int.Parse(dr.ItemArray[0].ToString()), int.Parse(dr.ItemArray[2].ToString())));
                HandleList.SetValue(dr.ItemArray[0], j);
                TagList.SetValue(dr.ItemArray[1], j++);
            }
        }

    }
}
