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
    public class PHIEUNHAPHANGsController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/PHIEUNHAPHANGs
        public async Task<ActionResult> Index()
        {
            var pHIEUNHAPHANGs = db.PHIEUNHAPHANGs.Include(p => p.NHANVIEN);
            return View(await pHIEUNHAPHANGs.ToListAsync());
        }

        // GET: Admin/PHIEUNHAPHANGs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPHANG pHIEUNHAPHANG = await db.PHIEUNHAPHANGs.FindAsync(id);
            if (pHIEUNHAPHANG == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUNHAPHANG);
        }

        // GET: Admin/PHIEUNHAPHANGs/Create
        public ActionResult Create()
        {
            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN");
            return View();
        }

        // POST: Admin/PHIEUNHAPHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MAPHIEUNHAPHANG,NGAYNHAPHANG,TONGTIEN,MANHANVIEN")] PHIEUNHAPHANG pHIEUNHAPHANG)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUNHAPHANGs.Add(pHIEUNHAPHANG);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN", pHIEUNHAPHANG.MANHANVIEN);
            return View(pHIEUNHAPHANG);
        }

        // GET: Admin/PHIEUNHAPHANGs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPHANG pHIEUNHAPHANG = await db.PHIEUNHAPHANGs.FindAsync(id);
            if (pHIEUNHAPHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN", pHIEUNHAPHANG.MANHANVIEN);
            return View(pHIEUNHAPHANG);
        }

        // POST: Admin/PHIEUNHAPHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MAPHIEUNHAPHANG,NGAYNHAPHANG,TONGTIEN,MANHANVIEN")] PHIEUNHAPHANG pHIEUNHAPHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUNHAPHANG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN", pHIEUNHAPHANG.MANHANVIEN);
            return View(pHIEUNHAPHANG);
        }

        // GET: Admin/PHIEUNHAPHANGs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPHANG pHIEUNHAPHANG = await db.PHIEUNHAPHANGs.FindAsync(id);
            if (pHIEUNHAPHANG == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUNHAPHANG);
        }

        // POST: Admin/PHIEUNHAPHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PHIEUNHAPHANG pHIEUNHAPHANG = await db.PHIEUNHAPHANGs.FindAsync(id);
            db.PHIEUNHAPHANGs.Remove(pHIEUNHAPHANG);
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
