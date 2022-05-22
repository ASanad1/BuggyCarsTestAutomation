using BuggyCarsTestAutomation.FunctionalTests.Common;
using Microsoft.Extensions.Configuration;
using System;

namespace BuggyCarsTestAutomation.Core
{
    class SettingsLoader
    {
        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath($"{AppContext.BaseDirectory}/Core")
                .AddJsonFile("appSettings.json",
                optional: true,
                reloadOnChange: true).Build();
        }

        public void LoadSettings()
        {
            var settings = GetConfiguration();
            Constants.GlobalBrowser = settings["settings:browser"];
            Constants.DriverImplicitWait = int.Parse(settings["settings:implicitWait"]);
            Constants.IsBrowserHeadless = bool.Parse(settings["settings:isHeadless"]);
            Constants.GlobalBaseUrl = settings["settings:baseUrl"];
        }
    }
}
