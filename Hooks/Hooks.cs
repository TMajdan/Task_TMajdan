﻿using AventStack.ExtentReports.Gherkin.Model;
using BoDi;
using OpenQA.Selenium;
using Task_TMajdan.SeleniumFramework;
using Task_TMajdan.Src.Delivery;
using Task_TMajdan.Utility;

namespace Task_TMajdan.Hooks
{
    [Binding]
    public class Hooks : ExtentReport
    {
        private readonly IObjectContainer _container;
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver? _driver;
        private string baseUrl = AppConfig.GetAppSetting("baseUrl");
        private string apiUrl = AppConfig.GetAppSetting("apiUrl");

        public Hooks(IObjectContainer container, ScenarioContext ScenarioContext)
        {
            _container = container;
            _scenarioContext = ScenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentReportInit();
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            var browserTags = scenarioContext.ScenarioInfo.Tags[0];
            var browserValue = browserTags.Split(':')[1].Trim();

            switch (browserValue)
            {
                case "Chrome":
                    _driver = DriverBuilder.SetupChromeDriver();
                    break;
                case "Edge":
                    _driver = DriverBuilder.SetupEdgeDriver();
                    break;
                case "Firefox":
                    _driver = DriverBuilder.SetupFirefoxDriver();
                    break;
                default:
                    _driver = DriverBuilder.SetupChromeDriver();
                    break;
            }

            UserLoginHandler.TryLoginWithApi(_driver, baseUrl, apiUrl);

            _container.RegisterInstanceAs<IWebDriver>(_driver);

            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenarioContext.ScenarioContainer.RegisterInstanceAs(_driver);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();

            DriverBuilder.QuitDriver(driver);
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            ReportUtility.SaveTestsStepToReport(scenarioContext, _driver, _container);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportTearDown();
        }
    }
}