using LaptopShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LaptopShopMVC.Controllers
{
    public class AccountController : Controller
    {
        LaptopDBContext context = new LaptopDBContext();
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                TAIKHOANQUANTRI user = null;
                TAIKHOANKHACHHANG customer = null;
                var tendangnhap = collection["tendangnhap"];
                var matkhau = collection["matkhau"];
                using (var db = new LaptopDBContext())
                {
                    user = db.TAIKHOANQUANTRIs.Where(n => n.TENDANGNHAP == tendangnhap && n.MATKHAU == matkhau).FirstOrDefault();
                }
                using (var db = new LaptopDBContext())
                {
                    customer = db.TAIKHOANKHACHHANGs.Where(n => n.TENDANGNHAP == tendangnhap && n.MATKHAU == matkhau).FirstOrDefault();
                }

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.TENDANGNHAP, false);
                    return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                }
                
                else if (customer != null)
                {
                    FormsAuthentication.SetAuthCookie(customer.TENDANGNHAP, false);
                    return RedirectToAction("Index", "Home");
                } 
                else
                {
                    ViewBag.ThongBao = "Log in failed, Try again!"; 
                }
            }
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Nhapkhachhang()
        {
            return View();
        }
        public static string cccd_khachhang = "";
        [HttpPost]
        public ActionResult Nhapkhachhang(FormCollection collection, KHACHHANG kh)
        {
            String ten = collection["TENKHACHHANG"];
            String email = collection["EMAIL"];
            String cccd = collection["CCCD"];
            String ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["NGAYSINH"]);
            String diachi = collection["DIACHI"];
            String sdt = collection["SDT"];

            if (String.IsNullOrEmpty(cccd))
            {
                ViewBag["NhapCCCD"] = "Phải nhập số căn cước công dân";
            }
            else
            {
                kh.TENKHACHHANG = ten;
                kh.EMAIL = email;
                kh.CCCD = cccd;
                kh.NGAYSINH = DateTime.Parse(ngaysinh);
                kh.DIACHI = diachi;
                kh.SDT = sdt;

                context.KHACHHANGs.Add(kh);
                context.SaveChanges();

                cccd_khachhang = cccd;
                return RedirectToAction("NhapTaiKhoankhachhang");
            }
            return this.Nhapkhachhang();
        }

        [HttpGet]
        public ActionResult Nhaptaikhoankhachhang()
        {   
            
            return View();
        }
        [HttpPost]
        public ActionResult Nhaptaikhoankhachhang(FormCollection collection, TAIKHOANKHACHHANG tk)
        {
            String tendangnhap = collection["TENDANGNHAP"];
            String matkhau = collection["MATKHAU"];


            if (cccd_khachhang != "")
            {
                var khachHangTaoTaiKhoan = context.KHACHHANGs.FirstOrDefault(p => p.CCCD.Contains(cccd_khachhang));

                if (khachHangTaoTaiKhoan != null)
                {
                    if (String.IsNullOrEmpty(tendangnhap))
                    {
                        ViewBag["Nhaptendangnhap"] = "Phải nhập tên đăng nhập";
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(matkhau))
                        {
                            ViewBag["Nhapmatkhau"] = "Phải nhập mật khẩu";
                        }
                        else
                        {
                            tk.TENDANGNHAP = tendangnhap;
                            tk.MATKHAU = matkhau;
                            tk.MAKHACHHANG = khachHangTaoTaiKhoan.MAKHACHHANG;

                            context.TAIKHOANKHACHHANGs.Add(tk);
                            context.SaveChanges();

                            return RedirectToAction("Login");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Nhapkhachhang");
                }
            }
            else
            {
                return RedirectToAction("Nhapkhachhang");
            }
            return this.Nhaptaikhoankhachhang();
        }
    }
}