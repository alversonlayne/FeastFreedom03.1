﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FeastFreedom03.Models;


namespace FeastFreedom03.Controllers
{
    public class KitchenController : Controller
    {
        private FeastFreedomEntities db = new FeastFreedomEntities();

        // GET: Kitchen
        public ActionResult Index()
        {
            ViewBag.Message = "WAAAAAAUUW! LOOK AT ALL THAT";
            @TempData["KitchenID"] = null; //Once the user returns to this page, clear the TempData
            return View(db.Kitchens.ToList());
            
        }




        // GET: Kitchen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            return View(kitchen);
        }




        // GET: Kitchen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kitchen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KitchenID,KitchenName,KitchenEmail,KitchenPassword")] Kitchen kitchen) //Still unsure what exactly this does
        {
            if (ModelState.IsValid)
            {
                db.Kitchens.Add(kitchen); //If the form looks good, add the basic user info to database.
                db.SaveChanges();
                TempData["KitchenID"] = kitchen.KitchenID;  //Store auto-generated KitchenID in TempData. This will be passed to other methods.
                return RedirectToAction("EditHours");
            }
            return View(kitchen);
        }




        // GET: Kitchen/Edit/5
        public ActionResult Edit(int? id) //You can't edit a Kitchen's info unless you know it's corresponding KitchenID number
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            return View(kitchen);
        }

        // POST: Kitchen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KitchenID,KitchenName,KitchenEmail,KitchenPassword")] Kitchen kitchen) //What does this do?
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitchen).State = EntityState.Modified; //If there is a change, Save it. If there isn't a change, there's nothing new to save.
                db.SaveChanges();
                return RedirectToAction("Kitchens");
            }
            
            return View(kitchen);
        }




        // GET: Kitchen/Delete/5
        public ActionResult Delete(int? id) //You need to know a Kitchen's KitchenID if you want to delete it.
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            return View(kitchen);
        }
        [HttpGet]
        public ActionResult EditHours()
        {
            TempData.Keep(); //We are holding the auto-generated KitchenID in TempData. 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHours(KitchenHOOp kitchenHours)
        {
            TempData.Keep();
            if (ModelState.IsValid)
            {
                kitchenHours.KitchenID = (int) @TempData["KitchenID"]; //Assign the carried-over KitchenID to THIS PARTICULAR INSTANCE of a KitchenHOOp Object/Row.
                db.KitchenHOOps.Add(kitchenHours);      //We have to know to which KitchenID we are assigning these hours. If we don't know the ID, we don't know which Kitchen we're talking about.
                db.SaveChanges();
                return RedirectToAction("EditMenu", TempData["KitchenID"]);
            }
            return RedirectToAction("EditMenu", TempData["KitchenID"]);
        }

        public ActionResult ListHours() //List all the data in our KitchenHOOp (Hours of Operation) database.
        {
            List<KitchenHOOp> hoursOfOperation = db.KitchenHOOps.ToList();
            return View(hoursOfOperation);
        }

        //henlo

        [HttpGet]
        public ActionResult EditMenu() //Let a Kitchen add food items to its menu. For the sake of simplicity, each Kitchen has one menu.
        {
            TempData.Keep();        //Hold on to TempData["KitchenID"] for now.
            return View();
        }

        [HttpPost]
        public ActionResult EditMenu(Item item)
        {
            if (ModelState.IsValid)
            {
                TempData.Keep();
                item.KitchenID = (int)@TempData["KitchenID"]; //Assign the KitchenID held in TempData to THIS PARTICULAR INSTANCE of an Item Object/Row.
                db.Items.Add(item);                 //If you don't know the item's KitchenID, you don't know which Kitchen sells it! The KitchenID is a foreign key.
                db.SaveChanges();                   //For now, TempData will be set to null once the user navigates back to the Kitchen/Index view. See above Index Action Method.
                
            }
            return RedirectToAction("EditMenu", TempData["KitchenID"]);
        }




        // POST: Kitchen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitchen kitchen = db.Kitchens.Find(id);
            db.Kitchens.Remove(kitchen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public ActionResult Items() //This list every item sold by every restaurant. Given more time, we could implement more sophisticated data structures.
        {
            List<Item> menuItems = db.Items.ToList();
            return View(menuItems);
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
