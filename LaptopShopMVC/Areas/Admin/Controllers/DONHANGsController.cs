using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaptopShopMVC.Models;

namespace LaptopShopMVC.Areas.Admin.Controllers
{
    public class DONHANGsController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/DONHANGs
        public async Task<ActionResult> Index()
        {
            var dONHANGs = db.DONHANGs.Include(d => d.KHACHHANG).Include(d => d.NHANVIEN);
            return View(await dONHANGs.ToListAsync());
        }

        // GET: Admin/DONHANGs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = await db.DONHANGs.FindAsync(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // GET: Admin/DONHANGs/Create
        public ActionResult Create()
        {
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG");
            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN");
            return View();
        }

        // POST: Admin/DONHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MADONHANG,TONGTIEN,NGAYTHANHTOAN,MANHANVIEN,MAKHACHHANG")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.DONHANGs.Add(dONHANG);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", dONHANG.MAKHACHHANG);
            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN", dONHANG.MANHANVIEN);
            return View(dONHANG);
        }

        // GET: Admin/DONHANGs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = await db.DONHANGs.FindAsync(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", dONHANG.MAKHACHHANG);
            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN", dONHANG.MANHANVIEN);
            return View(dONHANG);
        }

        // POST: Admin/DONHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MADONHANG,TONGTIEN,NGAYTHANHTOAN,MANHANVIEN,MAKHACHHANG")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONHANG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", dONHANG.MAKHACHHANG);
            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN", dONHANG.MANHANVIEN);
            return View(dONHANG);
        }

        // GET: Admin/DONHANGs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = await db.DONHANGs.FindAsync(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // POST: Admin/DONHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DONHANG dONHANG = await db.DONHANGs.FindAsync(id);
            db.DONHANGs.Remove(dONHANG);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
