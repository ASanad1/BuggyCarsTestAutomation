using BuggyCarsTestAutomation.Core;
using BuggyCarsTestAutomation.FunctionalTests.Common;
using BuggyCarsTestAutomation.FunctionalTests.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.Pages
{
    class LoginPage : BasePage
    {
        private readonly DriverHelper _driverHelper;
        private readonly ScenarioContext _scenarioContext;

        public LoginPage(DriverHelper driverHelper, ScenarioContext scenarioContext) : base(driverHelper, scenarioContext)
        {
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;
        }

        private IWebElement BannerLogo => _driverHelper.Driver.FindElement(By.LinkText("Buggy Rating"));
        private IWebElement BannerLoginTxt => _driverHelper.Driver.FindElement(By.Name("login"));
        private By BannerLoginTxtLocator => By.Name("login");
        private IWebElement BannerPasswordTxt => _driverHelper.Driver.FindElement(By.Name("password"));
        private IWebElement BannerLoginBtn => _driverHelper.Driver.FindElement(By.XPath("//button[@type = 'submit' and text() = 'Login']"));
        private IWebElement BannerRegisterBtn => _driverHelper.Driver.FindElement(By.LinkText("Register"));
        private IWebElement InvalidCredentialsError => _driverHelper.Driver.FindElement(By.XPath("//span[contains(@class, 'label-warning')]"));
        private IWebElement PopularMakeImage => _driverHelper.Driver.FindElement(By.XPath("//h2[text() = 'Popular Make']/following::img[1]"));
        private IWebElement PopularModelImage => _driverHelper.Driver.FindElement(By.XPath("//h2[text() = 'Popular Model']/following::img[1]"));
        private IWebElement OverallRatingImage => _driverHelper.Driver.FindElement(By.XPath("//h2[text() = 'Overall Rating']/following::img[1]"));

        public void ClickRegisterButton() => BannerRegisterBtn.Click();

        public void EnterLoginCredentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username) &&
                _scenarioContext.ContainsKey("LoginData") &&
                _scenarioContext.Get<LoginDataModel>("LoginData") != null)
                username = _scenarioContext.Get<LoginDataModel>("LoginData").Username;

            if (string.IsNullOrEmpty(password) &&
                _scenarioContext.ContainsKey("LoginData") &&
                _scenarioContext.Get<LoginDataModel>("LoginData") != null)
                password = _scenarioContext.Get<LoginDataModel>("LoginData").Password;

            GetDisplayedElement(BannerLoginTxtLocator, 20).SendKeys(username);
            BannerPasswordTxt.SendKeys(password);
        }

        public void ClickLoginButton() => BannerLoginBtn.Click();

        public void VerifyCredentialErrorMessage()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(InvalidCredentialsError.Displayed);
                Assert.AreEqual(Constants.InvalidCredentialsError, InvalidCredentialsError.Text.Trim());
            });
        }

        public void AssertUserIsLoggedOut()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(BannerLoginTxt.Displayed);
                Assert.IsTrue(BannerPasswordTxt.Displayed);
                Assert.IsTrue(BannerLoginBtn.Displayed);
                Assert.IsTrue(BannerRegisterBtn.Displayed);
            });
        }

        public void NavigateToPopularModelPage()
        {
            BannerLogo.Click();
            PopularModelImage.Click();
        }

        public void NavigateToPopularMakePage()
        {
            BannerLogo.Click();
            PopularMakeImage.Click();
        }

        public void NavigateToOverallRatingPage()
        {
            BannerLogo.Click();
            OverallRatingImage.Click();
        }
    }
}
