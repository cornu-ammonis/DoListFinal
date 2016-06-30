using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoListFinal.Models;

namespace DoListFinal.Controllers
{
    public class UserBanksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserBanks
        public ActionResult Index()
        {
            return View(db.UserBanks.ToList());
        }

        // GET: UserBanks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBank userBank = db.UserBanks.Find(id);
            if (userBank == null)
            {
                return HttpNotFound();
            }
            return View(userBank);
        }

        // GET: UserBanks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Item_indexes")] UserBank userBank)
        {
            if (ModelState.IsValid)
            {
                db.UserBanks.Add(userBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userBank);
        }

        // GET: UserBanks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBank userBank = db.UserBanks.Find(id);
            if (userBank == null)
            {
                return HttpNotFound();
            }
            return View(userBank);
        }

        // POST: UserBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Item_indexes")] UserBank userBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userBank);
        }

        // GET: UserBanks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBank userBank = db.UserBanks.Find(id);
            if (userBank == null)
            {
                return HttpNotFound();
            }
            return View(userBank);
        }

        // POST: UserBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserBank userBank = db.UserBanks.Find(id);
            db.UserBanks.Remove(userBank);
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
