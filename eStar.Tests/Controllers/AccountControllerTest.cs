using System;
using Rhino.Mocks;
using System.Linq;
using Rhino.Mocks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eStar.Controllers;
using eStar.ViewModels;
using eStar.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Web;

namespace eStar.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
     /*   TesteStarContext context = new TesteStarContext();
        AccountViewModel accountViewModel = new AccountViewModel();
        ContextValues setContext = new ContextValues();
        Account account = new Account();


        public void SetAccountDets(Account account, string email, string password)
        {
            account.Email = email;
            account.Password = password;
            accountViewModel.Account = account;
        }

        [TestMethod]
        public void ViewBagErrorIfLogInDetailsAreNull()
        {
            //Arrange
            AccountController controller = new AccountController(context);
            AccountModel accountModel = new AccountModel(context);
            setContext.SetContext(context);
            SetAccountDets(account, "", "");

            //Act
            controller.Login(accountViewModel, accountModel);

            //Assert
            Assert.AreEqual("Log in details invalid", controller.ViewBag.Error);
        }

        [TestMethod]
        public void ViewBagErrorIfEmailDetailsAreNull()
        {
            //Arrange
            AccountController controller = new AccountController(context);
            AccountModel accountModel = new AccountModel(context);
            setContext.SetContext(context);
            SetAccountDets(account, "", "test");

            //Act
            controller.Login(accountViewModel, accountModel);

            //Assert
            Assert.AreEqual("Log in details invalid", controller.ViewBag.Error);

        }

        [TestMethod]
        public void ViewBagErrorIfPasswordDetailsAreNull()
        {
            //Arrange
            AccountController controller = new AccountController(context);
            AccountModel accountModel = new AccountModel(context);
            setContext.SetContext(context);
            SetAccountDets(account, "student@test.com", "");

            //Act
            controller.Login(accountViewModel, accountModel);

            //Assert
            Assert.AreEqual("Log in details invalid", controller.ViewBag.Error);
        }

        [TestMethod]
        public void ViewBagErrorIfPasswordIncorrect()
        {
            //Arrange
            AccountController controller = new AccountController(context);
            AccountModel accountModel = new AccountModel(context);
            setContext.SetContext(context);
            SetAccountDets(account, "student@test.com", "wrong");

            //Act
            controller.Login(accountViewModel, accountModel);

            //Assert
            Assert.AreEqual("Log in details invalid", controller.ViewBag.Error);
        }
        
        [TestMethod]
        public void CorrectLoginDetails()
        {
            //Arrange
            AccountController controller = new AccountController(context);
            AccountModel accountModel = new AccountModel(context);

            setContext.SetContext(context);
            SetAccountDets(account, "student@test.com", "test");

            //Act
            controller.Login(accountViewModel, accountModel);

            //Assert
            
            Assert.AreEqual("Yes!", controller.ViewBag.Success);
        }   */
    }
}
