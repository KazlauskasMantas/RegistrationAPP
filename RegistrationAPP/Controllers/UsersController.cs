using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RegistrationAPP.Models;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Web.UI;
using System.Web.Services;

namespace RegistrationAPP.Controllers
{
    public class UsersController : Controller
    {
        private UserEntity db = new UserEntity();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        public ActionResult SignUp()
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Email,Name,Password,Bdate,AddInfo")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    TempData["successmessage"] = "Registration successfull";
                    return RedirectToAction("searchDB");
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.InnerException.InnerException as SqlException;
                    if (innerException != null)
                    {
                        ModelState.AddModelError("", "Email address is already registered");
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(user);

            }
            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //SEARCH
        public ActionResult searchDB(string searchName, string searchType)
        {
            var results = from s in db.Users
                          select s;
            if (!String.IsNullOrEmpty(searchName))
            {
                switch (searchType)
                {
                    case "Email":
                        results = results.Where(c => c.Email.Contains(searchName));
                        break;
                    case "Name":
                        results = results.Where(c => c.Name.Contains(searchName));
                        break;
                }
            }
            return View(results);
        }
    }
}
