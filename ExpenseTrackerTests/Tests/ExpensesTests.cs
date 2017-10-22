using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace ExpenseTrackerTests
{
  [TestClass]
  public class ExpensesTests : ExpensesLogic
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
    public void AddExpense()
    {
      var categoryName = "car";
      var day = "22";
      var month = "10";
      var year = "2017";
      var amount = "100.00";
      var reason = "car service";

      try
      {
        LoginRegisterLogic.Login(chrome, userName, pass);
        CategoriesLogic.AddCategory(chrome, categoryName);

        AddExpense(chrome, categoryName, day, month, year, amount, reason);

        var listExpensesButton = chrome.FindElement(By.LinkText("List Expenses"));
        listExpensesButton.Click();

        var table = chrome.FindElement(By.ClassName("table"));
        var tr = table.FindElements(By.TagName("tr")).ToList();
        tr.RemoveAt(0);

        Assert.IsTrue(tr[1].Text == "22.10.17 car 100,00 € car service");
      }
      catch(Exception e)
      {
        Assert.Fail(e.Message);
      }
    }
  }
}
