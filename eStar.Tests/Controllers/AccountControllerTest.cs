using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eStar.Controllers;
using eStar.ViewModels;
using eStar.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace eStar.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        public void SetAccountDets(Account account, string email, string password)
        {
            account.Email = email;
            account.Password = password;
        }

        [TestMethod]
        public void ViewBagErrorIfLogInDetailsAreNull()
        {
            //Arrange
            AccountController accountController = new AccountController();
            AccountModel accountModel = new AccountModel();
            AccountViewModel accountViewModel = new AccountViewModel();
            var account = new Account();
            SetAccountDets(account, "", "");

            //Act
            accountViewModel.Account = account;
            accountController.Login(accountViewModel, accountModel);

            //Assert
            Assert.AreEqual("Log in details invalid", accountController.ViewBag.Error);
        }

        [TestMethod]
        public void ViewBagErrorIfEmailDetailsAreNull()
        {
            //Arrange
            AccountController accountController = new AccountController();
            AccountModel accountModel = new AccountModel();
            AccountViewModel accountViewModel = new AccountViewModel();
            var account = new Account();
            SetAccountDets(account, "", "test");

            //Act
            accountViewModel.Account = account;
            accountController.Login(accountViewModel, accountModel);

            //Assert
            Assert.AreEqual("Log in details invalid", accountController.ViewBag.Error);
        }

        [TestMethod]
        public void ViewBagErrorIfPasswordDetailsAreNull()
        {
            //Arrange
            AccountController accountController = new AccountController();
            AccountModel accountModel = new AccountModel();
            AccountViewModel accountViewModel = new AccountViewModel();
            var account = new Account();
            SetAccountDets(account, "bet@test.com", "");

            //Act
            accountViewModel.Account = account;
            accountController.Login(accountViewModel, accountModel);

            //Assert
            Assert.AreEqual("Log in details invalid", accountController.ViewBag.Error);
        }

        [TestMethod]
        public void ViewBagErrorIfPasswordIncorrect()
        {
            //Arrange
            //var db = new FakeeStarContextDb();
            /*db.AddSet(TestData.Accounts);
            AccountController accountController = new AccountController(db);
            AccountModel accountModel = new AccountModel(db);
            AccountViewModel accountViewModel = new AccountViewModel();

            var account = new Account();
            SetAccountDets(account, "bet@test.com", "wrong");

            //Act
            accountViewModel.Account = account;
            accountController.Login(accountViewModel, accountModel);

            //Assert
            Assert.AreEqual("Log in details invalid", accountController.ViewBag.Error);*/
        }        
    }
}
