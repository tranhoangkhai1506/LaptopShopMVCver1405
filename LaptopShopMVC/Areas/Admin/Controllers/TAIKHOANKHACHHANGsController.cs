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
    public class TAIKHOANKHACHHANGsController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/TAIKHOANKHACHHANGs
        public async Task<ActionResult> Index()
        {
            var tAIKHOANKHACHHANGs = db.TAIKHOANKHACHHANGs.Include(t => t.KHACHHANG);
            return View(await tAIKHOANKHACHHANGs.ToListAsync());
        }

        // GET: Admin/TAIKHOANKHACHHANGs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANKHACHHANG tAIKHOANKHACHHANG = await db.TAIKHOANKHACHHANGs.FindAsync(id);
            if (tAIKHOANKHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOANKHACHHANG);
        }

        // GET: Admin/TAIKHOANKHACHHANGs/Create
        public ActionResult Create()
        {
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG");
            return View();
        }

        // POST: Admin/TAIKHOANKHACHHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TENDANGNHAP,MATKHAU,MAKHACHHANG")] TAIKHOANKHACHHANG tAIKHOANKHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.TAIKHOANKHACHHANGs.Add(tAIKHOANKHACHHANG);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", tAIKHOANKHACHHANG.MAKHACHHANG);
            return View(tAIKHOANKHACHHANG);
        }

        // GET: Admin/TAIKHOANKHACHHANGs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANKHACHHANG tAIKHOANKHACHHANG = await db.TAIKHOANKHACHHANGs.FindAsync(id);
            if (tAIKHOANKHACHHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", tAIKHOANKHACHHANG.MAKHACHHANG);
            return View(tAIKHOANKHACHHANG);
        }

        // POST: Admin/TAIKHOANKHACHHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TENDANGNHAP,MATKHAU,MAKHACHHANG")] TAIKHOANKHACHHANG tAIKHOANKHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tAIKHOANKHACHHANG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MAKHACHHANG = new SelectList(db.KHACHHANGs, "MAKHACHHANG", "TENKHACHHANG", tAIKHOANKHACHHANG.MAKHACHHANG);
            return View(tAIKHOANKHACHHANG);
        }

        // GET: Admin/TAIKHOANKHACHHANGs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANKHACHHANG tAIKHOANKHACHHANG = await db.TAIKHOANKHACHHANGs.FindAsync(id);
            if (tAIKHOANKHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOANKHACHHANG);
        }

        // POST: Admin/TAIKHOANKHACHHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TAIKHOANKHACHHANG tAIKHOANKHACHHANG = await db.TAIKHOANKHACHHANGs.FindAsync(id);
            db.TAIKHOANKHACHHANGs.Remove(tAIKHOANKHACHHANG);
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
