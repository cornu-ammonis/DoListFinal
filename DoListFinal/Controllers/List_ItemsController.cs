using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoListFinal.Models;
using Microsoft.AspNet.Identity;

namespace DoListFinal.Controllers
{
    public class List_ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: List_Items
        public ActionResult Index()
        {
            string current_user = User.Identity.GetUserId();
            List<List_Items> list_for_view = new List<List_Items>();
            IEnumerable<List_Items> ListQuery =
                from q in db.List_Items
                where q.User_ID == current_user
                orderby q.priority
                select q;

            foreach (List_Items L in ListQuery)
            {
                list_for_view.Add(L);
            }

            return View(list_for_view);


        }

        // GET: List_Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_Items list_Items = db.List_Items.Find(id);
            if (list_Items == null)
            {
                return HttpNotFound();
            }
            return View(list_Items);
        }

        // GET: List_Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: List_Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,priority")] List_Items list_Items)
        {
            if (ModelState.IsValid)
            {

                list_Items.User_ID = User.Identity.GetUserId();
                db.List_Items.Add(list_Items);
                db.SaveChanges();
                return RedirectToAction("Index");


            }

            return View(list_Items);
        }

        // GET: List_Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_Items list_Items = db.List_Items.Find(id);
            if (list_Items == null)
            {
                return HttpNotFound();
            }
            return View(list_Items);
        }

        // POST: List_Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,priority")] List_Items list_Items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(list_Items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(list_Items);
        }

        // GET: List_Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_Items list_Items = db.List_Items.Find(id);
            if (list_Items == null)
            {
                return HttpNotFound();
            }
            return View(list_Items);
        }

        // POST: List_Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List_Items list_Items = db.List_Items.Find(id);
            db.List_Items.Remove(list_Items);
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
