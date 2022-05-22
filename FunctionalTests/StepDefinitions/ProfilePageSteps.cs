using BuggyCarsTestAutomation.Core;
using BuggyCarsTestAutomation.FunctionalTests.Pages;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.StepDefinitions
{
    [Binding]
    public sealed class ProfilePageSteps
    {
        private readonly DriverHelper _driverHelper;
        private readonly ProfilePage _profilePage;

        public ProfilePageSteps(DriverHelper driverHelper, ScenarioContext scenarioContext)
        {
            _driverHelper = driverHelper;
            _profilePage = new ProfilePage(_driverHelper, scenarioContext);
        }

        [Given(@"I should be successfully logged in")]
        [Then(@"I should be successfully logged in")]
        public void ThenIShouldBeSuccessfullyLoggedIn() =>
            _profilePage.AssertUserIsLoggedIn();

        [When(@"I update profile details (.*)")]
        public void WhenIUpdateProfileDetails(string userInput) =>
            _profilePage.EnterProfileDetails(userInput);

        [When(@"Click save")]
        public void WhenClickSave() =>
            _profilePage.ClickSaveBtn();

        [Then(@"My profile should be updated")]
        public void ThenMyProfileShouldBeUpdated() =>
            _profilePage.VerifyProfileUpdateIsSuccessful();

        [When(@"I click logout")]
        public void WhenIClickLogout() =>
            _profilePage.ClickLogoutLink();
    }
}
