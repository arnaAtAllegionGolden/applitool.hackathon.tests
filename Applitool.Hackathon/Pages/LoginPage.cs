using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Applitool.Hackathon.Pages
{
    public static class LoginPage
    {
        

        public static void VerifyElementsOnLoginPage(IWebDriver driver)
        {
            //Verify log image
            Assert.IsTrue(driver.FindElement(By.XPath("//img[@src='img/logo-big.png']")).Displayed, "Logo image is not displayed!");

            //Verify login form
            IWebElement loginFormElement = driver.FindElement(By.XPath("//h4[@class='auth-header']"));
            Assert.IsTrue(loginFormElement.Displayed, "Could not find the 'Login Form' label!");
            Assert.AreEqual(loginFormElement.Text, "Login Form");

            //Verify username
            ReadOnlyCollection<IWebElement> formGroups = GetFormGroups(driver);
            Assert.IsTrue(formGroups.Count == 2, "Username or password fields are missing!");
            IWebElement userNameImage = formGroups[0].FindElement(By.XPath("//div"));
            Assert.IsTrue(userNameImage.Displayed, "Could not find the username image!");
            IWebElement userNameLabel = formGroups[0].FindElement(By.XPath("//label"));
            Assert.IsTrue(userNameLabel.Displayed, "Username label is not found!");
            Assert.AreEqual(formGroups[0].Text, "Username");
            IWebElement userNameTextField = GetUserNameElement(driver);
            Assert.IsTrue(userNameTextField.Displayed, "Username textfield is not found!");
            Assert.IsTrue(userNameTextField.Enabled, "Username textfield is disabled!");
            Assert.AreEqual(userNameTextField.GetAttribute("placeholder"), "Enter your username", "Username texfield placeholder value is incorrect!");
            Assert.AreEqual(userNameTextField.Text, "", "Username field is not empty!");

            //Verify password
            IWebElement passwordImage = formGroups[1].FindElement(By.XPath("//div"));
            Assert.IsTrue(passwordImage.Displayed, "Could not find the password image!");
            IWebElement passwordLabel = formGroups[1].FindElement(By.XPath("//label"));
            Assert.IsTrue(passwordLabel.Displayed, "Password label is not found!");
            Assert.AreEqual(formGroups[1].Text, "Password");
            IWebElement passwordTextField = GetPasswordElement(driver);
            Assert.IsTrue(passwordTextField.Displayed, "Password textfield is not found!");
            Assert.IsTrue(passwordTextField.Enabled, "Password textfield is disabled!");
            Assert.AreEqual(passwordTextField.Text, "", "Password text field is not empty!");

            //Verify other fields
            IWebElement signInBtn = GetSignInBtnElement(driver);
            Assert.IsTrue(signInBtn.Enabled, "Sign in button is disabled!");
            IWebElement rememberMeCheckBox = driver.FindElement(By.XPath("//label[@class='form-check-label']"));
            Assert.IsTrue(rememberMeCheckBox.Displayed, "Remember me checkbox is not displayed!");
            Assert.IsFalse(rememberMeCheckBox.Selected, "Remember me checkbox is selected by default!");
            IWebElement rememberMeLabel = driver.FindElement(By.XPath("//input[@class='form-check-input']"));
            Assert.IsTrue(rememberMeLabel.Displayed, "Remember me label is not found!");
            Assert.AreEqual(rememberMeCheckBox.Text, "Remember Me");
            Assert.IsTrue(driver.FindElement(By.XPath("//img[@src='img/social-icons/twitter.png']")).Displayed, "Twitter is not displayed!");
            Assert.IsTrue(driver.FindElement(By.XPath("//img[@src='img/social-icons/facebook.png']")).Displayed, "FB is not displayed!");
            Assert.IsTrue(driver.FindElement(By.XPath("//img[@src='img/social-icons/linkedin.png']")).Displayed, "LinkedIn is not displayed!");
        }

        private static void Login (IWebDriver driver, String userName, String password)
        {
            GetUserNameElement(driver).Clear();
            GetUserNameElement(driver).SendKeys(userName);
            GetPasswordElement(driver).Clear();
            GetPasswordElement(driver).SendKeys(password);
            GetSignInBtnElement(driver).Click();
        }

        public static void AttemptToLoginNotEnteringAny(IWebDriver driver)
        {
            Login(driver, "", "");
            Assert.AreEqual(GetLoginAlertElement(driver).Text, "Both Username and Password must be present");
        }

        public static void AttemptToLoginNotEnteringPassword(IWebDriver driver)
        {
            Login(driver, "UserName", "");
            Assert.AreEqual(GetLoginAlertElement(driver).Text, "Password must be present");
        }

        public static void AttemptToLoginNotEnteringUserName(IWebDriver driver)
        {
            Login(driver, "", "Password");
            Assert.AreEqual(GetLoginAlertElement(driver).Text, "Username must be present");
        }

        public static void LoginSuccess(IWebDriver driver)
        {
            Login(driver, "UserName", "Password");
        }

        private static ReadOnlyCollection<IWebElement> GetFormGroups(IWebDriver driver)
        {
            return driver.FindElements(By.XPath("//div[@class='form-group']"));
        }

        private static IWebElement GetUserNameElement(IWebDriver driver)
        {
            ReadOnlyCollection<IWebElement> formGroups = GetFormGroups(driver);
            return formGroups[0].FindElement(By.XPath("//input[@id='username']"));

        }

        private static IWebElement GetPasswordElement(IWebDriver driver)
        {
            ReadOnlyCollection<IWebElement> formGroups = GetFormGroups(driver);
            return formGroups[1].FindElement(By.XPath("//input[@id='password']"));
        }

        private static IWebElement GetSignInBtnElement(IWebDriver driver)
        {
            return driver.FindElement(By.Id("log-in"));

        }

        private static IWebElement GetLoginAlertElement(IWebDriver driver)
        {
            IWebElement alertElement = driver.FindElement(By.XPath("//div[@class='alert alert-warning']"));
            Assert.IsTrue(alertElement.Displayed, "Login failed alert is not displayed!");
            return alertElement;
        }
    }
}
