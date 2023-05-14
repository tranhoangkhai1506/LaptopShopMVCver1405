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
    public class DANHGIAsController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/DANHGIAs
        public async Task<ActionResult> Index()
        {
            var dANHGIAs = db.DANHGIAs.Include(d => d.KHACHHANG).Include(d => d.SANPHAM);
            return View(await dANHGIAs.ToListAsync());
        }

        // GET: Admin/DANHGIAs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHGIA dANHGIA = await db.DANHGIAs.FindAsync(id);
            if (dANHGIA == null)
            {
                return HttpNotFound();
            }
            return View(dANHGIA);
        }

        // GET: Admin/DANHGIAs/Create
        public ActionResult Create()
        {
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG");
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM");
            return View();
        }

        // POST: Admin/DANHGIAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MADANHGIA,MAKHACHHANG,MASANPHAM,NOIDUNG")] DANHGIA dANHGIA)
        {
            if (ModelState.IsValid)
            {
                db.DANHGIAs.Add(dANHGIA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", dANHGIA.MAKHACHHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", dANHGIA.MASANPHAM);
            return View(dANHGIA);
        }

        // GET: Admin/DANHGIAs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHGIA dANHGIA = await db.DANHGIAs.FindAsync(id);
            if (dANHGIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", dANHGIA.MAKHACHHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", dANHGIA.MASANPHAM);
            return View(dANHGIA);
        }

        // POST: Admin/DANHGIAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MADANHGIA,MAKHACHHANG,MASANPHAM,NOIDUNG")] DANHGIA dANHGIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dANHGIA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", dANHGIA.MAKHACHHANG);
            ViewBag.MASANPHAM = new SelectList(db.SANPHAMs, "MASANPHAM", "TENSANPHAM", dANHGIA.MASANPHAM);
            return View(dANHGIA);
        }

        // GET: Admin/DANHGIAs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHGIA dANHGIA = await db.DANHGIAs.FindAsync(id);
            if (dANHGIA == null)
            {
                return HttpNotFound();
            }
            return View(dANHGIA);
        }

        // POST: Admin/DANHGIAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DANHGIA dANHGIA = await db.DANHGIAs.FindAsync(id);
            db.DANHGIAs.Remove(dANHGIA);
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
