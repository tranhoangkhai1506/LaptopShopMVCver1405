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
    public class TAIKHOANQUANTRIsController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/TAIKHOANQUANTRIs
        public async Task<ActionResult> Index()
        {
            var tAIKHOANQUANTRIs = db.TAIKHOANQUANTRIs.Include(t => t.NHANVIEN).Include(t => t.PHANQUYEN);
            return View(await tAIKHOANQUANTRIs.ToListAsync());
        }

        // GET: Admin/TAIKHOANQUANTRIs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANQUANTRI tAIKHOANQUANTRI = await db.TAIKHOANQUANTRIs.FindAsync(id);
            if (tAIKHOANQUANTRI == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOANQUANTRI);
        }

        // GET: Admin/TAIKHOANQUANTRIs/Create
        public ActionResult Create()
        {
            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN");
            ViewBag.MAPHANQUYEN = new SelectList(db.PHANQUYENs, "MAPHANQUYEN", "TENPHANQUYEN");
            return View();
        }

        // POST: Admin/TAIKHOANQUANTRIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TENDANGNHAP,MATKHAU,MANHANVIEN,MAPHANQUYEN")] TAIKHOANQUANTRI tAIKHOANQUANTRI)
        {
            if (ModelState.IsValid)
            {
                db.TAIKHOANQUANTRIs.Add(tAIKHOANQUANTRI);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN", tAIKHOANQUANTRI.MANHANVIEN);
            ViewBag.MAPHANQUYEN = new SelectList(db.PHANQUYENs, "MAPHANQUYEN", "TENPHANQUYEN", tAIKHOANQUANTRI.MAPHANQUYEN);
            return View(tAIKHOANQUANTRI);
        }

        // GET: Admin/TAIKHOANQUANTRIs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANQUANTRI tAIKHOANQUANTRI = await db.TAIKHOANQUANTRIs.FindAsync(id);
            if (tAIKHOANQUANTRI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN", tAIKHOANQUANTRI.MANHANVIEN);
            ViewBag.MAPHANQUYEN = new SelectList(db.PHANQUYENs, "MAPHANQUYEN", "TENPHANQUYEN", tAIKHOANQUANTRI.MAPHANQUYEN);
            return View(tAIKHOANQUANTRI);
        }

        // POST: Admin/TAIKHOANQUANTRIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TENDANGNHAP,MATKHAU,MANHANVIEN,MAPHANQUYEN")] TAIKHOANQUANTRI tAIKHOANQUANTRI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tAIKHOANQUANTRI).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MANHANVIEN = new SelectList(db.NHANVIENs, "MANHANVIEN", "TENNHANVIEN", tAIKHOANQUANTRI.MANHANVIEN);
            ViewBag.MAPHANQUYEN = new SelectList(db.PHANQUYENs, "MAPHANQUYEN", "TENPHANQUYEN", tAIKHOANQUANTRI.MAPHANQUYEN);
            return View(tAIKHOANQUANTRI);
        }

        // GET: Admin/TAIKHOANQUANTRIs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANQUANTRI tAIKHOANQUANTRI = await db.TAIKHOANQUANTRIs.FindAsync(id);
            if (tAIKHOANQUANTRI == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOANQUANTRI);
        }

        // POST: Admin/TAIKHOANQUANTRIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TAIKHOANQUANTRI tAIKHOANQUANTRI = await db.TAIKHOANQUANTRIs.FindAsync(id);
            db.TAIKHOANQUANTRIs.Remove(tAIKHOANQUANTRI);
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
