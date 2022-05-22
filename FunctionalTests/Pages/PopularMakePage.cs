using BuggyCarsTestAutomation.Core;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.Pages
{
    class PopularMakePage : BasePage
    {
        private readonly DriverHelper _driverHelper;
        private readonly ScenarioContext _scenarioContext;

        public PopularMakePage(DriverHelper driverHelper, ScenarioContext scenarioContext) : base(driverHelper, scenarioContext)
        {
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;
        }

        private IWebElement PreviousPageNavigator => _driverHelper.Driver.FindElement(By.LinkText("«"));
        private IWebElement NextPageNavigator => _driverHelper.Driver.FindElement(By.LinkText("»"));
        private IWebElement PageNumber => _driverHelper.Driver.FindElement(By.XPath("//div[@class = 'row']//div[@class = 'pull-xs-right']"));

        public void NavigatePaginationPages() => NavigatePaginationPages(PreviousPageNavigator, NextPageNavigator, PageNumber);      
    }
}
