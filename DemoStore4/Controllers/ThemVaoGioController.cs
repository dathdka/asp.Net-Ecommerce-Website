using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoStore4.Models;
using DemoStore4.Models.SQLViews;
using Microsoft.AspNet.Identity;

namespace DemoStore4.Controllers
{
    [Authorize]
    public class ThemVaoGioController : ApiController
    {
        DemoStoreContext context = new DemoStoreContext();
        [HttpPost]
        public IHttpActionResult ThemVao(SanPham sp)
        {
            
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return BadRequest("Bạn cần đăng nhập để thực hiện");
            }
            if (context.GioHangs.Any(x=>x.Id == userId && x.MaSP == sp.MaSP))
            {
                context.GioHangs.Remove(context.GioHangs.SingleOrDefault(x => x.Id == userId
                && x.MaSP == sp.MaSP));
                context.SaveChanges();
                return Ok("cancel");
            }
            if (context.SanPhams.FirstOrDefault(x=>x.MaSP == sp.MaSP).SoLuong==0)
            {
                return BadRequest("Xin lỗi quý khách, mặt hàng này đã hết");
            }
            GioHang themMoi = new GioHang();
            themMoi.Id = userId;
            themMoi.MaSP = sp.MaSP;
            themMoi.SoLuongThem = 1;
            context.GioHangs.Add(themMoi);
            context.SaveChanges();
            return Ok();
        }
    }
}
