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
    public class CHITIETPHIEUNHAPHANGsController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/CHITIETPHIEUNHAPHANGs
        public async Task<ActionResult> Index()
        {
            var cHITIETPHIEUNHAPHANGs = db.CHITIETPHIEUNHAPHANGs.Include(c => c.PHIEUNHAPHANG).Include(c => c.SANPHAM);
            return View(await cHITIETPHIEUNHAPHANGs.ToListAsync());
        }

        // GET: Admin/CHITIETPHIEUNHAPHANGs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETPHIEUNHAPHANG cHITIETPHIEUNHAPHANG = await db.CHITIETPHIEUNHAPHANGs.FindAsync(id);
            if (cHITIETPHIEUNHAPHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETPHIEUNHAPHANG);
        }

        // GET: Admin/CHITIETPHIEUNHAPHANGs/Create
        public ActionResult Create()
        {
            ViewBag.MAPHIEUNHAPHANG = new SelectList(db.PHIEUNHAPHANGs, "MAPHIEUNHAPHANG", "MAPHIEUNHAPHANG");
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM");
            return View();
        }

        // POST: Admin/CHITIETPHIEUNHAPHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MAPHIEUNHAPHANG,MASANPHAM,SOLUONGNHAP,GIA,NGAYLAP")] CHITIETPHIEUNHAPHANG cHITIETPHIEUNHAPHANG)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETPHIEUNHAPHANGs.Add(cHITIETPHIEUNHAPHANG);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MAPHIEUNHAPHANG = new SelectList(db.PHIEUNHAPHANGs, "MAPHIEUNHAPHANG", "MAPHIEUNHAPHANG", cHITIETPHIEUNHAPHANG.MAPHIEUNHAPHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", cHITIETPHIEUNHAPHANG.MASANPHAM);
            return View(cHITIETPHIEUNHAPHANG);
        }

        // GET: Admin/CHITIETPHIEUNHAPHANGs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETPHIEUNHAPHANG cHITIETPHIEUNHAPHANG = await db.CHITIETPHIEUNHAPHANGs.FindAsync(id);
            if (cHITIETPHIEUNHAPHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAPHIEUNHAPHANG = new SelectList(db.PHIEUNHAPHANGs, "MAPHIEUNHAPHANG", "MAPHIEUNHAPHANG", cHITIETPHIEUNHAPHANG.MAPHIEUNHAPHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", cHITIETPHIEUNHAPHANG.MASANPHAM);
            return View(cHITIETPHIEUNHAPHANG);
        }

        // POST: Admin/CHITIETPHIEUNHAPHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MAPHIEUNHAPHANG,MASANPHAM,SOLUONGNHAP,GIA,NGAYLAP")] CHITIETPHIEUNHAPHANG cHITIETPHIEUNHAPHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETPHIEUNHAPHANG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MAPHIEUNHAPHANG = new SelectList(db.PHIEUNHAPHANGs, "MAPHIEUNHAPHANG", "MAPHIEUNHAPHANG", cHITIETPHIEUNHAPHANG.MAPHIEUNHAPHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", cHITIETPHIEUNHAPHANG.MASANPHAM);
            return View(cHITIETPHIEUNHAPHANG);
        }

        // GET: Admin/CHITIETPHIEUNHAPHANGs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETPHIEUNHAPHANG cHITIETPHIEUNHAPHANG = await db.CHITIETPHIEUNHAPHANGs.FindAsync(id);
            if (cHITIETPHIEUNHAPHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETPHIEUNHAPHANG);
        }

        // POST: Admin/CHITIETPHIEUNHAPHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CHITIETPHIEUNHAPHANG cHITIETPHIEUNHAPHANG = await db.CHITIETPHIEUNHAPHANGs.FindAsync(id);
            db.CHITIETPHIEUNHAPHANGs.Remove(cHITIETPHIEUNHAPHANG);
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
