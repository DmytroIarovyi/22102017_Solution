using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace ExpenseTrackerTests
{
  [TestClass]
  public class CategoriesTests : CategoriesLogic
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
      LoginRegisterLogic.RegisterUser(chrome, userName, pass, pass);
      LoginRegisterLogic.Logout(chrome);
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
    public void AddCategoryTest()
    {
      try
      {
        var categoryName = "car";
        LoginRegisterLogic.Login(chrome, userName, pass);

        AddCategory(chrome, categoryName);

        var table = chrome.FindElement(By.ClassName("table"));
        var categories = table.FindElements(By.TagName("tr")).ToList();
        categories.RemoveAt(0);

        Assert.IsTrue(categories.First().Text == categoryName);
      }
      catch(Exception e)
      {
        Assert.Fail(e.Message);
      }
    }


    [TestMethod]
    public void DeleteCategoryTest()
    {
      try
      {
        var categoryName = "car";

        LoginRegisterLogic.Login(chrome, userName, pass);

        AddCategory(chrome, categoryName);
        DeleteCategory(chrome, categoryName);
      }
      catch(Exception e)
      {
        Assert.Fail(e.Message);
      }
    }

    [TestMethod]
    public void EditCategoryTest()
    {
      try
      {
        var categoryName = "car";
        var newName = "house";

        LoginRegisterLogic.Login(chrome, userName, pass);

        AddCategory(chrome, categoryName);
        EditCategory(chrome, categoryName, newName);
      }
      catch (Exception e)
      {
        Assert.Fail(e.Message);
      }
    }
  }
}
