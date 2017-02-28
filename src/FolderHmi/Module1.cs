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
        public static List<Tag> TagList = new List<Tag>();
        public static int TagCount = 158;
        public static Array ItemServerHandles;
        public static OPCGroup _OPCGroup;
        public static Array ItemServerErrors;
        public static int[] OPCItemIsArray = new int[158];
        public static decimal ApBrazo = 10;

        static Module1()
        {
            DataTable dt = DbManager.GetDataTable("SELECT handle,name,formid,corr FROM tags ORDER BY handle");
            foreach (DataRow dr in dt.Rows)
            {
                TagList.Add(new Tag(int.Parse(dr.ItemArray[0].ToString()), dr.ItemArray[1].ToString(), 
                    int.Parse(dr.ItemArray[2].ToString()), int.Parse(dr.ItemArray[3].ToString())));
            }
        }

        internal static Array GetTagNameList()
        {
            return TagList.Select(x => x.Name).ToArray();
        }

        internal static Array GetHandleList()
        {
            return TagList.Select(x => x.Handle).ToArray();
        }
    }
}
