using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace web_plantastic.Pages
{
    public class AddEventModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string User { get; set; }
        [BindProperty]
        public string nume { get; set; }
        [BindProperty]
        public string descriere { get; set; }
        [BindProperty]
        public string tip { get; set; }

        [BindProperty]
        public DateTime dataEvenimentInceput { get; set; }
        [BindProperty]
        public DateTime dataEvenimentSfarsit { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool introducereData { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool invalidInput { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool eroare { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool succes { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (Request.Form["inapoi"] == "inapoi")
            {
                return RedirectToPage("/UserPage", new { User = User });
            }
            if (Request.Form["adauga"] == "adauga")
            {

                if (insertNewEvent(nume, tip, descriere, User))
                {
                    string dataInceput = dataEvenimentInceput.ToString("yyyy-MM-dd");
                    string oraInceput = dataEvenimentInceput.ToString("HH:mm:ss");
                    string dataSfarsit = dataEvenimentSfarsit.ToString("yyyy-MM-dd");
                    string oraSfarsit = dataEvenimentSfarsit.ToString("HH:mm:ss");
                    if (insertNewDate(dataInceput, dataSfarsit, oraInceput, oraSfarsit, nume, User)) 
                        return RedirectToPage("/AddEvent", new { succes = true, User = User });
                    else return RedirectToPage("/AddEvent", new { eroare = true, User = User });
                }
                else return RedirectToPage("/AddEvent", new{eroare = true,User = User});
            }
            return RedirectToPage("/AddEvent", new { User = User });
        }

        private bool insertNewEvent(string nume,string tip, string descriere, string user)
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "mybd";
            if (dbCon.IsConnect())
            {
                dbCon.Connection.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = dbCon.Connection;
                cmd.CommandText = "mydb.add_eveniment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tipEveniment", tip);
                cmd.Parameters["@tipEveniment"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@numeEveniment", nume);
                cmd.Parameters["@numeEveniment"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@descriereEveniment", descriere);
                cmd.Parameters["@descriereEveniment"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@numeLoginUser", user);
                cmd.Parameters["@numeLoginUser"].Direction = ParameterDirection.Input;

                cmd.ExecuteNonQuery();

                dbCon.Close();
                return true;
            }
            else
            {
                dbCon.Close();
                return false;
            }
        }
        private bool insertNewDate(string dataInceput, string dataSfarsit, string oraInceput, string oraSfarsit, string numeEveniment,string numeUser)
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "mybd";
            if (dbCon.IsConnect())
            {
                dbCon.Connection.Open();
                string query = $"select idEveniment from mydb.evenimente join mydb.useri on useri.idUser = evenimente.idUser where useri.loginNume = '{numeUser}' and evenimente.nume = '{numeEveniment}';";
                var cmd1 = new MySqlCommand(query,dbCon.Connection);

                var reader = cmd1.ExecuteReader();

                reader.Read();
                int idEveniment = reader.GetInt16(0);
                dbCon.Close();

                dbCon.Connection.Open();

                if (dbCon.IsConnect())
                {
                    var cmd2 = new MySqlCommand();
                    cmd2.Connection = dbCon.Connection;
                    cmd2.CommandText = "mydb.add_date";
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.AddWithValue("@data_inceput", dataInceput);
                    cmd2.Parameters["@data_inceput"].Direction = ParameterDirection.Input;

                    cmd2.Parameters.AddWithValue("@data_sfarsit", dataSfarsit);
                    cmd2.Parameters["@data_sfarsit"].Direction = ParameterDirection.Input;

                    cmd2.Parameters.AddWithValue("@ora_inceput", oraInceput);
                    cmd2.Parameters["@ora_inceput"].Direction = ParameterDirection.Input;

                    cmd2.Parameters.AddWithValue("@ora_sfarsit", oraSfarsit);
                    cmd2.Parameters["@ora_sfarsit"].Direction = ParameterDirection.Input;
                
                    cmd2.Parameters.AddWithValue("@id_eveniment", idEveniment);
                    cmd2.Parameters["@id_eveniment"].Direction = ParameterDirection.Input;

                    cmd2.ExecuteNonQuery();

                    dbCon.Close();
                    return true;
                }
                else
                {
                    dbCon.Close();
                    return false;
                }
            }
            else
            {
                dbCon.Close();
                return false;
            }
        }
    }
}