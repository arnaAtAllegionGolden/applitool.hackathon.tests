using System;
using Applitool.Hackathon.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Applitools;
using Applitools.Selenium;
using System.Drawing;

namespace Applitool.Hackathon
{
 

    [TestClass]
    public class VisualALTests
    {
        private static IWebDriver driver;
        private static String url_v1 = "https://demo.applitools.com/hackathon.html";
        private static String url_v2 = "https://demo.applitools.com/hackathonV2.html";
        private EyesRunner runner;
        private Eyes eyes;
        private static String appName = "Applitools Demo App";
        private static BatchInfo batch;

        [TestInitialize]
        public void Setup()
        {
            runner = new ClassicRunner();
            eyes = new Eyes(runner);
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            batch = new BatchInfo(appName);
            batch.Id = "Applitools_Demo_App_Batch_Id";
            eyes.Batch = batch;
        }

        [TestMethod]
        public void LoginPageUIElementsTest()
        {
            eyes.ForceFullPageScreenshot = true;
            eyes.Open(driver, appName, "Login Page", new Size(800, 600));
            driver.Navigate().GoToUrl(url_v2);
            LoginPage.AttemptToLoginNotEnteringAny(driver);
            eyes.CheckWindow("Login Page - after no entering credentials");
            LoginPage.AttemptToLoginNotEnteringUserName(driver);
            eyes.CheckWindow("Login Page - after entering username only");
            LoginPage.AttemptToLoginNotEnteringPassword(driver);
            eyes.CheckWindow("Login Page - after entering password only");
            LoginPage.LoginSuccess(driver);
            eyes.CheckWindow("Login Page - after entering username and password");
        }

        [TestMethod]
        public void DataDrivenTest()
        {
            eyes.ForceFullPageScreenshot = true;
            eyes.Open(driver, appName, "Finalcial Overview Page", new Size(800, 600));
            driver.Navigate().GoToUrl(url_v2);
            LoginPage.LoginSuccess(driver);
            eyes.CheckWindow("Finalcial Overview Page");
        }

        [TestMethod]
        public void TableSortTest()
        {
            eyes.ForceFullPageScreenshot = true;
            eyes.Open(driver, appName, "Finalcial Overview Page - Amount", new Size(800, 600));
            driver.Navigate().GoToUrl(url_v2);
            LoginPage.LoginSuccess(driver);
            FinancialOverviewPage.ClickAmountTableHeader(driver);
            eyes.CheckWindow("Finalcial Overview Page - Amount");
        }

        [TestMethod]
        public void CanvasChartTest()
        {
            eyes.Open(driver, appName, "Expenses Char Page", new Size(800, 600));
            driver.Navigate().GoToUrl(url_v2);
            LoginPage.LoginSuccess(driver);
            FinancialOverviewPage.ClickCompareExpenses(driver);
            eyes.CheckWindow("Expenses Char for recent 2 years");
            ExpensesChartPage.ClickShowDataForNextYear(driver);
            eyes.CheckWindow("Expenses Char after adding year view");
        }

        [TestMethod]
        public void DynamicContentTest()
        {
            eyes.Open(driver, appName, "Finalcial Overview Page - Flash Sales", new Size(800, 600));
            driver.Navigate().GoToUrl(url_v2 + "?showAd=true");
            LoginPage.LoginSuccess(driver);
            eyes.CheckWindow("Finalcial Overview Page - Flash Sales");
        }

        [TestCleanup]
        public void TearDown()
        {
            eyes.CloseAsync();
            driver.Quit();
            eyes.AbortIfNotClosed();
            TestResultsSummary allTestResults = runner.GetAllTestResults();
        }
    }
}
