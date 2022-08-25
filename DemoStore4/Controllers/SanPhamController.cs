using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoStore4.Models;
using DemoStore4.Models.SQLViews;
using Microsoft.AspNet.Identity;

namespace DemoStore4.Controllers
{
    public class SanPhamController : Controller
    {
        DemoStoreContext context = new DemoStoreContext();
        DemoStoreView ViewContext = new DemoStoreView();
        // GET: SanPham
        public ActionResult Index(string sortOrder)
        {   
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            var dssp = ViewContext.ViewSanPhams.ToList();
            switch (sortOrder)
            {
                case "name_desc":
                    dssp = dssp.OrderByDescending(s => s.TenSP).ToList();
                    break;
                case "Price":
                    dssp = dssp.OrderBy(s => s.DonGia).ToList();
                    break;
                case "Price_desc":
                    dssp = dssp.OrderByDescending(s => s.DonGia).ToList();
                    break;
                case "name":
                    dssp = dssp.OrderBy(s => s.TenSP).ToList();
                    break;
                default:
                    break;
            }
            var userID = User.Identity.GetUserId();
            foreach (var item in dssp)
            {
                if (userID!= null)
                {
                    item.DaDangNhap = true;
                    GioHang find = context.GioHangs.FirstOrDefault(x => x.MaSP == item.MaSP
                    && x.Id == userID);
                    if (find == null)
                    {
                        item.DaThemVao = true;
                    }
                }
            }
            return View(dssp);
        }

        public ActionResult SanPhamKM(string sortOrder)
        {
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            var dsspkm = ViewContext.KhuyenMais.ToList();
            switch (sortOrder)
            {
                case "name_desc":
                    dsspkm = dsspkm.OrderByDescending(s => s.TenSP).ToList();
                    break;
                case "Price":
                    dsspkm = dsspkm.OrderBy(s => s.DonGia).ToList();
                    break;
                case "Price_desc":
                    dsspkm = dsspkm.OrderByDescending(s => s.DonGia).ToList();
                    break;
                case "name":
                    dsspkm = dsspkm.OrderBy(s => s.TenSP).ToList();
                    break;
                default:
                    break;
            }
            var gh = context.GioHangs.ToList();
            var userID = User.Identity.GetUserId();
            foreach (var item in dsspkm)
            {
                if (userID != null)
                {
                    item.DaDangNhap = true;
                    GioHang find = context.GioHangs.FirstOrDefault(x => x.MaSP == item.MaSP
                    && x.Id == userID);
                    if (find == null)
                    {
                        item.DaThemVao = true;
                    }
                }
            }
            return View(dsspkm);
        }
    }
}