using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoStore4.Models;
using DemoStore4.Models.SQLViews;
using Microsoft.AspNet.Identity;
using System.Configuration;
using DemoStore4.Models.Payment;

namespace DemoStore4.Controllers
{
    public class HomeController : Controller
    {
        DemoStoreContext context = new DemoStoreContext();
        DemoStoreView view = new DemoStoreView();
        public ActionResult Index()
        {
            var dskm = view.KhuyenMaiHomes.ToList();
            return View(dskm);
        }

        public ActionResult TimKiem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TimKiem(FormCollection form)
        {
            var userID = User.Identity.GetUserId();
            string luachon;
            string tukhoa;
            int malsp;
            List<ViewSanPham> dstk = new List<ViewSanPham>();
            if (string.IsNullOrWhiteSpace(form["TuKhoa"])|| string.IsNullOrWhiteSpace(form["LuaChon"]))
            {
                dstk = view.ViewSanPhams.ToList();
            }
            else
            {
                luachon = form["LuaChon"];
                tukhoa = form["TuKhoa"];
                if (luachon.CompareTo("Sản phẩm")==0)
                {
                    dstk = view.ViewSanPhams.Where(x => x.TenSP.ToUpper().Contains(tukhoa.ToUpper())).ToList();
                }
                else
                {
                    foreach (var item in context.LoaiSPs)
                    {
                        if (item.TenLSP.ToLower().Contains(tukhoa.ToLower()))
                        {
                            malsp = item.MaLSP;
                            dstk = view.ViewSanPhams.Where(x => x.MaLSP == malsp).ToList();
                            break;
                        }
                    }
                }
            }
            foreach (var item in dstk)
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
            return View("KetQuaTimKiem", dstk);
        }

        public ActionResult KetQuaTimKiem(List<ViewSanPham> dstk, string sortOrder)
        {
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            switch (sortOrder)
            {
                case "name_desc":
                    dstk = dstk.OrderByDescending(s => s.TenSP).ToList();
                    break;
                case "Price":
                    dstk = dstk.OrderBy(s => s.DonGia).ToList();
                    break;
                case "Price_desc":
                    dstk = dstk.OrderByDescending(s => s.DonGia).ToList();
                    break;
                case "name":
                    dstk = dstk.OrderBy(s => s.TenSP).ToList();
                    break;
                default:
                    break;
            }
            return View(dstk);
        }
        [Authorize]
        public ActionResult GioHang()
        {
            var userID = User.Identity.GetUserId();
            var gh = context.GioHangs.Where(x => x.Id == userID).ToList();

            foreach (var item in gh)
            {
                item.TenSP = context.SanPhams.FirstOrDefault(x => x.MaSP == item.MaSP).TenSP;
                item.HinhAnh = context.SanPhams.FirstOrDefault(x => x.MaSP == item.MaSP).HinhAnh;
                item.DonGia = context.SanPhams.FirstOrDefault(x => x.MaSP == item.MaSP).DonGia;
                item.KhuyenMai = context.SanPhams.FirstOrDefault(x => x.MaSP == item.MaSP).KhuyenMai;
                item.GiaSauGiam = (item.DonGia * (double)item.SoLuongThem) - (item.DonGia * (double)item.SoLuongThem * item.KhuyenMai);
            }
            return View(gh);
        }


        [Authorize]
        public ActionResult Delete(string id, int id2)
        {
            var spdelete = context.GioHangs.FirstOrDefault(x => x.MaSP == id2 && x.Id == id);
            context.GioHangs.Remove(spdelete);
            context.SaveChanges();
            return RedirectToAction("GioHang");
        }

