using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace hotel.classes
{
      public class Client
{
    private int idClient;// passé de string à int pour coller avec la bd
    private string nomClient;
    private string prenomClient;
    private string tel;
    private Regex rgx;

    public Client(string n, string pn, string tl)
    {//Pour créer une instance
        NomClient = n;
        prenomClient = pn;
        tel = tl;
        
    }

    public Client()
    {

    }

    public int IdClient { get => idClient; set => idClient = value; }
    public string NomClient
    {
       get => nomClient;
       set
        {
                rgx = new Regex(@"^[a-zA-Z]+$");
                if (rgx.IsMatch(value))
                {
                    nomClient = value;
                }
                else throw new NotAstring();
            }
        }
        public string PrenomClient
        { get => prenomClient;
            set
            {
                rgx = new Regex(@"^[a-zA-Z]+$");
                if (rgx.IsMatch(value))                {
                    prenomClient = value;
                }
                else throw new NotAstring();
            }
        }
        public string Tel
        { get => tel;
          set
            {
                rgx = new Regex(@"^0[1-9]{1}(([0-9]{2}){4})|((\s[0-9]{2}){4})|((-[0-9]{2}){4})$");
                if (rgx.IsMatch(value))
                {
                    tel = value;
                }
                else throw new NotAstring();
            }
        }

        public Client(int id)
        {   // récupere les données client avec l'id
            SqlCommand command = new SqlCommand("SELECT * FROM Client  WHERE IDCl = @n", Connexion.Instance);
            command.Parameters.Add(new SqlParameter("@n", id));
            Connexion.Instance.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                IdClient = reader.GetInt32(0);                
                NomClient = reader.GetString(1);
                PrenomClient = reader.GetString(2);
                Tel = reader.GetString(3);
            }
            reader.Close();
            command.Dispose();
            Connexion.Instance.Close();
        }

        public void AffichCli()
        {
            Console.Clear();
            Console.WriteLine("*********-------------Affichage de la liste des clients-----------------***********\n\n");
            SqlCommand command = new SqlCommand("SELECT * FROM Client", Connexion.Instance);
            Connexion.Instance.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Numero client :" + reader.GetInt32(0) + "\tNom : " + reader.GetString(1)+ "Prénom : " + reader.GetString(2)+ "Télephone : " + reader.GetString(3));
                //Console.WriteLine();
            }
            reader.Close();
            command.Dispose();
            Connexion.Instance.Close();
        }

        public bool Find(string n,string p)
        {
            bool res = false;
            SqlCommand command = new SqlCommand("SELECT * FROM Client  WHERE NOM = @n AND PRENOM = @p "  , Connexion.Instance);
            command.Parameters.Add(new SqlParameter("@n", n));
            command.Parameters.Add(new SqlParameter("@p", p));
            Connexion.Instance.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) { res = true; }
            reader.Close();
            command.Dispose();
            Connexion.Instance.Close();
            return res;
        }
        public void Add()
        {
            SqlCommand command = new SqlCommand("INSERT INTO Client (Nom, Prenom, Tel) OUTPUT INSERTED.IDCL VALUES(@n,@p,@t)", Connexion.Instance);
            command.Parameters.Add(new SqlParameter("@n", NomClient));
            command.Parameters.Add(new SqlParameter("@p", PrenomClient));
            command.Parameters.Add(new SqlParameter("@t", Tel));
            Connexion.Instance.Open();
            IdClient = (int)command.ExecuteScalar();
            command.Dispose();
            Connexion.Instance.Close();
        }

        public bool DelCl(string nomCl,string prenomCl)//supprimer avec le nom
        {   
             if (Find(nomCl,prenomCl))//si on trouve le client
            {

                SqlCommand command = new SqlCommand("DELETE FROM Client WHERE Nom = @n and Prenom=@p", Connexion.Instance);
                command.Parameters.Add(new SqlParameter("@n", nomCl));
                command.Parameters.Add(new SqlParameter("@p", prenomCl));
                Connexion.Instance.Open();
                command.ExecuteNonQuery();
                command.Dispose();
                Connexion.Instance.Close();
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

    }
}


