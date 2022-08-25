using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoStore4.Models;
using System.Data.Entity.Migrations;

namespace DemoStore4.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminVoucherController : Controller
    {
        DemoStoreContext context = new DemoStoreContext();
        // GET: AdminVoucher
        public ActionResult Index()
        {
            var ds = context.Vouchers.ToList();
            return View(ds);
        }

        // GET: AdminVoucher/Details/5
        public ActionResult Details(int id)
        {
            var v = context.Vouchers.SingleOrDefault(x => x.MaKM == id);
            return View(v);
        }

        // GET: AdminVoucher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminVoucher/Create
        [HttpPost]
        public ActionResult Create(Voucher v)
        {
            try
            {
                ModelState.Remove("MaKM");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                context.Vouchers.Add(v);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminVoucher/Edit/5
        public ActionResult Edit(int id)
        {
            var v = context.Vouchers.SingleOrDefault(x => x.MaKM == id);
            return View(v);
        }

        // POST: AdminVoucher/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Voucher v)
        {
            try
            {
                var vupdate = context.Vouchers.FirstOrDefault(x => x.MaKM == id);
                vupdate = v;
                context.Vouchers.AddOrUpdate(vupdate);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminVoucher/Delete/5
        public ActionResult Delete(int id)
        {
            var vcdelete = context.Vouchers.FirstOrDefault(x => x.MaKM == id);
            var ds = context.CT_Voucher.Where(x => x.MaKM == id).ToList();
            foreach (var item in ds)
            {
                context.CT_Voucher.Remove(item);
                context.SaveChanges();
            }
            context.Vouchers.Remove(vcdelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
