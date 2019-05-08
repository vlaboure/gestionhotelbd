using System;
using System.Collections.Generic;
using System.Text;

namespace hotel.classes
{
    public class Reservation
    {
        private string codeReservation;
        private Client client;
        private string statut;// "rec" = enregistrée  "ovr"= terminée
        private string dateReservation;
        public event Action<string> ChangReserv;
        public string CodeReservation { get => codeReservation; set => codeReservation = value; }
        public string Statut { get => statut; set => statut = value; }
        internal Client Client { get => client; set => client = value; }
        public string DateReservation { get => dateReservation; set => dateReservation = value; }

        public Reservation(string dateReserv, Client cl,string stat)
        {
            CodeReservation = Guid.NewGuid().ToString();
            DateReservation = dateReserv;
         
            Client = cl;
            Statut = stat;
        }
        public Reservation()
        {

        }

    }
}
