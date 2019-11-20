using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Applitool.Hackathon.Pages
{
    public static class FinancialOverviewPage
    {

        public static void ClickAmountTableHeader(IWebDriver driver)
        {
            By amountTableHeaderBy = By.Id("amount");
            driver.FindElement(amountTableHeaderBy).Click();
        }


        public static void VerifyAmountAscendingOrder(IWebDriver driver)
        {     
            ArrayList amountsBeforeAsc = new ArrayList();
            ReadOnlyCollection<IWebElement> tableRows = driver.FindElements(By.XPath("//tbody/tr"));
            for (int i = 0; tableRows.Count > i; i++)
            {
                String amountInStr = driver.FindElement(By.XPath("//tbody/tr["+(i+1)+"]/td[5]/span")).Text;
                String amount = amountInStr.ToUpper().Replace("USD", "").Trim();
                if (amount.Contains("-"))
                {
                    amountsBeforeAsc.Add((decimal.Parse(Regex.Replace(amount, @"[^\d.]", ""))) * -1);
                }
                else
                {
                    amountsBeforeAsc.Add(decimal.Parse(Regex.Replace(amount, @"[^\d.]", "")));
                }
            }
            ArrayList amountsAfterAsc = new ArrayList();
            amountsAfterAsc.AddRange(amountsBeforeAsc);
            amountsAfterAsc.Sort();

            for (int i = 0; amountsBeforeAsc.Count > i; i++)
            {
                Assert.IsTrue(amountsBeforeAsc[i].Equals(amountsAfterAsc[i]), "By clicking on Amount is not showing the column in descending order!");
            }
        }

        public static void ClickCompareExpenses(IWebDriver driver)
        {
            driver.FindElement(By.Id("showExpensesChart")).Click();
        }

        public static void VerifyFlashSales(IWebDriver driver)
        {
            By flashSale1DivBy = By.Id("flashSale");
            By flashSale2DivBy = By.Id("flashSale2");
            Assert.IsTrue(driver.FindElement((flashSale1DivBy)).Displayed, "Flash sale-1 is not found!");
            Assert.IsTrue(driver.FindElement((flashSale2DivBy)).Displayed, "Flash sale-2 is not found!");
            By flashSale1ImageBy = By.XPath("//div[@id='flashSale']/img");
            By flashSale2ImageBy = By.XPath("//div[@id='flashSale2']/img");
            Assert.IsTrue(driver.FindElement((flashSale1ImageBy)).Enabled, "Flash sale-1 image is not found!");
            Assert.IsTrue(driver.FindElement((flashSale2ImageBy)).Enabled, "Flash sale-2 image is not found!");
            Assert.IsTrue(driver.FindElement((flashSale1ImageBy)).GetAttribute("src").Contains("img/flashSale.gif"), "Flash sale-1 image in incorrect!");
            Assert.IsTrue(driver.FindElement((flashSale2ImageBy)).GetAttribute("src").Contains("img/flashSale2.gif"), "Flash sale-2 image in incorrect!");
        }
    }
}
