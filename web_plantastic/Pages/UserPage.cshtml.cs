using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace web_plantastic.Pages
{
    public class Eveniment
    {
        public int id;
        public string nume;
        public string descriere;
        public string tip;
        public DateTime inceput;
        public DateTime sfarsit;
        
        public Eveniment(int id, string nume, string descriere, string tip, DateTime inceput,DateTime sfarsit)
        {
            this.id = id;
            this.nume = nume;
            this.descriere = descriere;
            this.tip = tip;
            this.inceput = inceput;
            this.sfarsit = sfarsit;
        }
    }
    public class UserPageModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string User { get; set; }
        [BindProperty(SupportsGet = true)]
        public int evenimentId { get; set; }
        [BindProperty]
        public string Greeting { get; set; }
        [BindProperty]
        public List<Eveniment> evenimente { get; set; }
        public void OnGet()
        {

            Greeting = greetUser(User);
            evenimente = getAllEvenimente(User);
            
        }
        
        public object ToggleEveniment(Eveniment ev)
        {
            if (ev.tip == "unic")
                ev.tip = "terminat";
            else if (ev.tip == "terminat")
                ev.tip = "unic";
            return ev;
        }

        private string greetUser(string user)
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "mybd";
            if (dbCon.IsConnect())
            {
                dbCon.Connection.Open();
                string query = $"SELECT showNume, showPrenume FROM mydb.useri WHERE loginNume = '{user}';";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    string nume = reader.GetString(0);
                    string prenume = reader.GetString(1);
                    dbCon.Close();

                    return $"Buna {nume} {prenume}";
                }
                else
                {
                    dbCon.Close();
                    return "Database Connection Error";
                }
            }
            else
            {
                dbCon.Close();
                return "Database Connection Error";
            }
        }

        private List<Eveniment> getAllEvenimente(string user)
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "mybd";
            if (dbCon.IsConnect())
            {
                List<Eveniment> evenimenteleObtinute = new List<Eveniment>();
                dbCon.Connection.Open();
                string query = $"SELECT * FROM mydb.evenimentele_useri WHERE loginNume = '{user}';";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    int idEveniment = reader.GetInt16(0);
                    string numeEveniment = reader.GetString(1);
                    string tipEveniment = reader.GetString(2);
                    string descriereEveniment = reader.GetString(3);
                    DateTime inceputEveniment = reader.GetDateTime(4);
                    DateTime sfarsitEveniment = reader.GetDateTime(5);

                    Eveniment ev = new Eveniment(idEveniment, numeEveniment, descriereEveniment,
                        tipEveniment, inceputEveniment, sfarsitEveniment);
                    evenimenteleObtinute.Add(ev);
                }
                dbCon.Close();
                return evenimenteleObtinute;
            }
            else
            {
                dbCon.Close();
                return null;
            }
        }

        
    }
}