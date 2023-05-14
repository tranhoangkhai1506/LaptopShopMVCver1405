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
    public class SANPHAMsController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/SANPHAMs
        public async Task<ActionResult> Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.DANHMUC).Include(s => s.THUONGHIEU);
            return View(await sANPHAMs.ToListAsync());
        }

        // GET: Admin/SANPHAMs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = await db.SANPHAMs.FindAsync(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.MADANHMUC = new SelectList(db.DANHMUCs, "MADANHMUC", "TENDANHMUC");
            ViewBag.MATHUONGHIEU = new SelectList(db.THUONGHIEUx, "MATHUONGHIEU", "TENTHUONGHIEU");
            return View();
        }

        // POST: Admin/SANPHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MASANPHAM,TENSANPHAM,MOTA,HINH,RAM,ROM,CPU,CARDMANHINH,THOIGIANBAOHANH,GIABAN,GIANHAP,TINHTRANG,SOLUONG,MATHUONGHIEU,MADANHMUC")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MADANHMUC = new SelectList(db.DANHMUCs, "MADANHMUC", "TENDANHMUC", sANPHAM.MADANHMUC);
            ViewBag.MATHUONGHIEU = new SelectList(db.THUONGHIEUx, "MATHUONGHIEU", "TENTHUONGHIEU", sANPHAM.MATHUONGHIEU);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = await db.SANPHAMs.FindAsync(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADANHMUC = new SelectList(db.DANHMUCs, "MADANHMUC", "TENDANHMUC", sANPHAM.MADANHMUC);
            ViewBag.MATHUONGHIEU = new SelectList(db.THUONGHIEUx, "MATHUONGHIEU", "TENTHUONGHIEU", sANPHAM.MATHUONGHIEU);
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MASANPHAM,TENSANPHAM,MOTA,HINH,RAM,ROM,CPU,CARDMANHINH,THOIGIANBAOHANH,GIABAN,GIANHAP,TINHTRANG,SOLUONG,MATHUONGHIEU,MADANHMUC")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MADANHMUC = new SelectList(db.DANHMUCs, "MADANHMUC", "TENDANHMUC", sANPHAM.MADANHMUC);
            ViewBag.MATHUONGHIEU = new SelectList(db.THUONGHIEUx, "MATHUONGHIEU", "TENTHUONGHIEU", sANPHAM.MATHUONGHIEU);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = await db.SANPHAMs.FindAsync(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = await db.SANPHAMs.FindAsync(id);
            db.SANPHAMs.Remove(sANPHAM);
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
