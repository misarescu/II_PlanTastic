using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace web_plantastic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string utilizator { get; set; }

        [BindProperty]
        public string parola { get; set; }
        [BindProperty]
        public string logInMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool LoginError { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(bool loginError)
        {

        }

        public IActionResult OnPost()
        {
            
            if (IsValidUser(utilizator, parola))
            {
                return RedirectToPage("/UserPage",new { User = utilizator});
            }
            else return RedirectToPage("/Index", new { LoginError = true });
        }

        public string helloRusia()
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "mybd";
            if (dbCon.IsConnect())
            {
                dbCon.Connection.Open();
                string query = "SELECT loginNume as user, cast(aes_decrypt(parola,'Mona Lisa') as char(45)) as parola FROM mydb.useri WHERE loginNume='Germania';";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {

                    string utilizator = reader.GetString(0);
                    string parola = reader.GetString(1);
                    dbCon.Close();
                    return $"Buna {utilizator}, parola ta este {parola}";
                }
                else
                {
                    dbCon.Close();
                    return "Userul sau parola gresita!";
                }

            }
            else {
                dbCon.Close();
                return "Eroare la conexiunea bazei de date!"; 
            }
        }

        private bool IsValidUser(string utilizator,string parola)
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "mybd";
            if (dbCon.IsConnect())
            {
                dbCon.Connection.Open();
                string query = $"SELECT loginNume as user, cast(aes_decrypt(parola,'Mona Lisa') as char(45)) as parola FROM mydb.useri WHERE loginNume='{utilizator}';";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    string recievedUser = reader.GetString(0);
                    string recievedPassword = reader.GetString(1);
                    dbCon.Close();
                    if (recievedPassword == parola)
                    {
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
            else
            {
                dbCon.Close();
                return false;
            }
        }
    }
}
