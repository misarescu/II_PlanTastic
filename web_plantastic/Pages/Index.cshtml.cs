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

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public string helloRusia()
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "mybd";
            if (dbCon.IsConnect())
            {
                string query = "SELECT loginNume as user, cast(aes_decrypt(parola,'Mona Lisa') as char(45)) as parola FROM mydb.useri WHERE loginNume='ermania';";
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
            else return "Eroare la conexiunea bazei de date!";
        }
    }
}
