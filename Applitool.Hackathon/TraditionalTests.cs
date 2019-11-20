using System;
using System.Collections.ObjectModel;
using Applitool.Hackathon.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Applitool.Hackathon
{
    [TestClass]
    public class TraditionalTests
    {
        static IWebDriver driver;
        static String url_v1 = "https://demo.applitools.com/hackathon.html";
        static String url_v2 = "https://demo.applitools.com/hackathonV2.html";


        [TestInitialize]
        public void Setup()
        {

            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url_v2);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TestMethod]
        public void LoginPageUIElementsTest()
        {
            LoginPage.VerifyElementsOnLoginPage(driver);
        }

        [TestMethod]
        public void DataDrivenTest()
        {
            LoginPage.AttemptToLoginNotEnteringAny(driver);
            LoginPage.AttemptToLoginNotEnteringUserName(driver);
            LoginPage.AttemptToLoginNotEnteringPassword(driver);
            LoginPage.LoginSuccess(driver);
        }

        [TestMethod]
        public void TableSortTest()
        {
            LoginPage.LoginSuccess(driver);
            FinancialOverviewPage.ClickAmountTableHeader(driver);
            FinancialOverviewPage.VerifyAmountAscendingOrder(driver);
            ExpensesChartPage.ClickShowDataForNextYear(driver);
            ExpensesChartPage.VerifyAddedDataSet(driver);
            ExpensesChartPage.VerifyYears(driver);
            ExpensesChartPage.VerifyBarsHeights(driver);
        }

        [TestMethod]
        public void CanvasChartTest()
        {
            driver.Navigate().GoToUrl(url_v1 + "?showAd=true");
            LoginPage.LoginSuccess(driver);
            FinancialOverviewPage.ClickCompareExpenses(driver);
            ExpensesChartPage.VerifyYears(driver);
            ExpensesChartPage.VerifyBarsHeights(driver);
            ExpensesChartPage.ClickShowDataForNextYear(driver);
            ExpensesChartPage.VerifyAddedDataSet(driver);
        }

        [TestMethod]
        public void DynamicContentTest()
        {
            LoginPage.LoginSuccess(driver);
            FinancialOverviewPage.VerifyFlashSales(driver);
        }

        [TestCleanup]
        public void TearDown()
        {

            driver.Quit();
        }
    }
}
