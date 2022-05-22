using BuggyCarsTestAutomation.Core;
using BuggyCarsTestAutomation.FunctionalTests.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace BuggyCarsTestAutomation.FunctionalTests.Pages
{
    class PopularModelPage : BasePage
    {
        private readonly DriverHelper _driverHelper;
        private readonly ScenarioContext _scenarioContext;

        public PopularModelPage(DriverHelper driverHelper, ScenarioContext scenarioContext) : base(driverHelper, scenarioContext)
        {
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;
        }

        private IWebElement CommentTxt => _driverHelper.Driver.FindElement(By.Id("comment"));
        private IWebElement VoteBtn => _driverHelper.Driver.FindElement(By.XPath("//button[text() = 'Vote!']"));
        private IWebElement SuccessfulVoteMessage => _driverHelper.Driver.FindElement(By.XPath("//p[@class = 'card-text' and contains(text(), 'Thank you')]"));

        public void SubmitVote()
        {
            var voteCommentTxt = Guid.NewGuid().ToString();
            CommentTxt.SendKeys(voteCommentTxt);
            _scenarioContext.Add("VoteComment", voteCommentTxt);
            VoteBtn.Click();
        }

        public void VerifyVoteIsRecorded()
        {
            var voteCommentTxt = _scenarioContext.ContainsKey("LoginData") &&
                string.IsNullOrEmpty(_scenarioContext.Get<string>("VoteComment")) ?
                _scenarioContext.Get<string>("VoteComment") : string.Empty;

            Assert.AreEqual(Constants.SuccessfulVoteMessage, SuccessfulVoteMessage.Text.Trim());
            _driverHelper.Driver.Navigate().Refresh();
            var voteComment = _driverHelper.Driver.FindElement(By.XPath($"//table[@class = 'table']/descendant::td[text() = '{voteCommentTxt.Trim()}']"));            
            Assert.IsTrue(voteComment.Displayed);        
        }
    }
}
