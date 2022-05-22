using BuggyCarsTestAutomation.Core;
using BuggyCarsTestAutomation.FunctionalTests.Pages;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.StepDefinitions
{
    [Binding]
    public sealed class PopularMakePageSteps
    {
        private readonly DriverHelper _driverHelper;
        private readonly PopularMakePage _popularMakePage;

        public PopularMakePageSteps(DriverHelper driverHelper, ScenarioContext scenarioContext)
        {
            _driverHelper = driverHelper;
            _popularMakePage = new PopularMakePage(_driverHelper, scenarioContext);
        }

        [Then(@"I navigate through popular make pages until last page")]
        public void ThenINavigateThroughPopularMakePagesUntilLastPage() =>
            _popularMakePage.NavigatePaginationPages();
    }
}
