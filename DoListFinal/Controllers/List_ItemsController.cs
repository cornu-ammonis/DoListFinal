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
            ViewModel view_model = new ViewModel();

           List<Uncompleted_List_Item> uncompleted_list_for_view = new List<Uncompleted_List_Item>();
           List<Completed_List_Items> completed_list_for_view = new List<Completed_List_Items>();

            IEnumerable<Uncompleted_List_Item> ListQuery =
                from q in db.Uncompleted_List_Items
                where q.User_ID == current_user
                orderby q.priority
                select q;

            foreach (Uncompleted_List_Item L in ListQuery)
            {
                uncompleted_list_for_view.Add(L);
            }

            IEnumerable<Completed_List_Items> CompletedListQuery =
                   from q in db.Completed_List_Items
                   where q.User_ID == current_user
                   orderby q.priority
                   select q;

            
            foreach (Completed_List_Items L in CompletedListQuery)
            {
                
                completed_list_for_view.Add(L);
            }

            view_model.uncompleted_items = uncompleted_list_for_view;
            view_model.completed_items = completed_list_for_view;
            return View(view_model);


        }


       public ActionResult toggle(int? id)
        {
           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var list_item = db.List_Items.Find(id);

            if (list_item == null)
            {
                return HttpNotFound();
            }

            /* if(db.List_Items.Find(id).is_complete == true)
            {
                Completed_List_Items list_item = db.Completed_List_Items.Find(id);
                db = list_item.toggle_complete(db);
                db.SaveChanges();
            }
            else if (db.List_Items.Find(id).is_complete == false)
            {
                Uncompleted_List_Item list_item = db.Uncompleted_List_Items.Find(id);
                db = list_item.toggle_complete(db);
                db.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            } */

            db = list_item.toggle_complete(db);
            db.SaveChanges();

            return RedirectToAction("Index");
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
        public ActionResult Create([Bind(Include = "ID,Description,priority")] Uncompleted_List_Item list_Items)
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
        public ActionResult Edit([Bind(Include = "ID,Description,priority,is_complete")] List_Items list_Items)
        {
            if (ModelState.IsValid)
            {
                list_Items.User_ID = User.Identity.GetUserId();
                /*   if (list_Items.is_complete == true)
                   {
                       Completed_List_Items complete_edited = new Completed_List_Items(list_Items.Description, list_Items.priority, list_Items.User_ID);
                       List_Items to_remove = db.List_Items.Find(list_Items.ID);
                       db.List_Items.Remove(to_remove);
                       db.Completed_List_Items.Add(complete_edited);
                       db.SaveChanges();
                       return RedirectToAction("Index");
                   }
                   else if (list_Items.is_complete == false)
                   {
                       Uncompleted_List_Item uncomplete_edited = new Uncompleted_List_Item(list_Items.Description, list_Items.priority, list_Items.User_ID);
                       List_Items to_remove = db.List_Items.Find(list_Items.ID);
                       db.List_Items.Remove(to_remove);
                       db.Uncompleted_List_Items.Add(uncomplete_edited);
                       db.SaveChanges();
                       return RedirectToAction("Index");

                   }

                   return HttpNotFound();
                   */
                db = list_Items.implement_edit(db);
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
