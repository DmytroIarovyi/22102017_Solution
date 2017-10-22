using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ExpenseTrackerTests
{
  [TestClass]
  public class LoginTests : LoginRegisterLogic
  {
    IWebDriver chrome;
    string uri;
    string userName;
    string pass;

    [TestInitialize]
    public void TestInitialize()
    {
      chrome = new ChromeDriver(Environment.CurrentDirectory);
      uri = "http://thawing-shelf-73260.herokuapp.com/";
      userName = "test";
      pass = "test";

      chrome.Navigate().GoToUrl(uri);
      chrome.Manage().Window.Maximize();

      //Initial steps
      RegisterUser(chrome, userName, pass, pass);
      Logout(chrome);
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
    public void LoginTest()
    {
      try
      {
        //Login 
        Login(chrome, userName, pass);

        //Check that correct user is logged in
        var loggedUserName = chrome.FindElement(By.CssSelector("a[href='editaccount']")).Text;
        Assert.IsTrue(loggedUserName == userName);

        //logout
        Logout(chrome);
      }
      catch (Exception ex)
      {
        Assert.Fail($"{ex.Message}");
      }
    }

    [TestMethod]
    public void LoginWrongPasswordTest()
    {
      try
      {
        //Login 
        Login(chrome, userName, "wrong");

        //Assert error
        var error = chrome.FindElement(By.ClassName("alert-danger"));
        Assert.AreEqual("unknown login or wrong password", error.Text);
      }
      catch (Exception ex)
      {
        Assert.Fail($"{ex.Message}");
      }
    }

    [TestMethod]
    public void LoginWrongUserNameTest()
    {
      try
      {
        //Login 
        Login(chrome, "wrongName", pass);

        //Assert error
        var error = chrome.FindElement(By.ClassName("alert-danger"));
        Assert.AreEqual("unknown login or wrong password", error.Text);
      }
      catch (Exception ex)
      {
        Assert.Fail($"{ex.Message}");
      }
    }
  }
}
