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
    public class PHIEUBAOHANHsController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/PHIEUBAOHANHs
        public async Task<ActionResult> Index()
        {
            var pHIEUBAOHANHs = db.PHIEUBAOHANHs.Include(p => p.KHACHHANG).Include(p => p.SANPHAM);
            return View(await pHIEUBAOHANHs.ToListAsync());
        }

        // GET: Admin/PHIEUBAOHANHs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUBAOHANH pHIEUBAOHANH = await db.PHIEUBAOHANHs.FindAsync(id);
            if (pHIEUBAOHANH == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUBAOHANH);
        }

        // GET: Admin/PHIEUBAOHANHs/Create
        public ActionResult Create()
        {
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG");
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM");
            return View();
        }

        // POST: Admin/PHIEUBAOHANHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MAPHIEUBAOHANH,MAKHACHHANG,NGAYBATDAU,NGAYKETTHUC,MASANPHAM")] PHIEUBAOHANH pHIEUBAOHANH)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUBAOHANHs.Add(pHIEUBAOHANH);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", pHIEUBAOHANH.MAKHACHHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", pHIEUBAOHANH.MASANPHAM);
            return View(pHIEUBAOHANH);
        }

        // GET: Admin/PHIEUBAOHANHs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUBAOHANH pHIEUBAOHANH = await db.PHIEUBAOHANHs.FindAsync(id);
            if (pHIEUBAOHANH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", pHIEUBAOHANH.MAKHACHHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", pHIEUBAOHANH.MASANPHAM);
            return View(pHIEUBAOHANH);
        }

        // POST: Admin/PHIEUBAOHANHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MAPHIEUBAOHANH,MAKHACHHANG,NGAYBATDAU,NGAYKETTHUC,MASANPHAM")] PHIEUBAOHANH pHIEUBAOHANH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUBAOHANH).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", pHIEUBAOHANH.MAKHACHHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", pHIEUBAOHANH.MASANPHAM);
            return View(pHIEUBAOHANH);
        }

        // GET: Admin/PHIEUBAOHANHs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUBAOHANH pHIEUBAOHANH = await db.PHIEUBAOHANHs.FindAsync(id);
            if (pHIEUBAOHANH == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUBAOHANH);
        }

        // POST: Admin/PHIEUBAOHANHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PHIEUBAOHANH pHIEUBAOHANH = await db.PHIEUBAOHANHs.FindAsync(id);
            db.PHIEUBAOHANHs.Remove(pHIEUBAOHANH);
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
