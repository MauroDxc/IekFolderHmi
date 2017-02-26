using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace FolderHmi
{
    class DbManager
    {
        private static String sconnection = "server=localhost;User Id=root;password=password;Persist Security Info=True;database=iekfolder; pooling=false";

        public static bool Update(string table, KeyValuePair<string, string> []values, KeyValuePair<string, string>[] filters)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                StringBuilder updateString = new StringBuilder("");
                StringBuilder whereString = new StringBuilder("");

                foreach (var v in values)
                {
                    updateString.Append(v.Key + "=" + v.Value + ",");
                }
                foreach (var v in filters)
                {
                    whereString.Append(v.Key + "=" + v.Value + ",");
                }
                updateString.Remove(updateString.Length - 1, 1);
                whereString.Remove(whereString.Length - 1, 1);

                string myInsertQuery = "UPDATE " + table + " SET " + updateString + " WHERE " + whereString;
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("El servidor contestó: " + e.Message, " Error Sevidor de datos",
             MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public static DataTable GetDataTable(string cmd)
        {
            MySqlConnection myConnection = new MySqlConnection(sconnection);
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd, myConnection);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            da.Fill(data);
            myConnection.Close();
            return data;

        }

        public static int Insert(String tabla, String datos)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                string myInsertQuery = "INSERT INTO " + tabla + " Values(" + datos + "); select @@IDENTITY;";
                DataTable data = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(myInsertQuery, myConnection);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
                da.Fill(data);
                myConnection.Close();
                int identity = int.Parse(data.Rows[0].ItemArray[0].ToString());
                return identity;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("El servidor contestó: " + e.Message, " Error Sevidor de datos",
             MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return -1;
            }

        }
        

    }
}

