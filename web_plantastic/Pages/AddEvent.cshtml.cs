using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        [BindProperty(SupportsGet = true)]
        public bool parolaNepotrivita { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool alreadyExists { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool invalidInput { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool eroare { get; set; }

        public void OnGet()
        {

        }
    }
}