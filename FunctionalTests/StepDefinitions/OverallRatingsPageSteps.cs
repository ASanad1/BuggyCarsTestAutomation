using BuggyCarsTestAutomation.Core;
using BuggyCarsTestAutomation.FunctionalTests.Pages;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.StepDefinitions
{
    [Binding]
    public sealed class OverallRatingsPageSteps
    {
        private readonly DriverHelper _driverHelper;
        private readonly OverallRatingsPage _overallRatingsPage;

        public OverallRatingsPageSteps(DriverHelper driverHelper, ScenarioContext scenarioContext)
        {
            _driverHelper = driverHelper;
            _overallRatingsPage = new OverallRatingsPage(_driverHelper, scenarioContext);
        }

        [When(@"I select make (.*) and model (.*)")]
        public void WhenISelectMakeAndModel(string make, string model) =>
            _overallRatingsPage.SelectCarMakeAndModel(make, model);

        [Then(@"I navigate to the (.*) (.*) page")]
        public void ThenINavigateToThePage(string make, string model) =>
            _overallRatingsPage.VerifyMakeAndModelPageIsDisplayed(make, model);
    }
}
