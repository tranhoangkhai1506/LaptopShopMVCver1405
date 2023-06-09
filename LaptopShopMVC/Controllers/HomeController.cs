﻿using LaptopShopMVC.App_Start;
using LaptopShopMVC.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaptopShopMVC.Extras;
using System.Runtime.ConstrainedExecution;
using System.Data.Entity.Migrations;
using LaptopShopMVC.Email;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Contexts;
using System.Web.Services.Description;

namespace LaptopShopMVC.Controllers
{
    public class HomeController : Controller
    {
        LaptopDBContext context = new LaptopDBContext();

        public ActionResult Index()
        {
            var listSanPham = context.SANPHAMs.ToList();
            return View(listSanPham);
        }


        [HttpGet]
        public ActionResult Searching()
        {
            return View();
        }
        public ActionResult SearchingResults(string laptopName, string nameBrand, double? minPrice, double? maxPrice, string ramValue)
        {

            // Apply the filter to the list of laptops
            List<SANPHAM> filteredLaptops = context.SANPHAMs.ToList();

            if (laptopName != null)
            {
                filteredLaptops = filteredLaptops.Where(laptop => laptop.TENSANPHAM.IndexOf(laptopName, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
            }

            if (ramValue != "" && ramValue != "Select RAM Value")
            {
                filteredLaptops = filteredLaptops.Where(laptop => laptop.RAM == ramValue).ToList();
            }

            if (minPrice != null && maxPrice != null)
            {
                filteredLaptops = filteredLaptops.Where(p => p.GIABAN >= Convert.ToDecimal(minPrice) & p.GIABAN <= Convert.ToDecimal(maxPrice)).ToList();
            }

            if (nameBrand != "Select Brand" && nameBrand != null)
            {
                filteredLaptops = filteredLaptops.Where(laptop => laptop.THUONGHIEU.TENTHUONGHIEU.Contains(nameBrand)).ToList();
            }

            if (filteredLaptops.Count() != 0)
            {
                return View(filteredLaptops);
            }
            else
            {
                return RedirectToAction("errorResult", new { message = "We're sorry, we did not find any products. Please enter the information you are looking for. Thank You!" });
            }
        }

        public ActionResult viewAllProductOfBrand(string tenThuongHieu)
        {
            var listSanPham = context.SANPHAMs.Where(p => p.THUONGHIEU.TENTHUONGHIEU.Contains(tenThuongHieu)).ToList();
            if (listSanPham.Count() != 0)
            {
                return View(listSanPham);
            }
            return RedirectToAction("errorResult",new { message  = "We're sorry, we did not find any products. Please enter the information you are looking for. Thank You!" });
        }

        public ActionResult viewDetail(int maSanPham)
        {
            var sanPham = context.SANPHAMs.FirstOrDefault(p => p.MASANPHAM == maSanPham);
            if (sanPham != null)
            {
                return View(sanPham);
            }
            return RedirectToAction("errorResult", new { message = "We're sorry, we did not find any products. Please enter the information you are looking for. Thank You!" });
        }

        public ActionResult errorResult(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public ActionResult Details(int id)
        {
            var sanPham = context.SANPHAMs.FirstOrDefault(p => p.MASANPHAM == id);
            if (sanPham != null)  
            {
                ViewBag.giaBan = sanPham.GIABAN;
                return View(sanPham);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public JsonResult addToCart(int id)
        {
            if (Session["cart"] != null)
            {
                List<cart> mainlist = (List<cart>)Session["cart"];
                cart productInCart = mainlist.FirstOrDefault(p => p.maSanPham == id);
                
                if(productInCart != null)
                {
                    productInCart.soLuong += 1;
                }
                else 
                {
                    cart obj = new cart();
                    obj.maSanPham = id;
                    obj.soLuong = 1;
                    mainlist.Add(obj);
                }
                Session["cart"] = (List<cart>) mainlist;
            }
            else
            {
                List<cart> firstlist = new List<cart>();
                cart obj = new cart();
                obj.maSanPham = id;
                obj.soLuong = 1;
                firstlist.Add(obj);
                Session["cart"] = firstlist;
            }
            
            return Json(Session["cart"], JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewCart()
        {
            List<cart> mainlist = (List<cart>)Session["cart"];
            List<ViewCart> viewlist = new List<ViewCart>();

            if (mainlist != null)
            {
                foreach (var item in mainlist)
                {
                    ViewCart obj = new ViewCart();
                    SANPHAM sanPham = context.SANPHAMs.Where(p => p.MASANPHAM == item.maSanPham).FirstOrDefault();
                    obj.MASANPHAM = sanPham.MASANPHAM;
                    obj.TENSANPHAM = sanPham.TENSANPHAM;
                    obj.SOLUONG = item.soLuong;
                    obj.HINH = sanPham.HINH;
                    obj.GIABAN = sanPham.GIABAN;
                    obj.TONGTIEN = Convert.ToString(item.soLuong * Convert.ToDouble(sanPham.GIABAN));

                    viewlist.Add(obj);
                }
                return View(viewlist);
            }
            else
            {
                return RedirectToAction("errorResult", new { message = "We're sorry, Your cart is empty. Thank You!" });
            }
        }

        public ActionResult themSoLuong_cart(int id)
        {
            List<cart> mainlist = (List<cart>)Session["cart"];
            int soLuong = 0;
            for (int i = 0; i < mainlist.Count; i++)
            {
                if (mainlist[i].maSanPham == id)
                {
                    mainlist[i].soLuong = mainlist[i].soLuong + 1;
                    soLuong = mainlist[i].soLuong;
                    break;
                }
            }
            Session["cart"] = (List<cart>)mainlist;

            return Json(soLuong, JsonRequestBehavior.AllowGet);
        }

        public ActionResult giamSoLuong_cart(int id)
        {
            List<cart> mainlist = (List<cart>)Session["cart"];
            int soLuong = 0;
            for (int i = 0; i < mainlist.Count; i++)
            {
                if (mainlist[i].maSanPham == id)
                {
                    if (mainlist[i].soLuong > 1)
                    {
                        mainlist[i].soLuong = mainlist[i].soLuong - 1;
                        soLuong = mainlist[i].soLuong;
                        break;
                    }
                    else
                    {
                        soLuong = 1;
                        break;
                    }
                }

            }
            Session["cart"] = (List<cart>)mainlist;

            return Json(soLuong, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveToCart(int id)
        {
            List<cart> mainlist = (List<cart>)Session["cart"];

            if (mainlist.Count() != 0)
            {
                cart cartItemToRemove = mainlist.FirstOrDefault(item => item.maSanPham == id);
                if (cartItemToRemove != null)
                {
                    mainlist.RemoveAll(item => item.maSanPham == id);
                } 
            }

            Session["cart"] = (List<cart>)mainlist;

            return Json(mainlist, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        public ActionResult checkOut()
        {
            string tenDangNhap = User.Identity.Name;
            KHACHHANG khachHang;
            if (tenDangNhap != null)
            {
                TAIKHOANKHACHHANG taiKhoanKH = context.TAIKHOANKHACHHANGs.FirstOrDefault(p => p.TENDANGNHAP.Contains(tenDangNhap));
                if (taiKhoanKH != null)
                {
                    khachHang = taiKhoanKH.KHACHHANG;
                    return this.View(khachHang);
                }
                else
                {
                    return RedirectToAction("errorResult", new
                    {
                        message = "Bạn cần đăng nhập bằng tài khoản khách hàng để mua hàng!"
                    });
                }
            }
            return RedirectToAction("errorResult", new
            {
                message = "Bạn cần đăng nhập bằng tài khoản khách hàng để mua hàng!"
            });

        }
        public List<ViewCart> sessionToCart()
        {
            List<cart> cartSession = (List<cart>)Session["cart"];
            List<ViewCart> listCart = new List<ViewCart>();

            if (cartSession != null)
            {
                foreach(var cart in cartSession)
                {
                    ViewCart obj = new ViewCart();
                    SANPHAM sanPham = context.SANPHAMs.FirstOrDefault(p => p.MASANPHAM == cart.maSanPham);
                    obj.MASANPHAM = sanPham.MASANPHAM;
                    obj.TENSANPHAM = sanPham.TENSANPHAM;
                    obj.HINH = sanPham.HINH;
                    obj.GIABAN = sanPham.GIABAN;
                    obj.SOLUONG = cart.soLuong;
                    obj.TONGTIEN = Convert.ToString(cart.soLuong * Convert.ToDouble(sanPham.GIABAN));

                    listCart.Add(obj);
                }
            }
            return listCart;

        }

        public void creataPhieuBaoHanh(int maSanPham)
        {
            TAIKHOANKHACHHANG taiKhoanKH = context.TAIKHOANKHACHHANGs.FirstOrDefault(p => p.TENDANGNHAP.Contains(User.Identity.Name));
            SANPHAM sanPham = context.SANPHAMs.FirstOrDefault(p => p.MASANPHAM == maSanPham);

            PHIEUBAOHANH newPBH = new PHIEUBAOHANH();

            newPBH.MAKHACHHANG = taiKhoanKH.KHACHHANG.MAKHACHHANG;
            newPBH.MASANPHAM = sanPham.MASANPHAM;
            newPBH.NGAYBATDAU = DateTime.Now;
            DateTime ngayHienTai = DateTime.Now;
            int soThangCach = Convert.ToInt32(sanPham.THOIGIANBAOHANH); // Số tháng cần cách

            DateTime ngayCach = ngayHienTai.AddMonths(soThangCach);
            newPBH.NGAYKETTHUC = ngayCach;

            context.PHIEUBAOHANHs.AddOrUpdate(newPBH);
            context.SaveChanges();

        }

        public void creataCTDH(int maDonHang)
        {
            List<ViewCart> listGioHang = sessionToCart();
            TAIKHOANKHACHHANG taiKhoanKH = context.TAIKHOANKHACHHANGs.FirstOrDefault(p => p.TENDANGNHAP.Contains(User.Identity.Name));

            var maDonHangtoChiTiet = context.DONHANGs.FirstOrDefault(p => p.MADONHANG == maDonHang);
            if (maDonHangtoChiTiet != null)
            {
                foreach (var item in listGioHang)
                {
                    CHITIETDONHANG newChiTietDonHang = new CHITIETDONHANG();
                    newChiTietDonHang.MADONHANG = maDonHangtoChiTiet.MADONHANG;
                    newChiTietDonHang.MASANPHAM = item.MASANPHAM;
                    newChiTietDonHang.SOLUONG = Convert.ToInt16(item.SOLUONG);
                    newChiTietDonHang.GIA = Convert.ToDecimal(item.TONGTIEN);
                    newChiTietDonHang.NGAYLAP = DateTime.Now;
                    context.CHITIETDONHANGs.Add(newChiTietDonHang);
                    context.SaveChanges();

                    creataPhieuBaoHanh(newChiTietDonHang.MASANPHAM);

                }
                decimal TongTien = listGioHang
                        .GroupBy(p => p.MASANPHAM) // Group the items by MASANPHAM
                        .OrderBy(p => p.Key) // Order the groups by MASANPHAM
                        .Sum(p => p.Sum(x => decimal.Parse(x.TONGTIEN))); // Calculate the sum of TongTien for each group

                maDonHangtoChiTiet.TONGTIEN = TongTien;
                context.SaveChanges();
            }
        }
        public ActionResult errorPayment()
        {
            TAIKHOANKHACHHANG taiKhoanKH = context.TAIKHOANKHACHHANGs.FirstOrDefault(p => p.TENDANGNHAP.Contains(User.Identity.Name));
            DONHANG donhang_KH = context.DONHANGs.Where(m => m.MAKHACHHANG == taiKhoanKH.KHACHHANG.MAKHACHHANG).ToArray().Last();
            var listCTDH = context.CHITIETDONHANGs.Where(p => p.MADONHANG == donhang_KH.MADONHANG).ToList();
            context.CHITIETDONHANGs.RemoveRange(listCTDH);
            context.DONHANGs.Remove(donhang_KH);
            context.SaveChanges();
            return View();
        }

        public void sendingEMail()
        {
            TAIKHOANKHACHHANG taiKhoanKH = context.TAIKHOANKHACHHANGs.FirstOrDefault(p => p.TENDANGNHAP.Contains(User.Identity.Name));

            DONHANG donhang_KH = context.DONHANGs.Where(m => m.MAKHACHHANG == taiKhoanKH.KHACHHANG.MAKHACHHANG).ToArray().Last();
            EmailViewModels emailVm = new EmailViewModels();
            decimal tongTien = Convert.ToDecimal(donhang_KH.TONGTIEN);

            emailVm.EmailBody = @"<h2>Hello " + taiKhoanKH.KHACHHANG.TENKHACHHANG + "! </h2> <br />" +
                            "<h3>THÔNG TIN ĐƠN HÀNG</h3>" +
                            "Mã Đơn Hàng: " + donhang_KH.MADONHANG + "<br/>" +
                            "Ngày Thanh Toán: " + donhang_KH.NGAYTHANHTOAN + "<br/>" +
                            "Tổng Tiền: " + tongTien.ToString("#.####").Replace(",", "") + "<br/>" +
                            "Ngày gửi: " + DateTime.Now.ToString() + "<br/>" +
                            "<br/>Thanks for shopping with FRICA!";

            emailVm.SenderEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"];
            emailVm.SenderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"];
            emailVm.SmtpHostServer = System.Configuration.ConfigurationManager.AppSettings["smtpHostServer"];
            emailVm.SmtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
            emailVm.ReceiverEmailAddress = taiKhoanKH.KHACHHANG.EMAIL;
            emailVm.EmailSubject = "FRICA - COMFIRM";
            try
            {
                var client = new SmtpClient(emailVm.SmtpHostServer, emailVm.SmtpPort)
                {
                    EnableSsl = true,
                    Timeout = 100000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailVm.SenderEmailAddress, emailVm.SenderPassword)
                };
                var mailMessage = new MailMessage
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    From = new MailAddress(emailVm.SenderEmailAddress)
                };
                mailMessage.To.Add(emailVm.ReceiverEmailAddress);
                mailMessage.Subject = emailVm.EmailSubject;
                mailMessage.Body = emailVm.EmailBody;
                client.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult paymentSuccessful()
        {
            return View();
        }

        public ActionResult datHangThanhCong()
        {
            return View();
        }

        public ActionResult datHang()
        {
            TAIKHOANKHACHHANG taiKhoanKH = context.TAIKHOANKHACHHANGs.FirstOrDefault(p => p.TENDANGNHAP.Contains(User.Identity.Name));

            if (taiKhoanKH != null && Session["cart"] != null)
            {
                DONHANG newDonHang = new DONHANG();
                newDonHang.MAKHACHHANG = taiKhoanKH.KHACHHANG.MAKHACHHANG;
                newDonHang.MANHANVIEN = 1;
                newDonHang.NGAYTHANHTOAN = DateTime.Now;
                context.DONHANGs.Add(newDonHang);
                context.SaveChanges();
                creataCTDH(newDonHang.MADONHANG);
                sendingEMail();
                Session["cart"] = null;
            }
            return RedirectToAction("datHangThanhCong", "Home");
        }

        public ActionResult order()
        {
            TAIKHOANKHACHHANG taiKhoanKH = context.TAIKHOANKHACHHANGs.FirstOrDefault(p => p.TENDANGNHAP.Contains(User.Identity.Name));

            List<ViewCart> listGioHang = sessionToCart();
            decimal TongTien = listGioHang
                        .GroupBy(p => p.MASANPHAM) // Group the items by MASANPHAM
                        .OrderBy(p => p.Key) // Order the groups by MASANPHAM
                        .Sum(p => p.Sum(x => decimal.Parse(x.TONGTIEN))); // Calculate the sum of TongTien for each group

            if (taiKhoanKH != null && Session["cart"] != null)
            {
                DONHANG newDonHang = new DONHANG();
                newDonHang.MAKHACHHANG = taiKhoanKH.KHACHHANG.MAKHACHHANG;
                newDonHang.MANHANVIEN = 1;
                newDonHang.NGAYTHANHTOAN = DateTime.Now;
                context.DONHANGs.Add(newDonHang); 
                context.SaveChanges();
                creataCTDH(newDonHang.MADONHANG);
                Session["cart"] = null;
            }
            return RedirectToAction("Payment", "VnPay", new {tongTien = TongTien});
        }

        public ActionResult viewProfileAndChangePassword(int? id)
        {
            KHACHHANG kHACHHANG = context.KHACHHANGs.FirstOrDefault(p => p.MAKHACHHANG == id);
            return View(kHACHHANG);
        }

        public ActionResult viewProfileAndChangePasswordAdmin(int? id)
        {
            NHANVIEN nHANVIEN = context.NHANVIENs.FirstOrDefault(p => p.MANHANVIEN == id);
            return View(nHANVIEN);
        }
        // POST: Admin/KHACHHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

            public async Task<ActionResult> EditKhachHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = await context.KHACHHANGs.FindAsync(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditKhachHang([Bind(Include = "MAKHACHHANG,TENKHACHHANG,EMAIL,CCCD,NGAYSINH,DIACHI,SDT")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                context.Entry(kHACHHANG).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("checkOut", "Home", new {id = kHACHHANG.MAKHACHHANG});
            }
            return View(kHACHHANG);
        }


        public ActionResult viewDetailDonHang(int? id)
        {
            var listChiTietDonHang = context.CHITIETDONHANGs.Where(p => p.MADONHANG == id).ToList();
            return View(listChiTietDonHang);
        }
        public async Task<ActionResult> EditAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = await context.NHANVIENs.FindAsync(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // POST: Admin/NHANVIENs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAdmin([Bind(Include = "MANHANVIEN,TENNHANVIEN,HINH,NGAYSINH,CCCD,CHUCVU,DIACHI,EMAIL,SDT")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                context.Entry(nHANVIEN).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("viewProfileAndChangePasswordAdmin", "Home", new { id = nHANVIEN.MANHANVIEN });
            }
            return View(nHANVIEN);
        }

        public async Task<ActionResult> ChangePassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANKHACHHANG tAIKHOANKHACHHANG = await context.TAIKHOANKHACHHANGs.FindAsync(id);
            if (tAIKHOANKHACHHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAKHACHHANG = new SelectList(context.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", tAIKHOANKHACHHANG.MAKHACHHANG);
            return View(tAIKHOANKHACHHANG);
        }

        // POST: Admin/TAIKHOANKHACHHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Include = "TENDANGNHAP,MATKHAU,MAKHACHHANG")] TAIKHOANKHACHHANG tAIKHOANKHACHHANG)
        {
            if (ModelState.IsValid)
            {
                context.Entry(tAIKHOANKHACHHANG).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MAKHACHHANG = new SelectList(context.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", tAIKHOANKHACHHANG.MAKHACHHANG);
            return View(tAIKHOANKHACHHANG);
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = await context.KHACHHANGs.FindAsync(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Admin/KHACHHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MAKHACHHANG,TENKHACHHANG,EMAIL,CCCD,NGAYSINH,DIACHI,SDT")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                context.Entry(kHACHHANG).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("viewProfileAndChangePassword", "Home", new { id =  kHACHHANG.MAKHACHHANG });
            }
            return View(kHACHHANG);
        }

        public ActionResult all_Computer()
        {
            var listSanPham = context.SANPHAMs.ToList();
            return View(listSanPham);
        }

    }

}