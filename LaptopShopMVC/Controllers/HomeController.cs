using LaptopShopMVC.App_Start;
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
        public ActionResult SearchingResults(string nameLapTop, string nameBrand, double? minPrice, double? maxPrice, string ramValue)
        {
            List<SANPHAM> listSanPhams = null;

            if (nameLapTop != "")
            {
                listSanPhams = context.SANPHAMs.Where(p => p.TENSANPHAM.Contains(nameLapTop)).ToList();

                if (nameBrand != "Select Brand" && nameBrand != "")
                {
                    listSanPhams = context.SANPHAMs.Where(p => p.THUONGHIEU.TENTHUONGHIEU.Contains(nameBrand) && p.TENSANPHAM.Contains(nameLapTop)).ToList();

                    if (minPrice != null && maxPrice != null)
                    {
                        decimal min = Convert.ToDecimal(minPrice);
                        decimal max = Convert.ToDecimal(maxPrice);

                        listSanPhams = context.SANPHAMs.Where(p => p.GIABAN >= min & p.GIABAN <= max && p.THUONGHIEU.TENTHUONGHIEU.Contains(nameBrand)).ToList();

                        if (ramValue != "" && ramValue != "Select RAM Value")
                        {
                            listSanPhams = context.SANPHAMs.Where(p => p.RAM.Contains(ramValue) && p.TENSANPHAM.Contains(nameLapTop)).ToList(); 
                        } 
                    }

                }else if (minPrice != null && maxPrice != null)
                {
                    decimal min = Convert.ToDecimal(minPrice);
                    decimal max = Convert.ToDecimal(maxPrice);

                    listSanPhams = context.SANPHAMs.Where(p => p.GIABAN >= min & p.GIABAN <= max).ToList();

                    if (ramValue != "" && ramValue != "Select RAM Value")
                    {
                        listSanPhams = context.SANPHAMs.Where(p => p.RAM.Contains(ramValue) && p.TENSANPHAM.Contains(nameLapTop)).ToList();
                    }
                }
                else
                {
                    if (ramValue != "" && ramValue != "Select RAM Value")
                    {
                        listSanPhams = context.SANPHAMs.Where(p => p.RAM.Contains(ramValue) && p.TENSANPHAM.Contains(nameLapTop)).ToList();
                    }
                }
            } else if (nameBrand != "Select Brand" && nameBrand != "")
            {
                listSanPhams = context.SANPHAMs.Where(p => p.THUONGHIEU.TENTHUONGHIEU.Contains(nameBrand)).ToList();

                if (minPrice != null && maxPrice != null)
                {
                    decimal min = Convert.ToDecimal(minPrice);
                    decimal max = Convert.ToDecimal(maxPrice);

                    listSanPhams = context.SANPHAMs.Where(p => p.GIABAN >= min && p.GIABAN <= max && p.THUONGHIEU.TENTHUONGHIEU.Contains(nameBrand)).ToList(); 
                }
                if (ramValue != "" && ramValue != "Select RAM Value")
                {
                    listSanPhams = context.SANPHAMs.Where(p => p.RAM.Contains(ramValue) && p.THUONGHIEU.TENTHUONGHIEU.Contains(nameBrand)).ToList();
                }
            }
            else if (minPrice != null && maxPrice != null)
            {
                decimal min = Convert.ToDecimal(minPrice);
                decimal max = Convert.ToDecimal(maxPrice);

                listSanPhams = context.SANPHAMs.Where(p => p.GIABAN >= min & p.GIABAN <= max).ToList();

                if (ramValue != "" && ramValue != "Select RAM Value")
                {
                    listSanPhams = context.SANPHAMs.Where(p => p.RAM.Contains(ramValue) && p.TENSANPHAM.Contains(nameLapTop)).ToList();
                }
            } else if (ramValue != "" && ramValue != "Select RAM Value")
            {
                listSanPhams = context.SANPHAMs.Where(p => p.RAM.Contains(ramValue)).ToList();
            }
            else
            {
                return RedirectToAction("errorResult");
            }

            if (listSanPhams.Count() != 0)
            {
                return View(listSanPhams);
            }
            else
            {
                return RedirectToAction("errorResult");
            }

        }

        public ActionResult viewAllProductOfBrand(string tenThuongHieu)
        {
            var listSanPham = context.SANPHAMs.Where(p => p.THUONGHIEU.TENTHUONGHIEU.Contains(tenThuongHieu)).ToList();
            if (listSanPham.Count() != 0)
            {
                return View(listSanPham);
            }
            return RedirectToAction("errorResult");
        }

        public ActionResult viewDetail(int maSanPham)
        {
            var sanPham = context.SANPHAMs.FirstOrDefault(p => p.MASANPHAM == maSanPham);
            if (sanPham != null)
            {
                return View(sanPham);
            }
            return RedirectToAction("errorResult");
        }

        public ActionResult errorResult()
        {
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
            return RedirectToAction("errorResult");

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
                    return RedirectToAction("errorResult");
                }
            }
            return RedirectToAction("errorResult");

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
            int soThangCach = 6; // Số tháng cần cách

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
        public ActionResult order()
        {
            KHACHHANG khachHang;

            TAIKHOANKHACHHANG taiKhoanKH = context.TAIKHOANKHACHHANGs.FirstOrDefault(p => p.TENDANGNHAP.Contains(User.Identity.Name));
            
            if (taiKhoanKH != null)
            {
                DONHANG newDonHang = new DONHANG();
                newDonHang.MAKHACHHANG = taiKhoanKH.KHACHHANG.MAKHACHHANG;
                newDonHang.MANHANVIEN = 1;
                newDonHang.NGAYTHANHTOAN = DateTime.Now;
                context.DONHANGs.Add(newDonHang); 
                context.SaveChanges();
                creataCTDH(newDonHang.MADONHANG);
                
                //send email
                khachHang = taiKhoanKH.KHACHHANG;

                DONHANG donhang_KH = context.DONHANGs.Where(m => m.MAKHACHHANG == khachHang.MAKHACHHANG).ToArray().Last();

                EmailViewModels emailVm = new EmailViewModels();

                emailVm.EmailBody = @"<h2>Hello " + khachHang.TENKHACHHANG + "! </h2> <br />" +
                                "<h3>THÔNG TIN ĐƠN HÀNG</h3>" +
                                "Mã Đơn Hàng: " + donhang_KH.MADONHANG + "<br/>" +
                                "Ngày Thanh Toán: " + donhang_KH.NGAYTHANHTOAN + "<br/>" +
                                "Tổng Tiền: " + donhang_KH.TONGTIEN + "<br/>" +
                                "Ngày gửi: " + DateTime.Now.ToString() + "<br/>" +
                                "<br/>Thanks for shopping with FRICA!";

                emailVm.SenderEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"];
                emailVm.SenderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"];
                emailVm.SmtpHostServer = System.Configuration.ConfigurationManager.AppSettings["smtpHostServer"];
                emailVm.SmtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                emailVm.ReceiverEmailAddress = khachHang.EMAIL;
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
                    return HttpNotFound();
                }
            }
            return this.View();
        }
    }

}