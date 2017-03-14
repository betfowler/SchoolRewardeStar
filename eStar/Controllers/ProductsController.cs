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

        public ActionResult StoreView()
        {
            var products = db.Products.Include(p => p.ProductCategories);
            return View(products.ToList());
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
            string imagePath = product.Image;

            var path = HttpContext.Server.MapPath(imagePath);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

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
