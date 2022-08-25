using DemoStore4.Models;
using DemoStore4.Models.SQLViews;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoStore4.Controllers
{
    public class ThongTinController : Controller
    {
        DemoStoreContext context = new DemoStoreContext();
        // GET: ThongTin
        [Authorize]
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            UserView user = new UserView();
            var nd = user.NguoiDungs.FirstOrDefault(x => x.Id == userID);
            return View(nd);
        }


        public ActionResult DonDatHang()
        {
            var userID = User.Identity.GetUserId();
            var dsdh = context.DonDHs.Where(x => x.IDUser == userID).ToList();
            return View(dsdh);
        }

        public ActionResult ThongTinDonHang (int id)
        {
            var userID = User.Identity.GetUserId();
            List<CT_DDH> ctdh = new List<CT_DDH>();
            var check = context.DonDHs.FirstOrDefault(x => x.MaDDH == id);
            if (check.IDUser == userID)
            {
                ctdh = context.CT_DDH.Where(x => x.MaDDH == id).ToList();
                foreach (var item in ctdh)
                {
                    item.TenSP = context.SanPhams.FirstOrDefault(x => x.MaSP == item.MaSP).TenSP;
                    item.HinhAnh = context.SanPhams.FirstOrDefault(x => x.MaSP == item.MaSP).HinhAnh;
                }
                return View(ctdh);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult SuaThongTin ()
        {
            UserContext contex = new UserContext();
            var userID = User.Identity.GetUserId();
            var user = contex.AspNetUsers.FirstOrDefault(x => x.Id == userID);
            return View(user);
        }

        [HttpPost]
        public ActionResult SuaThongTin(FormCollection form)
        {
            UserContext contex = new UserContext();
            var userID = User.Identity.GetUserId();
            var updateuser = contex.AspNetUsers.FirstOrDefault(x => x.Id == userID);
            updateuser.Email = form["Email"];
            updateuser.PhoneNumber = form["PhoneNumber"];
            updateuser.Name = form["Name"];
            updateuser.Address = form["Address"];
            contex.AspNetUsers.AddOrUpdate(updateuser);
            contex.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Voucher ()
        {
            var userID = User.Identity.GetUserId();
            var dsvc = context.CT_Voucher.Where(x => x.IDUser == userID).ToList();
            List<CT_Voucher> ds = new List<CT_Voucher>();
            foreach (var item in dsvc)
            {
                if (context.Vouchers.FirstOrDefault(x=>x.MaKM == item.MaKM).NgayKT> DateTime.Now && !item.DaSD)
                {
                    item.MoTa = "Voucher giảm" + context.Vouchers.FirstOrDefault(x => x.MaKM == item.MaKM).GiaKM * 100 + "%";
                    ds.Add(item);
                }

            }
            return View(ds);
        }
    }
}