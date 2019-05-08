using System;
using System.Collections.Generic;
using System.Text;

namespace hotel.classes 
{
    class NotAstring : Exception
    {

        public NotAstring() : base("Entrée non valide...")
        {

        }
    }
}
