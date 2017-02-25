using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eStar.Controllers;
using eStar.Models;

namespace eStar.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void ShouldBeAbleToCountNumberOfLettersInSimpleSentence()
        {
            var sentenceToScan = "TDD is awesome!";
            var characterToScanFor = "e";
            var expectedResult = 2;
            var stringUtils = new StringUtils();
            int result = stringUtils.FindNumberOfOccurences(sentenceToScan, characterToScanFor);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ShouldBeAbleToCountNumberOfLettersInAComplexSentence()
        {
            var sentenceToScan = "Once is unique, twice is a coincidence, three times is a pattern";
            var characterToScanFor = "n";
            var expectedResult = 4;
            var stringUtils = new StringUtils();
            AccountController controller = new AccountController();

            int result = stringUtils.FindNumberOfOccurences(sentenceToScan, characterToScanFor);
            Assert.AreEqual(expectedResult, result);
        }

    }
}
