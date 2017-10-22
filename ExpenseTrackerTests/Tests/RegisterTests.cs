using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Linq;

namespace ExpenseTrackerTests
{
  [TestClass]
  public class RegisterTests : LoginRegisterLogic
  {
    IWebDriver chrome;
    string uri;
    string userName;
    string pass;
    string passNew;

    [TestInitialize]
    public void TestInitialize()
    {
      chrome = new ChromeDriver(Environment.CurrentDirectory);
      uri = "http://thawing-shelf-73260.herokuapp.com/";
      userName = "test";
      pass = "test";
      passNew = "newpassword";

      chrome.Navigate().GoToUrl(uri);
      chrome.Manage().Window.Maximize();
    }

    [TestCleanup]
    public void Cleanup()
    {
      //Here should be deleting the account from database. I am unable to do this having no access to DB.
      //Database.Delete(userName);

      chrome.Close();
      chrome.Quit();
      chrome.Dispose();
    }

    //---------------------------------------------------------------------------------------//

    [TestMethod]
    public void RegisterTest()
    {
      try
      {
        //Register user
        RegisterUser(chrome, userName, pass, pass);

        //Check that correct user is logged in
        var loggedUserName = chrome.FindElement(By.CssSelector("a[href='editaccount']")).Text;
        Assert.IsTrue(loggedUserName == userName);

        //Logout
        Logout(chrome);

        //Check that current page is login page 
        var loggedOutPage = chrome.FindElement(By.ClassName("container"));
        Assert.AreEqual("Login\r\nUsername\r\nPassword\r\nRegister new user", loggedOutPage.Text);
      }
      catch(Exception e)
      {
        Assert.Fail($"{e.Message}");
      }
    }

    [TestMethod]
    public void RegisterUserAlreadyExistsTest()
    {
      try
      {
        //Register user
        RegisterUser(chrome, userName, pass, pass);
        Logout(chrome);

        //Try to register again with the same credentials
        RegisterUser(chrome, userName, pass, pass);

        //Assert error
        var error = chrome.FindElement(By.ClassName("alert-danger"));
        Assert.AreEqual("password1 wasn't equal to password2", error.Text);
        //error text in line above is wrong. Should be changed after bugfix
      }
      catch (Exception e)
      {
        Assert.Fail($"{e.Message}");
      }
    }

    [TestMethod]
    public void RegisterNotEqualPasswordsTest()
    {
      try
      {
        //Try to register again with two different passwords
        RegisterUser(chrome, userName, pass, "wrong");

        //Assert error
        var alert = chrome.SwitchTo().Alert().Text;
        Assert.AreEqual("Error: Passwords aren't equal!", alert);
      }
      catch (Exception e)
      {
        Assert.Fail($"{e.Message}");
      }
    }

    [TestMethod]
    public void EditAccountTest()
    {
      try
      {
        //Register user
        //RegisterUser(chrome, userName, pass, pass);

        Login(chrome, userName, pass);

        //push Account button
        var loggedUserName = chrome.FindElement(By.CssSelector("a[href='editaccount']"));
        loggedUserName.Click();

        //Try to edit account entering wrong old password
        UpdateUserAccount(chrome, "wrong", passNew, passNew);

        //Assert error
        var oldPassError = chrome.FindElement(By.ClassName("alert-danger"));
        Assert.AreEqual("oldpassword wasn't correct", oldPassError.Text);

        //Try to edit account entering different new passwords
        UpdateUserAccount(chrome, pass, passNew, "wrong");

        //Assert error
        var alertText = chrome.SwitchTo().Alert().Text;
        Assert.AreEqual("Error: New Passwords aren't equal!", alertText);
        chrome.SwitchTo().Alert().Accept();

        //Edit account with correct passwords
        UpdateUserAccount(chrome, pass, passNew, passNew);

        //logout
        Logout(chrome);

        //use old password for login
        Login(chrome, userName, pass);

        //Assert error
        var error = chrome.FindElement(By.ClassName("alert-danger"));
        Assert.AreEqual("unknown login or wrong password", error.Text);

        //use correct password for login
        Login(chrome, userName, passNew);

        //Check that user is logged in
        Assert.IsTrue(chrome.FindElement(By.CssSelector("a[href='editaccount']")).Text == userName);
      }
      catch (Exception e)
      {
        Assert.Fail($"{e.Message}");
      }
    }
  }
}
