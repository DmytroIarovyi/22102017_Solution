using OpenQA.Selenium;

namespace ExpenseTrackerTests
{
  public class ExpensesLogic
  {
    /// <summary>
    /// Add expense
    /// </summary>
    /// <param name="driver">Chrome, FireFox, InternetExplorer, Safari etc.</param>
    /// <param name="categoryName">name for a category</param>
    /// <param name="day">select day</param>
    /// <param name="month">select month</param>
    /// <param name="year">select year</param>
    /// <param name="amount">amount for input</param>
    /// <param name="reason">resaon</param>
    public void AddExpense(IWebDriver driver, string categoryName, string day, string month, string year, string amount, string reason)
    {
      var addExpenseButton = driver.FindElement(By.LinkText("Add Expense"));
      addExpenseButton.Click();

      var inputDay = driver.FindElement(By.CssSelector("input[id = day]"));
      inputDay.SendKeys(day);

      var inputMonth = driver.FindElement(By.CssSelector("input[id = month]"));
      inputMonth.Clear();
      inputMonth.SendKeys(month);

      var inputYear = driver.FindElement(By.CssSelector("input[id = year]"));
      inputYear.SendKeys(year);

      var category = driver.FindElement(By.CssSelector("select[id = category]"));
      category.SendKeys(categoryName);

      var inputAmount = driver.FindElement(By.CssSelector("input[id = amount]"));
      inputAmount.SendKeys(amount);

      var inputReason = driver.FindElement(By.CssSelector("input[id = reason]"));
      inputReason.SendKeys(reason);

      driver.FindElement(By.CssSelector("input[id = submit]")).Click();
    }
  }
}
