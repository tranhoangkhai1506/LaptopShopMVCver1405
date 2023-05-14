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
    public class THUONGHIEUxController : Controller
    {
        private LaptopDBContext db = new LaptopDBContext();

        // GET: Admin/THUONGHIEUx
        public async Task<ActionResult> Index()
        {
            return View(await db.THUONGHIEUx.ToListAsync());
        }

        // GET: Admin/THUONGHIEUx/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUONGHIEU tHUONGHIEU = await db.THUONGHIEUx.FindAsync(id);
            if (tHUONGHIEU == null)
            {
                return HttpNotFound();
            }
            return View(tHUONGHIEU);
        }

        // GET: Admin/THUONGHIEUx/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/THUONGHIEUx/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MATHUONGHIEU,TENTHUONGHIEU,XUATXU")] THUONGHIEU tHUONGHIEU)
        {
            if (ModelState.IsValid)
            {
                db.THUONGHIEUx.Add(tHUONGHIEU);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tHUONGHIEU);
        }

        // GET: Admin/THUONGHIEUx/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUONGHIEU tHUONGHIEU = await db.THUONGHIEUx.FindAsync(id);
            if (tHUONGHIEU == null)
            {
                return HttpNotFound();
            }
            return View(tHUONGHIEU);
        }

        // POST: Admin/THUONGHIEUx/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MATHUONGHIEU,TENTHUONGHIEU,XUATXU")] THUONGHIEU tHUONGHIEU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHUONGHIEU).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tHUONGHIEU);
        }

        // GET: Admin/THUONGHIEUx/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUONGHIEU tHUONGHIEU = await db.THUONGHIEUx.FindAsync(id);
            if (tHUONGHIEU == null)
            {
                return HttpNotFound();
            }
            return View(tHUONGHIEU);
        }

        // POST: Admin/THUONGHIEUx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            THUONGHIEU tHUONGHIEU = await db.THUONGHIEUx.FindAsync(id);
            db.THUONGHIEUx.Remove(tHUONGHIEU);
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
