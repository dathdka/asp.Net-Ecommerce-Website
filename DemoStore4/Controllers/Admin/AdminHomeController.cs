using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoStore4.Models;
namespace DemoStore4.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeController : Controller
    {
        DemoStoreContext contex = new DemoStoreContext();
        // GET: AdminHome
        public ActionResult Index()
        {
            var dshd = contex.DonDHs.ToList(); 
            return View(dshd);
        }
    }
}