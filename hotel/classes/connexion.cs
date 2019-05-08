using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace hotel.classes
{
    class Connexion
    {
        private static SqlConnection _instance = null;
        private static object _lock = new object();

        public static SqlConnection Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bdhotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    return _instance;
                }
            }
        }
    }
}
