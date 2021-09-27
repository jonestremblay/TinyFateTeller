using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace BirthEntryServiceDAO_WCF.Connections
{
    class Database
    {
        private static MySqlConnection cnx = null;
        private static String connectionString = "Server=sql5.freemysqlhosting.net;Uid=sql5440579;PWd=6Ps5JxudLG;Database=sql5440579";
        public static MySqlConnection GetConnection()
        {
            if (cnx == null)
            {
                cnx = new MySqlConnection();
                cnx.ConnectionString = connectionString;
                cnx.Open();
            }
            else if (cnx.State == ConnectionState.Closed || cnx.State == ConnectionState.Broken)
            {
                cnx.Open();
            }
            return cnx;
        }
        public static void Close() 
        {
            if (cnx != null)
            {
                cnx.Close();
            }
        
        }
    }
}