        [Authorize]
        public ActionResult XacNhanDonHang(double TongTien)
        {
            UserView user = new UserView();
            var userId = User.Identity.GetUserId();
            var nguoidung = user.NguoiDungs.FirstOrDefault(x => x.Id == userId);
            
            nguoidung.DaNhap = false;
            nguoidung.Diem = (int)user.NguoiDungs.FirstOrDefault(x => x.Id == userId).Points;
            if (nguoidung.Diem > 1000)
            {
                nguoidung.TongTien = TongTien - TongTien*0.05;
            }
            else
            {
                nguoidung.TongTien = TongTien;
            }
            return View(nguoidung);
        }
        [HttpPost]
        public ActionResult XacNhanDonHang ( FormCollection form, double TongTien)
        {
            int Voucher;
            UserView user = new UserView();
            var userId = User.Identity.GetUserId();
            var nd = user.NguoiDungs.FirstOrDefault(x => x.Id == userId);
            nd.Diem = (int)user.NguoiDungs.FirstOrDefault(x => x.Id == userId).Points;
            bool a = int.TryParse(form["voucher"], out Voucher);
            if (a)
            {
                var vc = context.Vouchers.FirstOrDefault(x => x.MaKM == Voucher && x.NgayKT > DateTime.Now);
                var vcnd = context.CT_Voucher.FirstOrDefault(x => x.IDUser.CompareTo(userId) == 0 && x.MaKM == Voucher);
                if (vcnd != null && vc !=null && !vcnd.DaSD)
                {
                    if (nd.Points>1000)
                    {
                        nd.TongTien = TongTien - TongTien * vc.GiaKM - TongTien*0.05;
                    }
                    else
                    {
                        nd.TongTien = TongTien - TongTien * 0.05;
                    }
                    
                    context.CT_Voucher.FirstOrDefault(x => x.IDUser.CompareTo(userId) == 0 && x.MaKM == Voucher).DaSD = true;
                    context.SaveChanges();
                    nd.DaNhap = true;
                }
                else
                {
                    nd.TongTien = TongTien - TongTien * 0.05;
                }
                
            }
            return View(nd);
        }

        public ActionResult Payment(double ThanhTien)
        {
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", (ThanhTien * 100).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }

        public ActionResult PaymentConfirm(double vnp_Amount)
        {


            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        var userId = User.Identity.GetUserId();
                        DonDH ddh = new DonDH();
                        ddh.IDUser = userId;
                        ddh.ThoiGianDat = DateTime.Now;
                        if (vnp_Amount/100 > 1000000)
                        {
                            var themVC = context.Vouchers
                                            .OrderByDescending(x => x.MaKM)
                                            .FirstOrDefault();
                            if (!context.CT_Voucher.Any(x=>x.MaKM == themVC.MaKM &&x.IDUser == userId))
                            {
                                CT_Voucher tangVC = new CT_Voucher();
                                tangVC.DaSD = false;
                                tangVC.IDUser = userId;
                                tangVC.MaKM = themVC.MaKM;
                                context.CT_Voucher.Add(tangVC);
                                context.SaveChanges();
                            }
                        }
                        ddh.TongTien = vnp_Amount / 100;
                        context.DonDHs.Add(ddh);
                        context.SaveChanges();
                        var dsh = context.GioHangs.Where(x => x.Id == userId).ToList();
                        foreach (var item in dsh)
                        {
                            context.SanPhams.FirstOrDefault(x => x.MaSP == item.MaSP).SoLuong -= (int)item.SoLuongThem;
                            CT_DDH n = new CT_DDH();
                            n.MaSP = item.MaSP;
                            var lastbuy = context.DonDHs.Where(x => x.IDUser == userId)
                                            .OrderByDescending(x =>x.MaDDH)
                                            .FirstOrDefault().MaDDH;
                            n.MaDDH = lastbuy;
                            n.ThoiGianNhan = null;
                            n.SLuong = (int)item.SoLuongThem;
                            context.CT_DDH.Add(n);
                            context.SaveChanges();
                            context.GioHangs.Remove(item);
                            context.SaveChanges();
                            UserContext user = new UserContext();
                            user.AspNetUsers.FirstOrDefault(x => x.Id == userId).Points += (int)(vnp_Amount / 100000);
                            user.SaveChanges();
                        }
                        //Thanh toán thành công
                        ViewBag.Message = "Thanh toán hóa đơn thành công";
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn ";
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return View();
        }
        public ActionResult LSanPham ()
        {
            var dslsp = context.LoaiSPs.ToList();
            return PartialView(dslsp);
        }

        public ActionResult LSanPhamKM()
        {
            var dslsp = context.LoaiSPs.ToList();
            return PartialView(dslsp);
        }

        public ActionResult SPTheoLoai(int id)
        {
            var dssp = view.ViewSanPhams.Where(x => x.MaLSP == id).ToList();
            return View("KetQuaTimKiem", dssp);
        }

        public ActionResult SPTheoLoaiKM(int id)
        {
            var dssp = view.ViewSanPhams.Where(x => x.MaLSP == id && x.KhuyenMai>0).ToList();
            return View("KetQuaTimKiem", dssp);
        }
    }
}