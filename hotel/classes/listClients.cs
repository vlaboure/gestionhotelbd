using System;
using System.Collections.Generic;
using System.Text;

namespace hotel.classes
{
    public class OlistClient
    {

        private List<Client> listeClients;
        public OlistClient()
        {
            ListeClients = new List<Client>();// on crée la liste des clients à la création de l'objet liste clients
        }

        public List<Client> ListeClients { get => listeClients; set => listeClients = value; }


        public Client AddClient(string n, string pn, string tel)
        {
            Client cl = new Client(n, pn, tel);
            ListeClients.Add(cl);
            return cl;
        }

        public Client Find(string nomCl)
        {
            Client cli=new Client();
            foreach (Client c in ListeClients)
            {
                
                if (nomCl == c.NomClient)
                {
                    cli = c;
                
                }
            }
            return cli;
        }

        public bool DelCl(string nomCl)//supprimer avec le nom
        {
            Client c = Find(nomCl);
            if (c != null)
            {
                ListeClients.Remove(c);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Client " + nomCl + " supprimé");
                Console.ResetColor();
                return true;

            }
            else
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CLIENT NON TROUVE");
                Console.ResetColor();
                return false;
            }

        }

        public void AffichListeCl()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("sortie de la liste des clients");
            Console.ResetColor();
            foreach (Client c in ListeClients)
            {
                Console.WriteLine(c.NomClient + " " + c.PrenomClient + "\t   téléphone : " + c.Tel);
            }
        }
    }
}
