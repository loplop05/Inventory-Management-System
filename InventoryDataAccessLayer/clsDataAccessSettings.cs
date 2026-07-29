using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace InventoryDataAccessLayer
{
    public class clsDataAccessSettings
    {


        // Connection string is now read from App.config to avoid hardcoding credentials in source code
        // TODO: For production, consider using encrypted configuration sections or environment variables
        public static string connectionString = ConfigurationManager.ConnectionStrings["InventoryDB"].ConnectionString;








    }
}
