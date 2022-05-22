using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BuggyCarsTestAutomation.FunctionalTests.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Concurrent;
using TechTalk.SpecFlow;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(3)]
namespace BuggyCarsTestAutomation.Core
{
    [Binding]
    public sealed class Hooks
    {
        private static ExtentTest _feature;
        private static readonly ConcurrentDictionary<string, ExtentTest> _featureDictionary = new ConcurrentDictionary<string, ExtentTest>();
        [ThreadStatic]
        private static ExtentTest _scenario;
        private static AventStack.ExtentReports.ExtentReports _report;
        private readonly ScenarioContext _scenarioContext;
        private readonly DriverHelper _driverHelper;

        public Hooks(ScenarioContext scenarioContext, DriverHelper driverHelper)
        {
            _scenarioContext = scenarioContext;
            _driverHelper = driverHelper;
        }

        [BeforeTestRun(Order = 1)]
        public static void SetSettings()
        {
            new SettingsLoader().LoadSettings();
        }

        [BeforeTestRun(Order = 2)]
        public static void InitiallizeReport()
        {
            var projectDirectory = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            var htmlReporter = new ExtentV3HtmlReporter(
                $"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\Reports\\BuggyCarsAutomationTestResults_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.html");
            _report = new AventStack.ExtentReports.ExtentReports();
            _report.AttachReporter(htmlReporter);
            _report.AddSystemInfo("Test User Name: ", Environment.UserName);
            _report.AddSystemInfo("Machine Name: ", Environment.MachineName);
            _report.AddSystemInfo("Browser: ", Constants.GlobalBrowser.ToUpper());
            _report.AddSystemInfo("Headless Browser: ", Constants.IsBrowserHeadless.ToString().ToUpper());
            htmlReporter.LoadConfig($"{ projectDirectory}Core\\extentConfig.xml");
        }

        [BeforeFeature]
        public static void InsertReportingFeatures(FeatureContext featureContext)
        {
            _feature = _report.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            _featureDictionary.TryAdd(featureContext.FeatureInfo.Title, _feature);
        }

        [BeforeScenario(Order = 1)]
        public void SetUpTest(FeatureContext featureContext)
        {
            var featureName = featureContext.FeatureInfo.Title;
            if (_featureDictionary.ContainsKey(featureName))
            {
                _scenario = _featureDictionary[featureName].CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
            }
        }

        [BeforeScenario(Order = 2)]
        public void LaunchBrowser()
        {
            switch (Constants.GlobalBrowser.ToLower().Trim())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    if (Constants.IsBrowserHeadless)
                        chromeOptions.AddArguments("--headless", "--window-size=1920,1200");

                    _driverHelper.Driver = new ChromeDriver(chromeOptions);
                    break;
                default:
                    throw new ArgumentException($"The selected browser: {Constants.GlobalBrowser} is not supported");
            }
            _driverHelper.Driver.Manage().Window.Maximize();
            _driverHelper.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Constants.DriverImplicitWait);
            _driverHelper.Driver.Navigate().GoToUrl(Constants.GlobalBaseUrl);
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                if (stepType == "When")
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                if (stepType == "Then")
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                if (stepType == "And")
                    _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
                if (stepType == "But")
                    _scenario.CreateNode<But>(_scenarioContext.StepContext.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                var mediaEntity = CreateScreenshot();
                if (mediaEntity != null)
                {
                    if (stepType == "Given")
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message, mediaEntity);
                    if (stepType == "When")
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message, mediaEntity);
                    if (stepType == "Then")
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message, mediaEntity);
                    if (stepType == "And")
                        _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message, mediaEntity);
                    if (stepType == "But")
                        _scenario.CreateNode<But>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message, mediaEntity);
                }
                else
                {
                    if (stepType == "Given")
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message);
                    if (stepType == "When")
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message);
                    if (stepType == "Then")
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message);
                    if (stepType == "And")
                        _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message);
                    if (stepType == "But")
                        _scenario.CreateNode<But>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message);
                }
            }
        }

        [AfterScenario]
        public void TearDownDriver()
        {
            if (_driverHelper.Driver != null)
            {
                _driverHelper.Driver.Quit();
                _driverHelper.Driver = null;
            }
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            _report.Flush();
        }

        private MediaEntityModelProvider CreateScreenshot()
        {
            var fileName = $"{TestContext.CurrentContext.Test.ID}_{DateTime.Now:hhmmssff}.jpg";
            MediaEntityModelProvider mediaEntityModelProvider = null;
            try
            {
                var screenshot = ((ITakesScreenshot)_driverHelper.Driver).GetScreenshot().AsBase64EncodedString;
                mediaEntityModelProvider = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, fileName).Build();
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
            return mediaEntityModelProvider;
        }
    }
}
