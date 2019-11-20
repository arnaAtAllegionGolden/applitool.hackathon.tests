using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applitool.Hackathon.Pages
{
    public static class ExpensesChartPage
    {

        public static void VerifyYears(IWebDriver driver)
        {
            //Cannot verify these with Selenium
        }

        public static void VerifyBarsHeights(IWebDriver driver)
        {
            //Cannot verify these with Selenium
        }

        public static void ClickShowDataForNextYear(IWebDriver driver)
        {
            driver.FindElement(By.Id("addDataset")).Click();
        }

        public static void VerifyAddedDataSet(IWebDriver driver)
        {
            //Cannot verify these with Selenium
        }
    }
}
