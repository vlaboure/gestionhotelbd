using System;
using System.Collections.Generic;
using System.Text;

namespace hotel.classes
{
    class NotAnumber : Exception
    {
        public NotAnumber() : base("Entrée non valide...") { } 
    }
}
