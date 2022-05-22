using BuggyCarsTestAutomation.Core;
using BuggyCarsTestAutomation.FunctionalTests.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.Pages
{
    class ProfilePage : BasePage
    {
        private readonly DriverHelper _driverHelper;
        private readonly ScenarioContext _scenarioContext;

        public ProfilePage(DriverHelper driverHelper, ScenarioContext scenarioContext) : base(driverHelper, scenarioContext)
        {
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;
        }

        private IWebElement UserGreeting => _driverHelper.Driver.FindElement(By.XPath("//span[contains(text(), 'Hi, ')]"));
        private IWebElement ProfileLink => _driverHelper.Driver.FindElement(By.LinkText("Profile"));
        private IWebElement LogoutLink => _driverHelper.Driver.FindElement(By.LinkText("Logout"));
        private IWebElement FirstNameTxt => _driverHelper.Driver.FindElement(By.Id("firstName"));
        private IWebElement LastNameTxt => _driverHelper.Driver.FindElement(By.Id("lastName"));
        private IWebElement GenderTxt => _driverHelper.Driver.FindElement(By.Id("gender"));
        private IWebElement GenderMaleSelection => _driverHelper.Driver.FindElement(By.XPath("//datalist[@id = 'genders']/option[@value = 'Male']"));
        private IWebElement GenderFemaleSelection => _driverHelper.Driver.FindElement(By.XPath("//datalist[@id = 'genders']/option[@value = 'Female']"));
        private IWebElement AgeTxt => _driverHelper.Driver.FindElement(By.Id("age"));
        private IWebElement AddressTxt => _driverHelper.Driver.FindElement(By.Id("address"));
        private IWebElement PhoneTxt => _driverHelper.Driver.FindElement(By.Id("phone"));
        private IWebElement HobbyMenu => _driverHelper.Driver.FindElement(By.Id("hobby"));
        private IWebElement SaveBtn => _driverHelper.Driver.FindElement(By.XPath("//button[@type = 'submit' and text() = 'Save']"));
        private IWebElement CancelBtn => _driverHelper.Driver.FindElement(By.LinkText("Cancel"));
        private IWebElement ProfileUpdateResult => _driverHelper.Driver.FindElement(By.XPath("(//div[contains(@class, 'alert-success')])[1]"));

        public bool IsUserLoggedIn() => UserGreeting.Displayed && ProfileLink.Displayed && LogoutLink.Displayed;
        public void AssertUserIsLoggedIn()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(UserGreeting.Displayed);
                Assert.IsTrue(ProfileLink.Displayed);
                Assert.IsTrue(LogoutLink.Displayed);
            });
        }

        public void EnterProfileDetails(string userInput)
        {
            var fieldAndValueList = new List<KeyValuePair<string, string>>();
            var fieldNamesAndValues = userInput.Split(';');
            foreach (var obj in fieldNamesAndValues)
            {
                var fieldAndValue = obj.Split(new[] { ':' }, 2);
                fieldAndValueList.Add(new KeyValuePair<string, string>(fieldAndValue[0], fieldAndValue[1]));
            }

            ProfileLink.Click();
            foreach (var field in fieldAndValueList)
            {
                if (!string.IsNullOrWhiteSpace(field.Value))
                {
                    switch (field.Key.Trim())
                    {
                        case "FirstName":
                            FirstNameTxt.Clear();
                            FirstNameTxt.SendKeys(field.Value);
                            break;
                        case "LastName":
                            LastNameTxt.Clear();
                            LastNameTxt.SendKeys(field.Value);
                            break;
                        case "Gender":
                            GenderTxt.Clear();
                            GenderTxt.Click();
                            switch (field.Value.Trim())
                            {
                                case "Male":
                                    ClickElementUsingJS(GenderMaleSelection);
                                    break;
                                case "Female":
                                    ClickElementUsingJS(GenderFemaleSelection);
                                    break;
                                default:
                                    GenderTxt.SendKeys(field.Value);
                                    break;
                            }
                            break;
                        case "Age":
                            AgeTxt.Clear();
                            var inputText = field.Value.Trim().ToLower().Equals("generate random age") ?
                                 new Random().Next(18, 76).ToString() : field.Value;
                            AgeTxt.SendKeys(inputText);
                            break;
                        case "Address":
                            AddressTxt.Clear();
                            AddressTxt.SendKeys(field.Value);
                            break;
                        case "Phone":
                            PhoneTxt.Clear();
                            PhoneTxt.SendKeys(field.Value);
                            break;
                        case "Hobby":
                            var hobbySelection = new SelectElement(HobbyMenu);
                            hobbySelection.SelectByText(field.Value.Trim());
                            break;
                            throw new ArgumentException($"The field name: {field.Value} is not supported");
                    }
                }
            }
        }

        public void ClickSaveBtn() => SaveBtn.Click();

        public void ClickCancelBtn() => CancelBtn.Click();

        public void VerifyProfileUpdateIsSuccessful() =>
            VerifyDisplayedMessage(Constants.SuccessfulProfileUpdateMessage, ProfileUpdateResult);

        public void ClickLogoutLink() => LogoutLink.Click();
    }
}
