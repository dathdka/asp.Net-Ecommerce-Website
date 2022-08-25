using DemoStore4.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoStore4.Controllers
{
    public class ThemSLController : ApiController
    {
        DemoStoreContext context = new DemoStoreContext();
        [HttpPost]
        public IHttpActionResult TangSL(GioHang gh)
        {
            var userId = User.Identity.GetUserId();
            var sp = context.SanPhams.FirstOrDefault(x => x.MaSP == gh.MaSP);
            var spct = context.GioHangs.FirstOrDefault(x => x.MaSP == gh.MaSP && x.Id == userId);
            if (sp != null)
            {
                if (spct.SoLuongThem < sp.SoLuong)
                {
                    context.GioHangs.FirstOrDefault(x => x.MaSP == gh.MaSP && x.Id == userId).SoLuongThem += 1;
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Số lượng sản phẩm trong kho không đủ");
                }
            }
            else
            {
                return BadRequest("Sản phẩm không tồn tại");
            }
        }
    }
}
