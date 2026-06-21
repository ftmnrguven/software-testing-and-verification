namespace Week14
{
    [Test]
public void Test2()
{
    //Örneğin amacı: Google tarayıcıyı aç. www.google.com'a git. bir metni arat

    //1-Yeni bir Selenium Web Driver örneği oluştur
    IWebDriver driverChrome = new ChromeDriver();

    //2-Belirtilen URL'e git
    driverChrome.Navigate().GoToUrl("https://www.google.com");

    //3-Tarayıcıyı tam ekran yap
    driverChrome.Manage().Window.Maximize();


    //4-Google'daki search text box'ın name özelliği 'q'. Bu q name özelliğini kullanarak ilgili arama textbox alanını bulacağız
    IWebElement elSearchBox = driverChrome.FindElement(By.Name("q"));

    //5-İlgili search alanına bir metin yazdıralım
    elSearchBox.SendKeys("Selenium Web Driver");


    //6-Beşinci adımda yazılan şeyi aratmak için ENTER tuşuna bastırmamız lazım
    elSearchBox.SendKeys(Keys.Enter);

    //7-İlk seçeneği bulma
    IWebElement firstResult = driverChrome.FindElement(By.CssSelector("h3"));

    //9-Çıkan ilk sonuca git
		firstResult.Click();
		
		//10-Tarayıcıyı kapat
		driverChrome.Quit();
}
}