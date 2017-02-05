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
    public delegate void StatusMessageDelegate(object sender, string message);

    class OpcManager
    {
        public event OpcDataChanged DataChanged;
        public static OpcManager Instance = new OpcManager();
        OPCServer _OPCServer;

        public event StatusMessageDelegate StatusMessageChanged;
        private string _statusMessage;
        public string StatusMessage
        {
            set
            {
                if (Instance.StatusMessageChanged != null)
                {
                    Instance.StatusMessageChanged(null, _statusMessage);
                }
            }
            private get { return _statusMessage; }
        }

        public enum CanonicalDataTypes
        {
            CanonDtByte = 17,
            CanonDtChar = 16,
            CanonDtWord = 18,
            CanonDtShort = 2,
            CanonDtDWord = 19,
            CanonDtLong = 3,
            CanonDtFloat = 4,
            CanonDtDouble = 5,
            CanonDtBool = 11,
            CanonDtString = 8,
        }

        private OpcManager()
        {
            try
            {
                _OPCServer = new OPCAutomation.OPCServer();
                _OPCServer.Connect(AppStatics.OPCServerName, "");
            }
            catch (Exception ex)
            {
                _OPCServer = null;
                throw;
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
                throw;

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
                throw;

            }
        }

        public bool Write(int index)
        {
            try
            {
                // Write only 1 item
                short ItemCount = 1;

                // Create some local scope variables to hold the value to be sent.
                // These arrays could just as easily contain all of the item we have added.
                int[] SyncItemServerHandles = new int[2];
                object[] SyncItemValues = new object[2];
                System.Array SyncItemServerErrors = null;
                OPCAutomation.OPCItem AnOpcItem = default(OPCAutomation.OPCItem);

                // Get the Servers handle for the desired item.  The server handles
                // were returned in add item subroutine.
                SyncItemServerHandles[1] = (int)AppStatics.ItemServerHandles.GetValue(index);
                AnOpcItem = AppStatics._OPCGroup.OPCItems.GetOPCItem((int)AppStatics.ItemServerHandles.GetValue(index));

                // Load the value to be written using Item's Canonical Data Type to
                // convert to correct type. 
                // See Kepware Application note on Canonical Data Types
                Array ItsAnArray = null;
                short CanonDT = 0;
                short vbArray = 8192;

                CanonDT = AnOpcItem.CanonicalDataType;


                // If it is an array, figure out the base type
                if (CanonDT > vbArray)
                {
                    CanonDT -= vbArray;
                }

                switch (CanonDT)
                {
                    case (short)CanonicalDataTypes.CanonDtByte:
                        if ((int)AppStatics.OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(byte), (int)AppStatics.OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, (string)AppStatics.TagList.GetValue(index)))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToByte((string)AppStatics.ValueList.GetValue(index));
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtChar:
                        if ((int)AppStatics.OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(SByte), (int)AppStatics.OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, (string)AppStatics.TagList.GetValue(index)))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToSByte((string)AppStatics.ValueList.GetValue(index));
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtWord:
                        if ((int)AppStatics.OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(UInt16), (int)AppStatics.OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, (string)AppStatics.TagList.GetValue(index)))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToUInt16(AppStatics.ValueList.GetValue(index));
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtShort:
                        if ((int)AppStatics.OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(Int16), (int)AppStatics.OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, (string)AppStatics.TagList.GetValue(index)))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToInt16((string)AppStatics.ValueList.GetValue(index));
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtDWord:
                        if ((int)AppStatics.OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(UInt32), (int)AppStatics.OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, (string)AppStatics.TagList.GetValue(index)))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToUInt32((string)AppStatics.ValueList.GetValue(index));
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtLong:
                        if ((int)AppStatics.OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(Int32), (int)AppStatics.OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, (string)AppStatics.TagList.GetValue(index)))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToInt32((string)AppStatics.ValueList.GetValue(index));
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtFloat:
                        if ((int)AppStatics.OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(float), (int)AppStatics.OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, (string)AppStatics.TagList.GetValue(index)))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToSingle((string)AppStatics.ValueList.GetValue(index));
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtDouble:
                        if ((int)AppStatics.OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(double), (int)AppStatics.OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, (string)AppStatics.TagList.GetValue(index)))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = AppStatics.ValueList.GetValue(index);
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtBool:
                        if ((int)AppStatics.OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(bool), (int)AppStatics.OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, (string)AppStatics.TagList.GetValue(index)))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToBoolean(AppStatics.ValueList.GetValue(index));
                        }
                        break;
                    // End case

                    case (short)CanonicalDataTypes.CanonDtString:
                        if ((int)AppStatics.OPCItemIsArray.GetValue(index) > 0)
                        {
                            ItsAnArray = Array.CreateInstance(typeof(string), (int)AppStatics.OPCItemIsArray.GetValue(index));
                            if (!LoadArray(ref ItsAnArray, CanonDT, (string)AppStatics.TagList.GetValue(index)))
                            {
                                return false;
                            }
                            SyncItemValues[1] = (object)ItsAnArray;
                        }
                        else
                        {
                            SyncItemValues[1] = Convert.ToString((string)AppStatics.ValueList.GetValue(index));
                        }
                        break;
                    // End case

                    default:
                        //MessageBox.Show("OPCItemWriteButton Unknown data type", "Error al escribir", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                        // End case
                }

                // Invoke the SyncWrite operation.  Remember this call will wait until completion
                AppStatics._OPCGroup.SyncWrite(ItemCount, SyncItemServerHandles, SyncItemValues, out SyncItemServerErrors);

                if ((int)SyncItemServerErrors.GetValue(1) != 0)
                {
                    //MessageBox.Show("SyncItemServerError: " + SyncItemServerErrors.GetValue(1));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // Error handling
                //MessageBox.Show("OPC server write item failed with exception: " + ex.Message + " INDEX = " + index.ToString() + " c=" + opcItemValueToWritte[index].Text + "/", "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                return false;
            }


        }


        public static bool LoadArray(ref System.Array AnArray, int CanonDT, string wrTxt)
        {
            int ii = 0;
            int loc = 0;
            int Wlen = 0;
            int start = 0;

            try
            {
                start = 1;
                Wlen = wrTxt.Length;
                for (ii = AnArray.GetLowerBound(0); ii <= AnArray.GetUpperBound(0); ii++)
                {
                    loc = wrTxt.IndexOf(",", 0);
                    if (ii < AnArray.GetUpperBound(0))
                    {
                        if (loc == 0)
                        {
                            //MessageBox.Show("Valor escrito: Numero incorrecto de digitos para el tamaño del arreglo?", "Error de argumento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }
                    else
                    {
                        loc = Wlen + 1;
                    }

                    switch (CanonDT)
                    {
                        case (int)CanonicalDataTypes.CanonDtByte:
                            AnArray.SetValue(Convert.ToByte((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtChar:
                            AnArray.SetValue(Convert.ToSByte((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case


                        case (int)CanonicalDataTypes.CanonDtWord:
                            AnArray.SetValue(Convert.ToUInt16((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtShort:
                            AnArray.SetValue(Convert.ToInt16((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtDWord:
                            AnArray.SetValue(Convert.ToInt32((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtLong:
                            AnArray.SetValue(Convert.ToInt32((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtFloat:
                            AnArray.SetValue(Convert.ToSingle((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtDouble:
                            AnArray.SetValue(Convert.ToDouble((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtBool:
                            AnArray.SetValue(Convert.ToBoolean((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        case (int)CanonicalDataTypes.CanonDtString:
                            AnArray.SetValue(Convert.ToString((wrTxt.Substring(start, loc - start))), ii);
                            break;
                        // End case

                        default:
                            //MessageBox.Show("El tipo de valor que se intenta escribir es desconocido", "Error de argumento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                    }

                    start = loc + 1;
                }

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Al intentar escribir el vaor se genero la siguiente excepción: " + ex.Message, "Excepción de OPC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }


        private void OPCGroup_DataChanged(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            Objects.OpcItemEventArgs e = new Objects.OpcItemEventArgs();
            for (int i = 1; i <= ClientHandles.Length; i++)
            {
                if (ItemValues.GetValue(i) == null) continue;
                e.ItemHandle = (int)ClientHandles.GetValue(i);
                if (typeof(double) == ItemValues.GetValue(i).GetType())
                {
                    e.ItemValue = ItemValues.GetValue(i) != null ? (double)ItemValues.GetValue(i) : 0.0;
                }
                else if (typeof(bool) == ItemValues.GetValue(i).GetType())
                {
                    e.ItemValue = ItemValues.GetValue(i) != null ? (bool)ItemValues.GetValue(i) : false;
                    e.IsFault = (bool)e.ItemValue && (e.ItemHandle <= 9 && e.ItemHandle >= 6);
                }
                else if (typeof(byte) == ItemValues.GetValue(i).GetType())
                {
                    e.ItemValue = ItemValues.GetValue(i) != null ? (byte)ItemValues.GetValue(i) : new Byte();
                }
                else if (typeof(string) == ItemValues.GetValue(i).GetType())
                {
                    e.ItemValue = ItemValues.GetValue(i) != null ? (string)ItemValues.GetValue(i) : "";
                }
                DataChanged(this, e);
            }
        }
    }
}
