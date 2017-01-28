using OPCAutomation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderHmi
{
    public delegate void OpcDataChanged(object sender, Objects.OpcItemEventArgs e);
    class OpcManager
    {
        public event OpcDataChanged DataChanged;
        OPCServer _OPCServer;

        public OpcManager()
        {
            try
            {
                _OPCServer = new OPCAutomation.OPCServer();
                _OPCServer.Connect(AppStatics.OPCServerName, "");
            }
            catch (Exception ex)
            {
                _OPCServer = null;
            }
            try
            {
                _OPCServer.OPCGroups.DefaultGroupIsActive = true;
                _OPCServer.OPCGroups.DefaultGroupDeadband = 0;
                AppStatics._OPCGroup = _OPCServer.OPCGroups.Add("G1");
                AppStatics._OPCGroup.UpdateRate = 250;
                AppStatics._OPCGroup.IsSubscribed = true;
                AppStatics._OPCGroup.DataChange += OPCGroup_DataChanged;
            }
            catch (Exception ex)
            {
                _OPCServer = null;

            }
            try
            {
                //_OPCGroup.OPCItems.DefaultIsActive = true;
                AppStatics._OPCGroup.OPCItems.AddItems(AppStatics.TagCount, AppStatics.TagList, AppStatics.HandleList, out AppStatics.ItemServerHandles, out AppStatics.ItemServerErrors);
                bool itemgood = false;
                for (int i = 1; i <= AppStatics.TagCount; i++)
                {
                    int ab = (Int32)AppStatics.ItemServerErrors.GetValue(i);
                    if (ab == 0)
                    {
                        itemgood = true;
                    }
                }
                if (!itemgood)
                {
                }

            }
            catch (Exception ex)
            {
                _OPCServer = null;

            }
        }

        private void OPCGroup_DataChanged(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            Objects.OpcItemEventArgs e = new Objects.OpcItemEventArgs();
            e.ItemHandle = (int)ClientHandles.GetValue(1);
            e.ItemValue = ItemValues.GetValue(1) != null ? double.Parse(ItemValues.GetValue(1) + "") : 0.0;
            DataChanged(this, e);
        }
    }
}
