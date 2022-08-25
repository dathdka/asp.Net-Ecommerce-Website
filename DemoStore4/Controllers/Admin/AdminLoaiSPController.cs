using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoStore4.Models;

namespace DemoStore4.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminLoaiSPController : Controller
    {
        DemoStoreContext context = new DemoStoreContext();
        // GET: LoaiSPAdmin

        public ActionResult Index()
        {
            var dslsp = context.LoaiSPs.ToList();
            return View(dslsp);
        }
        // GET: LoaiSPAdmin/Details/5
        public ActionResult Details(int id)
        {
            var lsp = context.LoaiSPs.SingleOrDefault(x => x.MaLSP == id);
            return View(lsp);
        }

        // GET: LoaiSPAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiSPAdmin/Create

        [HttpPost]
        public ActionResult Create(LoaiSP sp)
        {
            try
            {
                ModelState.Remove("MaLSP");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                context.LoaiSPs.Add(sp);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LoaiSPAdmin/Edit/5

        public ActionResult Edit(int id)
        {
            var lsp = context.LoaiSPs.SingleOrDefault(x => x.MaLSP == id);
            return View(lsp);
        }

        // POST: LoaiSPAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, LoaiSP sp)
        {
            try
            {
                ModelState.Remove("MaLSP");
                context.LoaiSPs.FirstOrDefault(x => x.MaLSP == id).TenLSP = sp.TenLSP;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LoaiSPAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            var lsp = context.LoaiSPs.SingleOrDefault(x => x.MaLSP == id);
            context.LoaiSPs.Remove(lsp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
