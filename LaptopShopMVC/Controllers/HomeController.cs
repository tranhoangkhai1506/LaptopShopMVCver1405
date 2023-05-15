using LaptopShopMVC.App_Start;
using LaptopShopMVC.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaptopShopMVC.Extras;

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
                int check = 0;
                foreach (var item in mainlist)
                {
                    if (item.maSanPham == id)
                    {
                        item.soLuong = item.soLuong + 1;
                        check = 0;
                        break;
                    }
                    else
                    {
                        check = 1;
                    }
                }
                if (check == 1)
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

    }

}