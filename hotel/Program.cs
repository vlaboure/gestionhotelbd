using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.IO;
using hotel.classes;
using System.Text.RegularExpressions;

namespace hotel
{
    class program

    {
        // on crée les objets liste clients et liste des réservations
        // contenant les méthodes et listes
        //client ---> ajouter ou supprimer client + liste client
        // reservation---> ajouter ou supprimer réservation + liste reservation
        //****************-------------------***************************
        public static OlisteReservations listDesReservations = new OlisteReservations();
        public static OlistClient listDesClients = new OlistClient();
        public static bool HotelPlein = false;
        public static string fileHotel;
        public static bool numOk = true;
        public static bool hotelPlein { get; private set; }
        //public static Client cli = new Client();
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //Client cli = new Client();
            Menu();
        }

        //static int Entree(int choix) {
        //    try
        //    {
        //        do
        //        {
        //            string temp = Console.ReadLine();
        //            if (TestVal(temp) != false)
        //            {
        //                choix = Convert.ToInt32(temp);
        //                numOk = true;
        //            }
        //            else numOk = false;


        //        } while (numOk == false);


        //    }
        //    catch (NotAnumber ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.ReadLine();
        //    }
        //}

        static bool TestVal(string ch)
        {
            bool res = true;
            Regex rgx = new Regex(@"^[0-9]");
            if (!rgx.IsMatch(ch))
            {
                res = false;
                throw new NotAnumber();
            }
            return res;
        }

        static void Menu()
        {
            Console.WriteLine("Console de réservation de chambres");
            int choix = 0;
            if (!hotelPlein)
            {
                //Console.WriteLine("1- Gestion client");
                do
                {
                    Console.Clear();
                    Console.WriteLine("1- Gestion des clients");
                    Console.WriteLine("2- Gestion réservations");
                    Console.WriteLine("0-Quitter");
                    do
                    {
                        try
                        {
                            string temp = Console.ReadLine();
                            numOk = TestVal(temp);
                            if (numOk != false)
                            {
                                choix = Convert.ToInt32(temp);
                                numOk = true;
                            }
                            else numOk = false;
                        }
                        catch (NotAnumber ex)
                        {
                            Console.WriteLine(ex.Message);
                            numOk = false;

                        }
                    } while (numOk == false);

                    switch (choix)
                    {
                        case 1:
                            MenuCl();
                            break;
                        case 2:
                            MenuReservation();
                            break;
                        case 0:
                            StreamWriter writer = new StreamWriter(fileHotel + ".txt");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Pas de fonction");
                            break;
                    }
                } while (choix != 0);
            }
            else
            {
                Console.WriteLine("Désolé l'hotel est complet ");
                Console.ReadLine();
            }
        }

        static void MenuCl()
        {

            int choix = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1- Affichage de la liste des clients, et détail de chaque client");
                Console.WriteLine("2- Ajout d’un nouveau client  ");
                Console.WriteLine("3- suppression d’un client existant");
                Console.WriteLine("4-Quitter");
                Console.ResetColor();
                do
                {
                    try
                    {

                        string temp = Console.ReadLine();
                        if (TestVal(temp) != false)
                        {
                            choix = Convert.ToInt32(temp);
                            numOk = true;
                        }
                        else numOk = false;


                    }



                    catch (NotAnumber ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                } while (numOk == false);

                switch (choix)
                {
                    case 1:

                        AffichCl();
                        break;
                    case 2:

                        AddCl();
                        break;
                    case 3:

                        DelClient();
                        break;
                    case 4:
                        Menu();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Pas de fonction");
                        break;
                }
            } while (choix != 0);
        }

