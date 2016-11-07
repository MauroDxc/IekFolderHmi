using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace FolderHmi
{
    class DbManager
    {
        private static MySqlConnection conn;

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

    }
}

