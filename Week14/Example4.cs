namespace Week14
{
    [Fact]
public void Test4()
{
    IWebDriver driver = new ChromeDriver();
    driver.Manage().Window.Maximize();


    driver.Navigate().GoToUrl("https://demoqa.com/dynamic-properties");


    //Bu sayfa her yüklendiğinde p elementinin id değeri değişmektedir. Bu id değerini bulalım.
    IWebElement labelWithRandomID = driver.FindElement(By.XPath("//p[contains(text(),'This text has random Id')]"));
    output.WriteLine(labelWithRandomID.GetAttribute("id"));
    output.WriteLine(labelWithRandomID.Text);



    //Belirli bir koşul gerçekleşinceye kadar kodları kilitlemek için kullanacağız.
    //2. parametre timeout süresidir. 10 saniye vererek sistem en fazla 10 saniye bekletilecek veya bir başka deyişle koşulun gerçekleşmesini en fazla 10 saniye bekleyeceğiz anlamına gelir.
    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


    /************************************************************
    * 1) Enable After 5 Seconds 
    ************************************************************/
    // Bu butonun ID'si "enableAfter"
    // 5 saniye sonra aktif hale gelecek. Aktif (Enabled) olmasını bekleyelim.

    //wait.Until metodu belirli bir koşul gerçekleşinceye kadar kod bloğunu kilitler. Örneğin aşağıda, enableAfter adındaki butonu buluyoruz ve onun enabled özelliği true oluncaya kadar bekliyoruz. Yani bu şu demek enabled özelliği true olmadığı sürece alttaki diğer kodlar çalışmaz.
    IWebElement enableButton = wait.Until<IWebElement>( drv =>
    {
        IWebElement element = drv.FindElement(By.Id("enableAfter"));
        if (element.Enabled)
            return element;
        return null;
    });

    enableButton.Click();

    output.WriteLine("Butona Tıklandı");



    /************************************************************
      2) Color Change
    ************************************************************/
    // Bu butonun ID'si "colorChange"
    // 5 saniye sonra rengi değişiyor (default mavi -> kırmızı).
    // Renginin "rgba(220, 53, 69, 1)" olduğunu bekleyelim (kırmızı).
    //colorChange butonunun yazı rengi kırmızı oluncaya kadar BEKLET!!!
    IWebElement colorChangeButton = wait.Until<IWebElement>(drv =>
    {
        IWebElement element = drv.FindElement(By.Id("colorChange"));
        string color = element.GetCssValue("color");
        output.WriteLine("Butonun Yazı Rengi : "+color);
        return element;
        return color == "rgba(220, 53, 69, 1)" ? element : null;
    });

    output.WriteLine("ColorChange butonunun rengi kırmızıya döndü");
    output.WriteLine($"CSS 'color' Değeri : {colorChangeButton.GetCssValue("color")}");



    IWebElement visibleAfterButton = wait.Until<IWebElement>(drv => {

        IWebElement element = drv.FindElement(By.Id("visibleAfter"));
        return element.Displayed ? element : null;
    });

    TestContext.WriteLine("visibleAfter Butonu Görünür Hale Geldi");
    visibleAfterButton.Click();



    driver.Quit();

}
}