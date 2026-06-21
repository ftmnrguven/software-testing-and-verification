namespace Week14
{
    [Test]
public void Test5()
{
    IWebDriver driver = new ChromeDriver();
    driver.Navigate().GoToUrl("https://demoqa.com/browser-windows");
    driver.Manage().Window.Maximize();


    //Şu an açık olan pencereyi tanımlayan bir id değeri vardır. Bunu elde edelim.
    string mainWindowID = driver.CurrentWindowHandle;

    //yeni sekme açacak olan butona bas
    IWebElement newTabButton = driver.FindElement(By.Id("tabButton"));
    newTabButton.Click();



    //Butona tıkladıktan sonra pencere 2 tane sekme olacak. Birincisi ana sekme ikincisi de açılan yeni sekme
    //Şimdi açılan yeni sekmeye geçip oradaki id değeri sampleHeading olan ifadenin textini alalım
    foreach(string windowHandle in driver.WindowHandles)
        if(windowHandle != mainWindowID)
        {
            //yeni sekmeye geç
            driver.SwitchTo().Window(windowHandle);

            //Biraz bekle
            Thread.Sleep(2000);

            //Yeni sekmedeki metni çek
            IWebElement messageLabel = driver.FindElement(By.Id("sampleHeading"));
            output.WriteLine($"Açılan Yeni Sekmedeki Mesaj : {messageLabel.Text}");


            //Ana Sekmeye geç
            driver.SwitchTo().Window(mainWindowID);
            break;
            
        }

    Thread.Sleep(3000);
    driver.Quit();
}

}