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
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string nume { get; set; }
        [BindProperty]
        public string prenume { get; set; }
        [BindProperty]
        public string mail { get; set; }
        [BindProperty]
        public string utilizator { get; set; }
        [BindProperty]
        public string parola1 { get; set; }
        [BindProperty]
        public string parola2 { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool parolaNepotrivita { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool alreadyExists { get; set; }
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

            if (Request.Form["register"] == "register")
            {
                if (inputOK(utilizator, nume, prenume, mail, parola1, parola2))
                {
                    if (isNewUser(utilizator, nume, prenume, mail, parola1))
                    {
                        if (samePassword(parola1, parola2))
                        {
                            if (insertNewUser(utilizator, nume, prenume, mail, parola1))
                                return RedirectToPage("/Register", new { succes = true });
                            else return RedirectToPage("/Register", new { eroare = true });
                        }
                        else return RedirectToPage("/Register", new { parolaNepotrivita = true });
                    }
                    else return RedirectToPage("/Register", new { alreadyExists = true });
                }
                else
                {
                    return RedirectToPage("/Register", new { invalidInput = true }); 
                }
            }
            if (Request.Form["inapoi"] == "inapoi")
            {
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Index");
        }

        private bool samePassword(string pass1, string pass2)
        {
            return pass1 == pass2;
        }

        private bool inputOK(string user,string nume, string prenume, string mail,string pass1, string pass2)
        {
            if (user == null)
                return false;
            if (nume == null)
                return false;
            if (prenume == null)
                return false;
            if (mail == null)
                return false;
            if (pass1 == null)
                return false;
            if (pass2 == null)
                return false;
            return true;
        }

        private bool isNewUser(string user, string nume, string prenume, string mail, string pass)
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "mybd";
            if (dbCon.IsConnect())
            {
                dbCon.Connection.Open();
                string query = $"SELECT loginNume FROM mydb.useri WHERE loginNume='{user}';";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                { 
                    dbCon.Close();
                    return false;
                }
                else
                {
                    dbCon.Close();
                    return true;
                }

            }
            else
            {
                dbCon.Close();
                return false;
            }
        }
        private bool insertNewUser(string user, string nume, string prenume, string mail, string pass)
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "mybd";
            if (dbCon.IsConnect())
            {
                dbCon.Connection.Open();
                string query = $"call mydb.add_user('{user}', '{pass}', '{mail}', '{nume}', '{prenume}');";
                var cmd = new MySqlCommand();
                cmd.Connection = dbCon.Connection;
                cmd.CommandText = "mydb.add_user";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@userLogin", user);
                cmd.Parameters["@userLogin"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters["@pass"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@email", mail);
                cmd.Parameters["@email"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@Nume", nume);
                cmd.Parameters["@Nume"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@Prenume", prenume);
                cmd.Parameters["@Prenume"].Direction = ParameterDirection.Input;

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
    }
}