        static void MenuReservation()
        {
            int choix;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("1- Ajout réservation");
                Console.WriteLine("2- Changer le statut de la réservation");
                Console.WriteLine("3- Afficher les réservations");
                Console.WriteLine("4-Quitter");
                choix = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();
                switch (choix)
                {
                    case 1:
                        AddReservation();
                        break;
                    case 2:
                        ChangReservation();
                        break;
                    case 3:
                        AffichReservation();
                        break;
                    case 4:
                        Menu();
                        Console.ResetColor();
                        break;
                    default:
                        Console.WriteLine("Pas de fonction");
                        break;
                }
            } while (choix != 0);
        }


        //----------------------ajouter client dans la base
        static Client AddCl()
        {
            Client cli = new Client();
            string nom = "";
            string prenom = "";
            string tel = "";
            bool test = true;

            do
            {
                Console.WriteLine("nom du client :");
                nom = Console.ReadLine();
                try
                {
                    cli.NomClient = nom;
                    test = true;
                }
                catch (NotAstring ex)
                {
                    Console.WriteLine(ex.Message);
                    test = false;
                }
            } while (test == false);

            Console.WriteLine("Prénom du client :");
            prenom = Console.ReadLine();
            try
            {
                cli.PrenomClient = prenom;
                test = true;
            }
            catch (NotAstring ex)
            {
                Console.WriteLine(ex.Message);
                test = false;


            } while (test == false) ;

            //cli = listDesClients.Find(nom);
            //    if (cli.NomClient != null)
            bool trouv = cli.Find(nom, prenom);// recherche si le nom et prénom existe
            if (trouv)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\n\n\nCe client est déjà enregisté dans notre base de donnée...");
                Console.ReadLine();
                Console.ResetColor();

            }
            //    {

            //        Console.ForegroundColor = ConsoleColor.DarkBlue;
            //        Console.WriteLine("\n\n\nCe client est déjà enregisté dans notre base de donnée...");
            //        Console.ReadLine();
            //        Console.ResetColor();
            //    }
            else
            {
                do
                {
                    Console.WriteLine("Numéro de téléphone :");
                    tel = Console.ReadLine();
                    try
                    {
                        cli.Tel = tel;
                        test = true;
                    }
                    catch (NotAstring ex)
                    {
                        Console.WriteLine(ex.Message);
                        test = false;
                    }
                } while (test == false);

                cli.Add();
                if (cli.NomClient == null)
                {
                    Console.WriteLine("erreur");
                }
            }
            return cli;
        }

        //--------------------***************

        //-------------------- Suppression client de la base------------------
        static void DelClient()
        {
            Console.WriteLine("nom du client à supprimer :");
            string nom = Console.ReadLine();
            if (listDesClients.DelCl(nom) != true)
            {
                Console.WriteLine("erreur");
            }
            else
            {
                Console.ReadLine();
                AffichCl();
            }
        }

        static void AffichCl()
        {
            listDesClients.AffichListeCl();
            Console.ReadLine();
        }

        static void AddReservation()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--Création d'une réservation------");
            Console.ResetColor();
            Console.Write("Nom du client : ");
            string nom = Console.ReadLine();
            Client cli = new Client();
            cli = listDesClients.Find(nom);
            if (cli.NomClient == null)
            {
                Console.Write("nous allons vous diriger vers la fenêtre de saisie de client\n");
                cli = AddCl();
            }
            Console.WriteLine("Date de réservation ?");
            string DateReser = Console.ReadLine();
            listDesReservations.AddReserv(cli, DateReser);
            Console.ReadLine();
        }

        //*********************------------------------------------***********************

        static void AffichChangement(string s)
        {
            // attention vous allez modifier le statut 
            // + change le bool quit si refus changement
            if (s != "")
            {
                string stat;
                stat = (s == "rec") ? "ovr" : "rec";
                Console.WriteLine("Vous allez passer la réservation de " + s + " à " + stat + "?\n o/n");
                string c = Console.ReadLine();
                if (c != "o")
                {
                    Console.WriteLine("Action non conforme !");
                    listDesReservations.NonConform = true;
                }
            }
        }

        static void ErreurChargement(string s)
        {

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Réservation inexistante !!!\n");
            Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ResetColor();

        }

        static void ChangReservation()
        {

            Console.Clear();
            Console.Write("Code réservation : ");
            string Code = Console.ReadLine();
            if (listDesReservations.Find_(Code) == null)
            {
                listDesReservations.ChangReserv += ErreurChargement;
                listDesReservations.ReservNoFind();
                listDesReservations.ChangReserv -= ErreurChargement;
            }
            else

            {
                listDesReservations.ChangReserv += AffichChangement;
                listDesReservations.ChgReserv(Code);// réservation trouvée
                listDesReservations.ChangReserv -= AffichChangement;
            }


            {

            }
        }

        static void AffichReservation()
        {
            listDesReservations.AffichListReserv();

        }
    }
}
