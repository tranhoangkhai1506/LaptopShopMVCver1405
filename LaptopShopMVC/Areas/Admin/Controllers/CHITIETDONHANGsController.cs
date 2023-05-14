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
    public class CHITIETDONHANGsController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/CHITIETDONHANGs
        public async Task<ActionResult> Index()
        {
            var cHITIETDONHANGs = db.CHITIETDONHANGs.Include(c => c.DONHANG).Include(c => c.SANPHAM);
            return View(await cHITIETDONHANGs.ToListAsync());
        }

        // GET: Admin/CHITIETDONHANGs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = await db.CHITIETDONHANGs.FindAsync(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDONHANG);
        }

        // GET: Admin/CHITIETDONHANGs/Create
        public ActionResult Create()
        {
            ViewBag.MADONHANG = new SelectList(db.DONHANGs, "MADONHANG", "MADONHANG");
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM");
            return View();
        }

        // POST: Admin/CHITIETDONHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MADONHANG,MASANPHAM,SOLUONG,GIA,NGAYLAP")] CHITIETDONHANG cHITIETDONHANG)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETDONHANGs.Add(cHITIETDONHANG);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MADONHANG = new SelectList(db.DONHANGs, "MADONHANG", "MADONHANG", cHITIETDONHANG.MADONHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", cHITIETDONHANG.MASANPHAM);
            return View(cHITIETDONHANG);
        }

        // GET: Admin/CHITIETDONHANGs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = await db.CHITIETDONHANGs.FindAsync(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADONHANG = new SelectList(db.DONHANGs, "MADONHANG", "MADONHANG", cHITIETDONHANG.MADONHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", cHITIETDONHANG.MASANPHAM);
            return View(cHITIETDONHANG);
        }

        // POST: Admin/CHITIETDONHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MADONHANG,MASANPHAM,SOLUONG,GIA,NGAYLAP")] CHITIETDONHANG cHITIETDONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETDONHANG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MADONHANG = new SelectList(db.DONHANGs, "MADONHANG", "MADONHANG", cHITIETDONHANG.MADONHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", cHITIETDONHANG.MASANPHAM);
            return View(cHITIETDONHANG);
        }

        // GET: Admin/CHITIETDONHANGs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = await db.CHITIETDONHANGs.FindAsync(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDONHANG);
        }

        // POST: Admin/CHITIETDONHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CHITIETDONHANG cHITIETDONHANG = await db.CHITIETDONHANGs.FindAsync(id);
            db.CHITIETDONHANGs.Remove(cHITIETDONHANG);
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
