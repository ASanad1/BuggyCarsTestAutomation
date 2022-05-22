using BuggyCarsTestAutomation.Core;
using BuggyCarsTestAutomation.FunctionalTests.Pages;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.StepDefinitions
{
    [Binding]
    public sealed class RegistrationPageSteps
    {
        private readonly DriverHelper _driverHelper;
        private readonly RegistrationPage _registrationPage;

        public RegistrationPageSteps(DriverHelper driverHelper, ScenarioContext scenarioContext)
        {
            _driverHelper = driverHelper;
            _registrationPage = new RegistrationPage(_driverHelper, scenarioContext);
        }

        [Given(@"I enter user details")]
        public void GivenIEnterUserDetails(Table table) =>
            _registrationPage.EnterRegistrationDetails(table);

        [Given(@"I click register")]
        [When(@"I click register")]
        public void WhenIClickRegister() =>
            _registrationPage.ClickRegisterButton();

        [Given(@"I will be registered successfully")]
        [Then(@"I will be registered successfully")]
        public void ThenIWillBeRegisteredSuccessfully() =>
            _registrationPage.VerifyRegistrationIsSuccessful();

        [Then(@"I get user already exist error")]
        public void ThenIGetUserAlreadyExistError() =>
            _registrationPage.VerifyRegistrationAlreadyExists();

        [Then(@"I get passwords mismatch error")]
        public void ThenIGetPasswordsMismatchError() =>
            _registrationPage.VerifyConfirmPasswordError();
    }
}
