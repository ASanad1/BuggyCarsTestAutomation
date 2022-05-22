using BuggyCarsTestAutomation.Core;
using BuggyCarsTestAutomation.FunctionalTests.Pages;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.StepDefinitions
{
    [Binding]
    public sealed class PopularModelPageSteps
    {
        private readonly DriverHelper _driverHelper;
        private readonly PopularModelPage _popularModelPage;

        public PopularModelPageSteps(DriverHelper driverHelper, ScenarioContext scenarioContext)
        {
            _driverHelper = driverHelper;
            _popularModelPage = new PopularModelPage(_driverHelper, scenarioContext);
        }

        [Then(@"I can vote for popular model with comment This car is amazing")]
        public void ThenICanVoteForPopularModelWithCommentThisCarIsAmazing() =>
            _popularModelPage.SubmitVote();

        [Then(@"I verify my vote is recorded")]
        public void ThenIVerifyMyVoteIsRecorded() =>
            _popularModelPage.VerifyVoteIsRecorded();
    }
}
