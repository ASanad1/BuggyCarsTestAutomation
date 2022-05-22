using BuggyCarsTestAutomation.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.Pages
{
    class BasePage
    {
        private readonly DriverHelper _driverHelper;
        private readonly ScenarioContext _scenarioContext;

        public BasePage(DriverHelper driverHelper, ScenarioContext scenarioContext)
        {
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;
        }

        private IWebElement BuggyRatingLogo => _driverHelper.Driver.FindElement(By.XPath("//a[text() = 'Buggy Rating']"));

        public void ClickBuggyRatingLogo()
        {
            BuggyRatingLogo.Click();
        }

        public IWebElement GetDisplayedElement(By locator, int waitTimeInSeconds)
        {
            return new WebDriverWait(_driverHelper.Driver, TimeSpan.FromSeconds(waitTimeInSeconds))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public void VerifyDisplayedMessage(string expectedMessage, IWebElement webElement) =>
            Assert.AreEqual(expectedMessage, webElement.Text.Trim());

        public void ClickElementUsingJS(IWebElement webElement) =>
            ((IJavaScriptExecutor)_driverHelper.Driver).ExecuteScript("arguments[0].click();", webElement);

        public bool IsWebElementPresent(By locator, int waitTimeInSeconds)
        {
            _driverHelper.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(waitTimeInSeconds);
            try
            {
                _driverHelper.Driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException) { return false; }
        }

        public void NavigatePaginationPages(IWebElement previousPageNavigator, IWebElement nextPageNavigator, IWebElement pageNumber)
        {
            Assert.IsTrue(previousPageNavigator.GetAttribute("class").Contains("disabled"));
            var expectedPageNumber = 1;

            while (!nextPageNavigator.GetAttribute("class").Contains("disabled"))
            {
                var pageNumberText = pageNumber.Text.Trim();
                var startingPosition = pageNumberText.LastIndexOf("page") + "page".Length;
                var length = pageNumberText.LastIndexOf("of") - startingPosition;
                var currentPageNumber = int.Parse(pageNumberText.Substring(startingPosition, length).Trim());

                Assert.AreEqual(expectedPageNumber, currentPageNumber);
                nextPageNavigator.Click();
                expectedPageNumber++;
            }
        }
    }
}
