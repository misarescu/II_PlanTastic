using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_plantastic.Pages
{
    public class UserPageModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string User { get; set; }
        public void OnGet()
        {

        }
    }
}