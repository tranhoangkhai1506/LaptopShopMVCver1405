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
    public class PHANQUYENsController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/PHANQUYENs
        public async Task<ActionResult> Index()
        {
            return View(await db.PHANQUYENs.ToListAsync());
        }

        // GET: Admin/PHANQUYENs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHANQUYEN pHANQUYEN = await db.PHANQUYENs.FindAsync(id);
            if (pHANQUYEN == null)
            {
                return HttpNotFound();
            }
            return View(pHANQUYEN);
        }

        // GET: Admin/PHANQUYENs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PHANQUYENs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MAPHANQUYEN,TENPHANQUYEN,QUYENHAN")] PHANQUYEN pHANQUYEN)
        {
            if (ModelState.IsValid)
            {
                db.PHANQUYENs.Add(pHANQUYEN);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pHANQUYEN);
        }

        // GET: Admin/PHANQUYENs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHANQUYEN pHANQUYEN = await db.PHANQUYENs.FindAsync(id);
            if (pHANQUYEN == null)
            {
                return HttpNotFound();
            }
            return View(pHANQUYEN);
        }

        // POST: Admin/PHANQUYENs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MAPHANQUYEN,TENPHANQUYEN,QUYENHAN")] PHANQUYEN pHANQUYEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHANQUYEN).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pHANQUYEN);
        }

        // GET: Admin/PHANQUYENs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHANQUYEN pHANQUYEN = await db.PHANQUYENs.FindAsync(id);
            if (pHANQUYEN == null)
            {
                return HttpNotFound();
            }
            return View(pHANQUYEN);
        }

        // POST: Admin/PHANQUYENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PHANQUYEN pHANQUYEN = await db.PHANQUYENs.FindAsync(id);
            db.PHANQUYENs.Remove(pHANQUYEN);
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
