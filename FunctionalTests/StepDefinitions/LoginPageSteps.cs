using BuggyCarsTestAutomation.Core;
using BuggyCarsTestAutomation.FunctionalTests.Pages;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.StepDefinitions
{
    [Binding]
    public sealed class LoginPageSteps
    {
        private readonly DriverHelper _driverHelper;
        private readonly LoginPage _loginPage;

        public LoginPageSteps(DriverHelper driverHelper, ScenarioContext scenarioContext)
        {
            _driverHelper = driverHelper;
            _loginPage = new LoginPage(_driverHelper, scenarioContext);
        }

        [Given(@"I navigate to registration page")]
        public void GivenINavigateToRegistrationPage() =>
            _loginPage.ClickRegisterButton();

        [Given(@"I enter username (.*) and password (.*)")]
        public void GivenIEnterUsernameAndPassword(string username, string password) =>
            _loginPage.EnterLoginCredentials(username, password);

        [Given(@"I click login")]
        [When(@"I click login")]
        public void WhenIClickLogin() =>
            _loginPage.ClickLoginButton();

        [Given(@"I can login using the registered login credentials")]
        [Then(@"I can login using the registered login credentials")]
        public void ThenICanLoginUsingTheRegisteredLoginCredentials()
        {
            _loginPage.EnterLoginCredentials(string.Empty, string.Empty);
            _loginPage.ClickLoginButton();
        }

        [Then(@"I get invalid credentials error")]
        public void ThenIGetInvalidCredentialsError() =>
            _loginPage.VerifyCredentialErrorMessage();

        [Then(@"I should be logged out successfully")]
        public void ThenIShouldBeLoggedOutSuccessfully() => 
            _loginPage.AssertUserIsLoggedOut();

        [Given(@"I go to popular model page")]
        [When(@"I go to popular model page")]
        public void WhenIGoToPopularModelPage() => 
            _loginPage.NavigateToPopularModelPage();

        [When(@"I go to popular make page")]
        public void WhenIGoToPopularMakePage() => 
            _loginPage.NavigateToPopularMakePage();

        [Given(@"I go to overall ratings page")]
        public void WhenIGoToOverallRatingsPage() => 
            _loginPage.NavigateToOverallRatingPage();
    }
}
