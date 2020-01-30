using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeastFreedom03.Models;
using FeastFreedom03.Views.ViewModels;


namespace FeastFreedom03.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            FeastFreedomEntities db = new FeastFreedomEntities();

            List<OrderViewModel> orderViewModel = new List<OrderViewModel>();

            /* var shoppingCart = ( from item in db.Items
                                  join details in db.OrderDetails on 
              )*/
            return View();
        }

        [HttpGet]
        public ActionResult SelectKitchen()
        {
            FeastFreedomEntities db = new FeastFreedomEntities();
            List<Kitchen> kitchenList = new List<Kitchen>();
            foreach (Kitchen kitchen in db.Kitchens)
            {
                kitchenList.Add(kitchen);
            }
            return View(kitchenList);
        }

        [HttpGet]
        public ActionResult OrderPage(int? kitchenID)
        {
            if (kitchenID != null)
            {
                FeastFreedomEntities context = new FeastFreedomEntities();
                List<OrderViewModel> orderVModels = new List<OrderViewModel>();

                var itemList = (from i in context.Items
                                join k in context.Kitchens
                                on i.KitchenID equals kitchenID
                                select new { i.ItemName, i.ItemPrice, i.Kitchen.KitchenName, i.ItemID, i.isVeg }).ToList().Distinct(); //.Distinct() prevemts rows from printing multiple times, for some reason. 

                foreach (var item in itemList)
                {
                    OrderViewModel ovm = new OrderViewModel();
                    ovm.ItemID = item.ItemID;
                    ovm.ItemName = item.ItemName;
                    ovm.ItemPrice = item.ItemPrice;
                    ovm.isVeg = item.isVeg;
                    ovm.Quantity = 0;
                    ovm.KitchenName = item.KitchenName;
                    orderVModels.Add(ovm);         
                }
                return View (orderVModels);
            }
            else return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderPage(List<OrderViewModel> _orderViewModels)
        {
            
            
            if (ModelState.IsValid)
            {
                FeastFreedomEntities db = new FeastFreedomEntities();
                List<OrderDetail> detailsList = new List<OrderDetail>();

                foreach (var item in _orderViewModels)
                {
                    if (item.Quantity > 0)
                    {
                        OrderDetail details = new OrderDetail();
                        details.ItemID = item.ItemID;
                        details.ItemTotal = item.ItemPrice * item.Quantity;
                        details.ItemName = item.ItemName;
                        
                        detailsList.Add(details);
                        db.OrderDetails.Add(details);
                        db.SaveChanges();

                    }
                }
                return RedirectToAction("OrderSummary", detailsList);
            }

            return View();

        }

        public ActionResult OrderComplete()
        {

            return View();
        }























        public ActionResult OrderSummary(List<OrderDetail> detailsList)
        {
            FeastFreedomEntities db = new FeastFreedomEntities();


            return View(detailsList);
        }





        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderPage(OrderViewModel orderViewModel)
        {
            FeastFreedomEntities1 db = new FeastFreedomEntities1();
            //OrderViewModel orderViewModel = new OrderViewModel();
            List<OrderViewModel> orderVModels = new List<OrderViewModel>();
            List<OrderDetail> detailsList = new List<OrderDetail>();

            











            /*
            if (ModelState.IsValid)
            {
                foreach (var item in orderViewModels)
                {
                    OrderDetail details = new OrderDetail();
                    if (item.Quantity > 0)
                    {
                        details.ItemID = item.ItemID;
                        details.ItemTotal = item.ItemPrice * item.Quantity;
                        detailsList.Add(details);
                    }
                }
                return RedirectToAction("OrderSummary", detailsList);
            }
             return RedirectToAction("OrderSummary");
        }
        */

        /*
                   foreach (string key in form.AllKeys)
                   {
                       OrderDetail details = new OrderDetail();
                       ViewBag.ItemName = form["ItemName"];
                       details.ItemName = @ViewBag.ItemName;
                       detailsList.Add(details);

                   }*/












    }
} 