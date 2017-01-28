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
        private static MySqlConnection conn;
        String sconnection = "server=localhost;User Id=root;password=password;Persist Security Info=True;database=iekfolder";

        private static void Connect()
        {
            if (conn != null)
            {
                conn.Close();
            }
            string connStr = String.Format("server={0};user id={1}; password={2}; database=iekfolder; pooling=false", "localhost", "root", "password");
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }

        public static DataTable GetDataTable(string cmd)
        {
            Connect();
            DataTable data = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd, conn);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            da.Fill(data);
            return data;

        }

        public bool insert(String tabla, String datos)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(sconnection);
                string myInsertQuery = "INSERT INTO " + tabla + " Values(" + datos + ")";
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
                MessageBox.Show("Valor agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("El servidor contestó: " + e.Message, " Error Sevidor de datos",
             MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

        }

    }
}

