using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Week14
{
  public class Example1
  {
    [Fact]
    public void Test1()
    {
      // Tarayıcıyı Aç
      using IWebDriver driver = new ChromeDriver();

      // İlgili URL Adresine Git
      driver.Navigate().GoToUrl("https://www.apple.com");

      // 2 saniye bekle
      Thread.Sleep(2000);

      // `using` ile driver otomatik kapatılacak
    }
  }
}