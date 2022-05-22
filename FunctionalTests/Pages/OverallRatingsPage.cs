using BuggyCarsTestAutomation.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.Pages
{
    class OverallRatingsPage : BasePage
    {
        private readonly DriverHelper _driverHelper;
        private readonly ScenarioContext _scenarioContext;

        public OverallRatingsPage(DriverHelper driverHelper, ScenarioContext scenarioContext) : base(driverHelper, scenarioContext)
        {
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;
        }

        private IWebElement PreviousPageNavigator => _driverHelper.Driver.FindElement(By.LinkText("«"));
        private IWebElement NextPageNavigator => _driverHelper.Driver.FindElement(By.LinkText("»"));
        private IWebElement PageNumber => _driverHelper.Driver.FindElement(By.XPath("//div[@class = 'row']//div[@class = 'pull-xs-right']"));

        public void NavigatePaginationPages() => NavigatePaginationPages(PreviousPageNavigator, NextPageNavigator, PageNumber);

        public void SelectCarMakeAndModel(string make, string model)
        {
            var carModelLocator = By.XPath($"//a[text() = '{make}']/../..//a[text() = '{model}']");
            for (int pageNumber = 1; pageNumber <= 5; pageNumber++)
            {
                if (IsWebElementPresent(carModelLocator, 5))
                {
                    _driverHelper.Driver.FindElement(carModelLocator).Click();
                    break;
                }
                else
                {
                    NextPageNavigator.Click();
                }
            }
        }

        public void VerifyMakeAndModelPageIsDisplayed(string make, string model)
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(_driverHelper.Driver.FindElement(By.XPath($"//h4[text() = '{make}']")).Displayed);
                Assert.IsTrue(_driverHelper.Driver.FindElement(By.XPath($"//h3[text() = '{model}']")).Displayed);
            });
        }
    }
}
