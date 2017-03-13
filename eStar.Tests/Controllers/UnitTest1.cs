using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eStar.Models;
using eStar.Controllers;

namespace eStar.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        TesteStarContext context = new TesteStarContext();
        ContextValues setContext = new ContextValues();

        public void SetProductDets(Product product, string name, string description, int price, string img, int category)
        {
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.Image = img;
            product.ProductCategory_ID = category;
        }

        [TestMethod]
        public void AddNewProducts()
        {
            //Arrange
            Product product = new Product();
            ProductsController products = new ProductsController();
            setContext.SetContext(context);
            SetProductDets(product, "Ruler", "30cm plastic ruler", 50, "", 1);

            //Act
            products.Create(product);

            //Assume

        }
    }
}
