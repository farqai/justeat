﻿using System.Configuration;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace JustEatAutomation.Utilities
{
    public class WebDriverConfig
    {
        private static IWebDriver _driver;
        private ScenarioContext _context;

        public WebDriverConfig(ScenarioContext context)
        {
            this._context = context;
        }

        public IWebDriver InitLocalDriver()
        {
            _driver = new ChromeDriver();  
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("url"));
            return _driver;
        }

        public void CleanUp()
        {
            try
            {
                _driver.Quit();
            }
            finally 
            {
                EnsureProcessTermination("chromedriver");
            }   
        }

        private static void EnsureProcessTermination(string processName)
        {
            try
            {
                var driverProcesses = Process.GetProcessesByName(processName);
                foreach (var driverProcess in driverProcesses)
                {
                    driverProcess.Kill();
                }
            }
            catch
            {
                // ignore
            }
        }
    }
}