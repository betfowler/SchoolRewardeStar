using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eStar.Models;
using System.IO;
using eStar.Security;

namespace eStar.Controllers
{
    public class ProductsController : Controller
    {
        private eStarContext db = new eStarContext();
        //add constructors
        /*public ProductsController() { }
        public ProductsController(IeStarContext context)
        {
            db = context;
        }*/

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductCategories);
            return View(products.ToList());
        }
        public Product find(int productID)
        {
            return db.Products.Where(pr => pr.Product_ID.Equals(productID)).FirstOrDefault();
        }

        //**VIEWING ITEMS IN BASKET**//
        public ActionResult BasketView(string message, int? productID)
        {
            Order order = db.Orders.Where(or => or.OrderStatus_ID.Equals(5) && or.User_ID.Equals(SessionPersister.UserID)).FirstOrDefault();

            if (message == "Fail")
            {
                ViewBag.Error = "Ooops! You don't have enough points.  This order costs " + order.TotalCost + " points and you have " + SessionPersister.Balance;
                message = "";
            }
            if (message == "Remove")
            {
                int id = Convert.ToInt32(productID);
                string productName = db.Products.Find(id).Name;
                ViewBag.Success = "Item: " + productName + " has been removed from you basket.";
                message = "";
            }
            if (message == "Success")
            {
                ViewBag.Success = "Your order has been made.";
                message = "";
            }

            if (order == null)
            {
                ViewBag.Empty = "You have no items in your basket.  <a href='../Products/StoreView'>Click here to continue shopping.</a>";
                return View();
            }
            else
            {
                order.Products = new List<ProductOrder>();
                foreach(var prodorder in db.ProductOrders.Where(po => po.Order_ID.Equals(order.Order_ID)).ToList())
                {
                    order.Products.Add(prodorder);
                }
                return View(order);
            }
        }

        //**REMOVE ORDER AND ALL PRODUCTORDERS**//
        public ActionResult RemoveAll(int orderID)
        {
            Order order = db.Orders.Find(orderID);

            foreach(ProductOrder productOrder in db.ProductOrders.Where(po => po.Order_ID.Equals(orderID)).ToList())
            {
                db.ProductOrders.Remove(productOrder);
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return RedirectToAction("BasketView");
        }

        //**REMOVING ITEM FROM BASKET**//
        public ActionResult RemoveItem(int productOrderID, int orderID)
        {
            ProductOrder productOrder = db.ProductOrders.Find(productOrderID);
            Order order = db.Orders.Find(orderID);
            int productID = productOrder.Product_ID;
            int productCost = productOrder.ProductPrice;
            db.ProductOrders.Remove(productOrder); //remove productOrder
            order.ProductCount = order.ProductCount - 1; //change product Count
            order.TotalCost = order.TotalCost - productCost;
            db.SaveChanges();

            SessionPersister.Basket = order.ProductCount;

            if(order.ProductCount == 0)//remove order
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }

            return RedirectToAction("BasketView", new { message = "Remove", productID = productID});
        }

        //**PURCHASE ITEMS**//
        public ActionResult PurchaseItems(int orderID)
        {
            Order order = db.Orders.Find(orderID);
            int balance = db.Accounts.OfType<Student>().Where(ac => ac.User_ID.Equals(SessionPersister.UserID)).FirstOrDefault().Balance;
            //if balance too low
            if(order.TotalCost > balance)
            {
                return RedirectToAction("BasketView", new { message = "Fail" });
            }
            else
            {
                order.OrderStatus_ID = 1; //pending
                order.OrderDate = DateTime.Today;
                Student student = db.Accounts.OfType<Student>().Where(ac => ac.User_ID.Equals(SessionPersister.UserID)).FirstOrDefault();
                student.Balance = balance - order.TotalCost;
                db.SaveChanges();

                SessionPersister.Balance = student.Balance;
                SessionPersister.Basket = 0;
                return RedirectToAction("BasketView", new { message = "Success" });
            }
        }

        public ActionResult AddItem(int productID)
        {
            if(SessionPersister.UserType == "Admin")
            {
                return RedirectToAction("StoreView");
            }

            int price = find(productID).Price;

            //check active order exists
            Order order = db.Orders.Where(or => or.OrderStatus_ID.Equals(5) && or.User_ID.Equals(SessionPersister.UserID)).FirstOrDefault();
            ProductOrder prodOrder = new ProductOrder();

            if (order == null)
            {
                order = new Order();
                order.User_ID = SessionPersister.UserID;
                order.OrderStatus_ID = 5; //5 = "Active"
                order.ProductCount = 1;
                order.TotalCost = price;
                db.Orders.Add(order);
                db.SaveChanges();
            }
            else
            {
                order.ProductCount = order.ProductCount + 1;
                order.TotalCost = order.TotalCost + price;
            }

            //set productorder
            prodOrder.Order_ID = order.Order_ID;
            prodOrder.Product_ID = productID;
            prodOrder.ProductName = find(productID).Name;
            prodOrder.ProductDesc = find(productID).Description;
            int prodCatID = find(productID).ProductCategory_ID;
            prodOrder.ProductCategory = db.ProductCategories.Where(pc => pc.ProductCategory_ID.Equals(prodCatID)).FirstOrDefault().CategoryName;
            prodOrder.ProductPrice = find(productID).Price;
            db.ProductOrders.Add(prodOrder);

            db.SaveChanges();

            SessionPersister.Basket = order.ProductCount;

            return RedirectToAction("StoreView", new { productID = productID, addMessage = "Success" });
        }


        //**VIEW ITEMS IN STORE**//
        public ActionResult StoreView(string sortOrder, string searchString, int? min, int? max, string ProductCategory_ID, int? productID, string addMessage)
        {
            if(addMessage == "Success")
            {
                int productid = Convert.ToInt32(productID);
                string name = db.Products.Where(pr => pr.Product_ID.Equals(productid)).FirstOrDefault().Name.ToString();
                ViewBag.Success = name + " has been added to your basket. <a href='../Products/BasketView' class='alert-link'>View your basket</a>";
                addMessage = "";
                productID = null;
            }

            List<Product> products = new List<Product>();
            ViewBag.Search = searchString;
            ViewBag.min = min;
            ViewBag.max = max;
            int number;
            ViewBag.ProductCategory_ID = new SelectList(db.ProductCategories, "ProductCategory_ID", "CategoryName");
            
            ViewBag.PriceParm = String.IsNullOrEmpty(sortOrder) ? "Price_desc" : "";

            //max must be bigger than min
            if (max < min)
            {
                min = 0;
                max = 1000;
                ViewBag.min = "0";
                ViewBag.max = "1000";
                ViewBag.Error = "Cost min must be lower than cost max";
            }
            //set ViewBag
            if(min == null)
            {
                ViewBag.min = "0";
            }
            if(max == null)
            {
                ViewBag.max = "1000";
            }

            products = db.Products.Include(p => p.ProductCategories).ToList();

            if (min != null && max != null)
            {
                products = products.Where(pr => pr.Price >= min && pr.Price <= max).ToList();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToUpper();

                products = products.Where(pr => pr.Name.ToUpper().Contains(search) || pr.Description.ToUpper().Contains(search)).ToList();
            }

            if (int.TryParse(ProductCategory_ID, out number))
            {
                int prodCat = Convert.ToInt32(ProductCategory_ID);
                products = products.Where(pr => pr.ProductCategory_ID.Equals(prodCat)).ToList();
            }

            switch (sortOrder)
            {
                case "Price_desc":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
            }

            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            PopulateCategoryDropDownList();

            ViewBag.ProductCategory_ID = new SelectList(db.ProductCategories, "ProductCategory_ID", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_ID,Name,Description,Price,Image,ProductCategory_ID")] Product product)
        {
            if (ModelState.IsValid)
            {
                if(Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if(file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("../Content/Images/ProductImages"), fileName);

                        //if file doesn't already exist
                        string pathToString = path.ToString();
                        string imageLocation = "\\Content\\Images\\ProductImages";
                        int pathIndex = pathToString.IndexOf(imageLocation);
                        string imagePathToSave = pathToString.Substring(pathIndex);

                        if (db.Products.Where(pr => pr.Image.Equals(imagePathToSave)).FirstOrDefault() != null)
                        {
                            ViewBag.Error = "An image with this name already exists.";
                            return View("Create");
                        }

                        product.Image = imagePathToSave;
                        file.SaveAs(path);

                    }
                }

                //if no image selected
                else
                {
                    product.Image = "/Content/Images/Store.png";
                }


                PopulateCategoryDropDownList(product.ProductCategory_ID);
                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ProductCategory_ID = new SelectList(db.ProductCategories, "ProductCategory_ID", "CategoryName", product.ProductCategory_ID);
            return View(product);
        }

        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var query = from r in db.ProductCategories
                        orderby r.CategoryName
                        select r;

            ViewBag.ProductCategory_ID = new SelectList(query, "ProductCategory_ID", "CategoryName", selectedCategory);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategory_ID = new SelectList(db.ProductCategories, "ProductCategory_ID", "CategoryName", product.ProductCategory_ID);
            ViewBag.ImagePath = product.Image;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_ID,Name,Description,Price,Image,ProductCategory_ID")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("../../Content/Images/ProductImages"), fileName);

                        string pathToString = path.ToString();
                        string imageLocation = "\\Content\\Images\\ProductImages";
                        int pathIndex = pathToString.IndexOf(imageLocation);
                        string imagePathToSave = pathToString.Substring(pathIndex);

                        product.Image = imagePathToSave;
                        file.SaveAs(path);
                    }
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategory_ID = new SelectList(db.ProductCategories, "ProductCategory_ID", "CategoryName", product.ProductCategory_ID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
