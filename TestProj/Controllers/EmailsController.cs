using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TestProj.Models;

namespace TestProj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailsController : Controller
    {
        private AppDbContext AppDbContext;
        public EmailsController(AppDbContext context)
        {
            AppDbContext = context;

        }

        [HttpPost]
        public IActionResult Index(EmailModel email)
        {

            foreach(string s in email.Attributes)
            {
                //check for duplicates
                if (!AppDbContext.Emails.Where(x=>x.Email == email.Email && x.Date==DateTime.Today && x.Attribute == s).Any())
                {
                    EmailModel e = new()
                    {
                        Date = DateTime.Today,
                        Attribute = s,
                        Email = email.Email,
                        Key = email.Key
                    };
                    AppDbContext.Emails.Add(e);
                    AppDbContext.SaveChanges();

                }
            }
            return Json(email);
        }
    }
}
