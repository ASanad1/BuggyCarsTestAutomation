using BuggyCarsTestAutomation.Core;
using BuggyCarsTestAutomation.FunctionalTests.Common;
using BuggyCarsTestAutomation.FunctionalTests.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BuggyCarsTestAutomation.FunctionalTests.Pages
{
    class RegistrationPage : BasePage
    {
        private readonly DriverHelper _driverHelper;
        private readonly ScenarioContext _scenarioContext;

        public RegistrationPage(DriverHelper driverHelper, ScenarioContext scenarioContext) : base(driverHelper, scenarioContext)
        {
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;
        }

        private IWebElement UsernameTxt => _driverHelper.Driver.FindElement(By.Id("username"));
        private IWebElement FirstNameTxt => _driverHelper.Driver.FindElement(By.Id("firstName"));
        private IWebElement LastNameTxt => _driverHelper.Driver.FindElement(By.Id("lastName"));
        private IWebElement PasswordTxt => _driverHelper.Driver.FindElement(By.Id("password"));
        private IWebElement ConfirmPasswordTxt => _driverHelper.Driver.FindElement(By.Id("confirmPassword"));
        private IWebElement ConfirmPasswordError => _driverHelper.Driver.FindElement(By.XPath("//input[@id = 'confirmPassword']/following-sibling::div[1]"));
        private IWebElement RegisterButton => _driverHelper.Driver.FindElement(By.XPath("//button[@type = 'submit' and text() = 'Register']"));
        private IWebElement CancelButton => _driverHelper.Driver.FindElement(By.LinkText("Cancel"));
        private IWebElement RegistrationResult => _driverHelper.Driver.FindElement(By.XPath("//div[contains(@class, 'result alert')]"));

        public void EnterRegistrationDetails(Table table)
        {
            var registrationDetails = table.CreateInstance<RegistrationDataModel>();
            if (registrationDetails.Username.Equals("Generate Random Username"))
                registrationDetails.GenerateRandomUsername();

            UsernameTxt.SendKeys(registrationDetails.Username);
            FirstNameTxt.SendKeys(registrationDetails.FirstName);
            LastNameTxt.SendKeys(registrationDetails.LastName);
            PasswordTxt.SendKeys(registrationDetails.Password);
            ConfirmPasswordTxt.SendKeys(registrationDetails.ConfirmPassword);

            _scenarioContext.Add("LoginData", new LoginDataModel
            {
                Username = registrationDetails.Username,
                Password = registrationDetails.Password
            });
        }

        public void ClickRegisterButton() =>
            RegisterButton.Click();

        public void VerifyRegistrationIsSuccessful() =>
            VerifyDisplayedMessage(Constants.SuccessfulRegistrationMessage, RegistrationResult);

        public void VerifyRegistrationAlreadyExists() =>
            VerifyDisplayedMessage(Constants.ExistingRegistrationError, RegistrationResult);

        public void VerifyConfirmPasswordError()
        {
            Assert.Multiple(() =>
            {
                VerifyDisplayedMessage(Constants.MismatchedPasswordsError, ConfirmPasswordError);
                Assert.IsFalse(RegisterButton.Enabled);
            });
        }
    }
}
