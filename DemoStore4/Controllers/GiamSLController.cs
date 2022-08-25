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
    public class GiamSLController : ApiController
    {
        DemoStoreContext context = new DemoStoreContext();
        [HttpPost]
        public IHttpActionResult GiamSL(GioHang gh)
        {
            var userId = User.Identity.GetUserId();
            var sp = context.SanPhams.FirstOrDefault(x => x.MaSP == gh.MaSP);
            var spct = context.GioHangs.FirstOrDefault(x => x.MaSP == gh.MaSP && x.Id == userId);
            if (sp != null)
            {
                if (spct.SoLuongThem >1)
                {
                    context.GioHangs.FirstOrDefault(x => x.MaSP == gh.MaSP && x.Id == userId).SoLuongThem -= 1;
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Số lượng Không được nhỏ hơn 1");
                }
            }
            else
            {
                return BadRequest("Sản phẩm không tồn tại");
            }
        }
    }
}
