using OpenQA.Selenium;

namespace ExpenseTrackerTests
{
  public class LoginRegisterLogic
  {
    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="driver">Chrome, FireFox, InternetExplorer, Safari etc.</param>
    /// <param name="userName">user name</param>
    /// <param name="pass1">password</param>
    /// <param name="pass2">repeat password</param>
    public static void RegisterUser(IWebDriver driver, string userName, string pass1, string pass2)
    {
      var registerButton = driver.FindElement(By.LinkText("Register new user"));
      registerButton.Click();

      var newUserName = driver.FindElement(By.CssSelector("input[id = login]"));
      newUserName.SendKeys(userName);

      var password1 = driver.FindElement(By.CssSelector("input[id = password1]"));
      password1.SendKeys(pass1);

      var password2 = driver.FindElement(By.CssSelector("input[id = password2]"));
      password2.SendKeys(pass2);

      driver.FindElement(By.CssSelector("input[id = submit]")).Click();
    }

    /// <summary>
    /// Update user account (change the password)
    /// </summary>
    /// <param name="driver">Chrome, FireFox, InternetExplorer, Safari etc.</param>
    /// <param name="passOld">old password</param>
    /// <param name="passNew">new password</param>
    /// <param name="passNewConfirm">confirm new password</param>
    public static void UpdateUserAccount(IWebDriver driver, string passOld, string passNew, string passNewConfirm)
    {
      var oldPassword = driver.FindElement(By.CssSelector("input[id = password]"));
      oldPassword.SendKeys(passOld);

      var newPassword = driver.FindElement(By.CssSelector("input[id = newpassword1]"));
      newPassword.SendKeys(passNew);

      var repeatNewPassword = driver.FindElement(By.CssSelector("input[id = newpassword2]"));
      repeatNewPassword.SendKeys(passNewConfirm);

      driver.FindElement(By.CssSelector("input[id = submit]")).Click();
    }

    /// <summary>
    /// Log in to the system
    /// </summary>
    /// <param name="driver">Chrome, FireFox, InternetExplorer, Safari etc.</param>
    /// <param name="userName">user name</param>
    /// <param name="pass">password</param>
    public static void Login(IWebDriver driver, string userName, string pass)
    {
      var newUserName = driver.FindElement(By.CssSelector("input[id = login]"));
      newUserName.SendKeys(userName);

      var password = driver.FindElement(By.CssSelector("input[id = password]"));
      password.SendKeys(pass);

      driver.FindElement(By.CssSelector("input[id = submit]")).Click();
    }

    /// <summary>
    /// Log out from the system
    /// </summary>
    /// <param name="driver">Chrome, FireFox, InternetExplorer, Safari etc.</param>
    public static void Logout(IWebDriver driver)
    {
      var logoutButton = driver.FindElement(By.LinkText("Logout"));
      logoutButton.Click();
    }
  }
}
