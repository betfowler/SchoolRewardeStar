using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eStar.Models;
using eStar.Security;

namespace eStar.Controllers
{
    public class OrdersController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Orders
        public ActionResult Index(string sortOrder, string searchString, string statusRadio, string ownerRadio)
        {

            ViewBag.Search = searchString;
            ViewBag.DateParm = String.IsNullOrEmpty(sortOrder) ? "Date_desc" : "";

            var orders = db.Orders.Where(or => or.OrderStatus_ID.Equals(1) || or.OrderStatus_ID.Equals(2)).Include  (o => o.OrderStatus).ToList();
            if(orders == null)
            {
                ViewBag.Empty = "There are no pending orders";
                return View();
            }

            foreach(var productOrder in orders)
            {
                productOrder.ProductOrders = db.ProductOrders.Where(po => po.Order_ID.Equals(productOrder.Order_ID)).ToList();
                if(productOrder.Admin == null)
                {
                    productOrder.Admin = "Unassigned";
                }
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToUpper();
                orders = orders.Where(s => s.Order_ID.ToString().ToUpper().Contains(search) || s.User_ID.ToString().ToUpper().Contains(search)).ToList();

            }

            //order status radio buttons
            if (statusRadio == "1")
            {
                ViewBag.Pending = "checked";
                orders = orders.Where(or => or.OrderStatus_ID.Equals(1)).ToList();
            }
            else if(statusRadio == "2")
            {
                ViewBag.InProgress = "checked";
                orders = orders.Where(or => or.OrderStatus_ID.Equals(2)).ToList();
            }
            else
            {
                ViewBag.All = "checked";
            }

            //order 'owner' radio buttons
            if(ownerRadio == "myOrders")
            {
                ViewBag.myOrders = "checked";
                orders = orders.Where(or => or.Admin.Equals(SessionPersister.Username)).ToList();
            }
            else if(ownerRadio == "unassignedOrders"){
                ViewBag.unassignedOrders = "checked";
                orders = orders.Where(or => or.Admin.Equals("Unassigned") || or.Admin.Equals(null)).ToList();
            }
            else{
                ViewBag.allOrders = "checked";
            }

            switch (sortOrder)
            {
                case "Date_desc":
                    orders = orders.OrderByDescending(o => o.OrderDate).ToList();
                    break;
                default:
                    orders = orders.OrderBy(o => o.OrderDate).ToList();
                    break;
            }

            return View(orders);
        }

        public ActionResult AcceptOrder(int orderID)
        {
            Order order = db.Orders.Find(orderID);
            order.OrderStatus_ID = 2;
            order.Admin = db.Accounts.Find(SessionPersister.UserID).FullName;
            db.SaveChanges();
            SessionPersister.Orders = db.Orders.Where(or => or.OrderStatus_ID.Equals(1) || or.OrderStatus_ID.Equals(2)).Count();
            return RedirectToAction("Index");
        }

        public ActionResult CompleteOrder(int orderID)
        {
            Order order = db.Orders.Find(orderID);
            order.OrderStatus_ID = 3;
            db.SaveChanges();
            SessionPersister.Orders = db.Orders.Where(or => or.OrderStatus_ID.Equals(1) || or.OrderStatus_ID.Equals(2)).Count();
            return RedirectToAction("Index");
        }

        public ActionResult OrderView()
        {
            var orders = db.Orders.Where(or => or.User_ID.Equals(SessionPersister.UserID) && or.OrderStatus_ID != 5).Include(or => or.OrderStatus).ToList();
            if(orders == null)
            {
                ViewBag.Empty = "You haven't made any orders.  Browse the eStore <a href = '../Products/StoreView'>here</a>.";
                return View();
            }

            foreach(var productOrder in orders)
            {
                productOrder.ProductOrders = db.ProductOrders.Where(po => po.Order_ID.Equals(productOrder.Order_ID)).ToList();
            }

            orders = orders.OrderByDescending(or => or.OrderDate).ToList();

            return View(orders);
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
