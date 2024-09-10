using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MuffinApp.Models;

namespace MuffinApp.Controllers
{
    public class OrdersController : Controller
    {
        private MuffinDbcontext db = new MuffinDbcontext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.orders.Include(o => o.MuffinItems);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ItemNo = new SelectList(db.muffinitem, "ItemNo", "MuffinName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderNo,ItemNo,CustomerName,UniversityRole,NumMuffins,BasicCost,DiscountAmt,VatAmt,TotalCost")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                orders.BasicCost = orders.CalBasicCost();
                orders.DiscountAmt = orders.CalcDiscount();
                orders.VatAmt = orders.CalVat();
                orders.TotalCost = orders.CalcTotal();
                db.orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemNo = new SelectList(db.muffinitem, "ItemNo", "MuffinName", orders.ItemNo);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemNo = new SelectList(db.muffinitem, "ItemNo", "MuffinName", orders.ItemNo);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderNo,ItemNo,CustomerName,UniversityRole,NumMuffins,BasicCost,DiscountAmt,VatAmt,TotalCost")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemNo = new SelectList(db.muffinitem, "ItemNo", "MuffinName", orders.ItemNo);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.orders.Find(id);
            db.orders.Remove(orders);
            db.SaveChanges();
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
