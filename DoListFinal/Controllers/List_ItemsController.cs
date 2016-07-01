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

            List<List_Items> display_list = new List<List_Items>();
            List<UserBank> indexbanklist = new List<UserBank>();
            indexbanklist = db.UserBanks.ToList();

            //checks if current user has an associated object in the database which stores indexes for that user's  to do list items; displays those items if found
            foreach (UserBank indexbank in indexbanklist )
            {
                if (indexbank.AppUserID == User.Identity.GetUserId())
                {
                    //indexes are stored as numbers in a string separated by commas -- creates array of strings each containing a single index
                    string[] index_array_strings = indexbank.Item_indexes.Split(',');
                    foreach (string ind in index_array_strings)
                    {
                        int ident;
                        if (Int32.TryParse(ind, out ident))
                        {
                            //if list item corresponding to index is found, adds that item to the list of items which is later passed to view 
                            if (db.List_Items.Find(ident) != null)
                            {
                                List_Items item_to_add = db.List_Items.Find(ident);
                                display_list.Add(item_to_add);
                            }
                        }


                    }
                }
            }
            //sorts list items in order of priority 
            List<List_Items> sortedlist = display_list.OrderBy(o => o.priority).ToList();
           
            return View(sortedlist);
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

                //pulls all stored index objects from database and if one matches current user, updates the list of indexes in that object upon list item creation
                List<UserBank> banklist = new List<UserBank>();
                banklist = db.UserBanks.ToList();
                foreach (UserBank u in banklist)
                {
                    if (u.AppUserID == User.Identity.GetUserId())
                    {
                        db.List_Items.Add(list_Items);
                        db.SaveChanges();

                        //this is important because saving the context to the database may change the list item's primary key value

                        int ind = db.Entry(list_Items).CurrentValues.GetValue<int>("ID");
                        u.Item_indexes = u.Item_indexes + ind.ToString() + ",";
                        db.Entry(u).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                   
                       
                    
                }

                //this code is only reached if no existing userbank object is found which matches current user ID (i.e. when new user creates list item for the first time).
                //creates new userbank object and stores the new list item's index.
                db.List_Items.Add(list_Items);
                db.SaveChanges();
                UserBank newbank = new UserBank();
                newbank.AppUserID = User.Identity.GetUserId();
                newbank.Item_indexes = "";

                //this is important because saving the context to the database may change the list item's primary key value

                int indd = db.Entry(list_Items).CurrentValues.GetValue<int>("ID");
                newbank.Item_indexes = newbank.Item_indexes + indd.ToString() + ",";
                db.UserBanks.Add(newbank);
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
