using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoStore4.Models;
using System.IO;
using System.Data.Entity.Migrations;

namespace DemoStore4.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminSPController : Controller
    {
        // GET: AdminSP
        DemoStoreContext context = new DemoStoreContext();

        public ActionResult Index()
        {
            var dssp = context.SanPhams.ToList();
            return View(dssp);
        }

        // GET: AdminSP/Details/5

        public ActionResult Details(int id)
        {
            var sp = context.SanPhams.SingleOrDefault(x => x.MaSP == id);
            return View(sp);
        }

        // GET: AdminSP/Create

        public ActionResult Create()
        {
            var lsp = new SanPham();
            lsp.ListLoaiSP = context.LoaiSPs.ToList();
            return View(lsp);
        }

        // POST: AdminSP/Create
        [HttpPost]

        public ActionResult Create(SanPham sp)
        {
            HttpPostedFileBase file = Request.Files["HinhAnh"];
            if (file != null || file.ContentLength > 0)
            {
                ModelState.Remove("MaSP");
                if (!ModelState.IsValid)
                {
                    sp.ListLoaiSP = context.LoaiSPs.ToList();
                    return View("Create", sp);
                }
                string _FileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("/Hinh/"), _FileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    file.SaveAs(path);
                }
                else
                {
                    file.SaveAs(path);
                }
                sp.HinhAnh = _FileName;
                context.SanPhams.Add(sp);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: AdminSP/Edit/5

        public ActionResult Edit(int id)
        {
            var sp = context.SanPhams.SingleOrDefault(x => x.MaSP == id);
            sp.ListLoaiSP = context.LoaiSPs.ToList();
            return View(sp);
        }

        // POST: AdminSP/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SanPham sp)
        {
            try
            {
                var spupdate = context.SanPhams.FirstOrDefault(x => x.MaSP == id);
                spupdate = sp;
                HttpPostedFileBase file = Request.Files["HinhAnh"];
                if (file != null || file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("/Hinh/"), _FileName);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        file.SaveAs(path);
                    }
                    else
                    {
                        file.SaveAs(path);
                    }
                    spupdate.HinhAnh = _FileName;
                    context.SanPhams.AddOrUpdate(spupdate);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            var spdelete = context.SanPhams.SingleOrDefault(x => x.MaSP == id);
            var ddh = context.CT_DDH.Where(x => x.MaSP == id).ToList();
            var gh = context.GioHangs.Where(x => x.MaSP == id).ToList();
            foreach (var item in gh)
            {
                context.GioHangs.Remove(item);
                context.SaveChanges();
            }
            foreach (var item in ddh)
            {
                context.CT_DDH.Remove(item);
                context.SaveChanges();
            }
            context.SanPhams.Remove(spdelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: AdminSP/Delete/5
    }
}
