using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace ExpenseTrackerTests
{
  public class CategoriesLogic
  {
    /// <summary>
    /// Add category
    /// </summary>
    /// <param name="driver">Chrome, FireFox, InternetExplorer, Safari etc.</param>
    /// <param name="name">name of a category</param>
    public static void AddCategory(IWebDriver driver, string name)
    {
      var listCategoriesButton = driver.FindElement(By.LinkText("List Categories"));
      listCategoriesButton.Click();

      var addCategoryButton = driver.FindElement(By.CssSelector("a[href='addcategory.jsp']"));
      addCategoryButton.Click();

      var categoryName = driver.FindElement(By.CssSelector("input[id = name]"));
      categoryName.SendKeys(name);

      driver.FindElement(By.CssSelector("input[id = submit]")).Click();
    }

    /// <summary>
    /// Delete category
    /// </summary>
    /// <param name="driver">Chrome, FireFox, InternetExplorer, Safari etc.</param>
    /// <param name="categoryName">name of a category</param>
    public static void DeleteCategory(IWebDriver driver, string categoryName)
    {
      var table = driver.FindElement(By.ClassName("table"));
      var categories = table.FindElements(By.TagName("tr")).ToList();
      categories.RemoveAt(0);

      var tags = categories.First().FindElements(By.TagName("a"));
      tags[1].Click();

      driver.SwitchTo().Alert().Accept();
      Thread.Sleep(5000);

      var tableNew = driver.FindElement(By.ClassName("table"));
      var categoriesAfterDelete = tableNew.FindElements(By.TagName("tr")).ToList();
      categoriesAfterDelete.RemoveAt(0);

      Assert.IsTrue(categoriesAfterDelete.Count == categories.Count - 1);
    }

    /// <summary>
    /// Edit category
    /// </summary>
    /// <param name="driver">Chrome, FireFox, InternetExplorer, Safari etc.</param>
    /// <param name="categoryName">name of a category</param>
    /// <param name="newName">new name for a category</param>
    public static void EditCategory(IWebDriver driver, string categoryName, string newName)
    {
      var table = driver.FindElement(By.ClassName("table"));
      var categories = table.FindElements(By.TagName("tr")).ToList();

      var tags = categories[1].FindElements(By.TagName("a"));
      tags[0].Click();

      var newname = driver.FindElement(By.CssSelector("input[id = name]"));
      newname.Clear();
      newname.SendKeys(newName);

      driver.FindElement(By.CssSelector("input[id = submit]")).Click();

      var categoriesNew = table.FindElements(By.TagName("tr")).ToList();
      Assert.IsTrue(categoriesNew[1].Text == newName);
    }
  }
}
