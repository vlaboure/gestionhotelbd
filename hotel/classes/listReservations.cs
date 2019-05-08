using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace hotel.classes
{
    public class OlisteReservations
    {
        public const int maxReserv = 1;
        private List<Reservation> listeReservation;
        public List<string> fullReserv_Date;//liste contenant les dates pleines
        public event Action<string> ChangReserv;
        public event Action<string> FullHotel;
        private bool nonConform;
        private bool quit;
       
        

        public OlisteReservations()

        {
            listeReservation = new List<Reservation>();// on crée la liste des réservations à la création de l'objet liste réservations
            fullReserv_Date = new List<string>();
            Quit = false;// si hotel plein
            FullHotel += Avertissement;
            FullHotel += InfoSite;
            NonConform = false;

        }

        public List<Reservation> ListeReservation { get => listeReservation; set => listeReservation = value; }
        public bool Quit { get => quit; set => quit = value; }
        public bool NonConform { get => nonConform; set => nonConform = value; }

        public void Avertissement(string date)
        {
            string Mess = "";
            Console.WriteLine("Nombre Maximum des chambres réservées à la date du :" + date);
            Console.WriteLine("Voulez vous diffuser automatiquementl'information sur à la page réservations ?\n o/n");// juste pour en avoir un deuxième event
            Mess = Console.ReadLine();
            if (Mess != "o")
            {
                Console.WriteLine("Touche non gérée");
                Environment.Exit(0);

            }
               
        }

        public void InfoSite(string date)
        {
            //FullHotel(date);
            Console.WriteLine("ICI ON APPELLE LA METHODE ....");
    
        }

        public bool AddReserv(Client cli, string dt)
        {
            bool res = true;
            int cpt = 0; // pour compter les réservations aux dates..
            string datReserv=""; // date stockée en int
            string stat="";
            string dtFull="";
            Reservation Reserv = new Reservation(dt, cli, "rec");// "rec"= reservation enregistrée
            foreach(Reservation r in listeReservation)
            {
                stat = r.Statut;
                datReserv = r.DateReservation;
                cpt = (dt == datReserv && stat=="rec" ) ? cpt+1 :cpt;// si date trouvée et statut enregisté, incrément du compteur de reservation par date
            }
            if(cpt >= maxReserv)// on a atteint le max de reservation
            {
                dtFull = fullReserv_Date.Find(item => item.Contains(datReserv));
       
                if (dtFull == null)
                {// comme la date n'est pas deja ds la liste ajouter
                    fullReserv_Date.Add(datReserv);        
                }
                FullHotel(datReserv);// even hotel plein à cette date
                res = false;
            }
            else
            {
                ListeReservation.Add(Reserv);
                Console.WriteLine("réservation pour le client " + cli.NomClient + " enregistrée");
            }
            return res;
        }

        public Reservation Find_(string code)
        {
            Reservation res=null;
            foreach (Reservation r in ListeReservation)
            {

                if (code == r.CodeReservation)
                {
                    res = r;
                    break;
                }
            }
            return res;
        }

        public void ReservNoFind()
        {
            Quit = true;
            ChangReserv("");
        }

        public bool ChgReserv(string code)
        {
            string FullExtist;
            Reservation reserv = new Reservation();
            reserv = Find_(code);
            bool res = true;

            if (reserv == null)
            {
                //Quit = true;
                //ChangReserv("");
                //res = false;
            }
            else
            {
                if (NonConform == false)
                {
                    //comme on passe le statut de reservation d'enregistrement à cloturé enlever de la liste des fullreserv si im y est
                    ChangReserv(reserv.Statut);
                    if (reserv.Statut == "rec")
                    {
                        string dtReserv = reserv.DateReservation;
                        reserv.Statut = "ovr";
                        FullExtist = fullReserv_Date.Find(item => item == dtReserv);//FullExist contient la date de fullreserv si existe
                        if (FullExtist != null) fullReserv_Date.Remove(dtReserv);
                    }
                    else
                    {   // ici ajouter à la liste fullreserv si maxreserv atteint
                        reserv.Statut = "rec";
                    }
                    NonConform = false;
                }

                

                if (Quit != true)
                {
                    AffichListReserv();
                    res = true;
                }
                Quit = false;
            }
            
            return res;
            
        }

        public void AffichListReserv()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("sortie de la liste des clients");
            Console.ResetColor();
            foreach (Reservation r in ListeReservation)
            {
                Console.WriteLine(r.CodeReservation+ " \t" + r.Client.NomClient + " " + r.Client.PrenomClient + "\t   date réservation : " + r.DateReservation+ "\t statut de la réservation : "+ r.Statut);
            }
            Console.ReadLine();
        }

    }
}
