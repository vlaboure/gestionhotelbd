﻿using System;
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
                        _instance = new SqlConnection(@"Data Source=(LocalDb)\bdhotel;Integrated Security=True");
                    return _instance;
                }
            }
        }
    }
}
