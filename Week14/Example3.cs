namespace Week14
{
    [Test]
public void Test3()
{
    IWebDriver driver = new ChromeDriver();
    
    driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");

    driver.Manage().Window.Maximize();

		//id değeri firstName olan html elemanını seç ve ona Ahmet değerini gönder
    IWebElement txtFirstName = driver.FindElement(By.Id("firstName"));
    txtFirstName.SendKeys("Ahmet");


    IWebElement txtLastName = driver.FindElement(By.Id("lastName"));
    txtLastName.SendKeys("Yılmaz");

    IWebElement txtUserEmail = driver.FindElement(By.Id("userEmail"));
    txtUserEmail.SendKeys("ahmetyilmaz@gmail.com");


    //sayfadaki label'lar arasından for özelliği 'gnder-radio-1' olan html element'ini seç
    IWebElement radioMale = driver.FindElement(By.CssSelector("label[for='gender-radio-1']"));
    radioMale.Click();


    IWebElement txtUserNumber = driver.FindElement(By.Id("userNumber"));
    txtUserNumber.SendKeys("05551234567");


    //Tarayıcı Pencersinde Aşağı kaydırmak için (bazı elementler görünmeyebilir)
    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
    js.ExecuteScript("window.scrollBy(0,250)");





      //Date of Birth alanında doğum tarihi verisini seçtiriyoruz. 
      //16 Nisan 2000 tarihini seçtirmeye çalışalım

      IWebElement pickerBirthDate = chromeDriver.FindElement(By.CssSelector("#dateOfBirthInput"));
      pickerBirthDate.Click();

      IWebElement pickerMonth = chromeDriver.FindElement(By.CssSelector(".react-datepicker__month-select"));
      SelectElement selectMonth = new SelectElement(pickerMonth);
      selectMonth.SelectByText("April");


      IWebElement pickerYear = chromeDriver.FindElement(By.CssSelector(".react-datepicker__year-select"));
      SelectElement selectYear = new SelectElement(pickerYear);
      selectYear.SelectByText("2000");

      IWebElement pickerDay = chromeDriver.FindElement(By.CssSelector(".react-datepicker__day--015"));
      pickerDay.Click();


      //Otomatik Tamamlama Alanında Aşağıdaki 3 Değerin girilmesinden sonra bunları seçilmesini sağlayalım.
      string[] subjects = { "Mat","Physi","Economi"};
      foreach(string s in subjects)
      {
          IWebElement selectInput = chromeDriver.FindElement(By.CssSelector("#subjectsInput"));
          selectInput.SendKeys(s);
          Thread.Sleep(300);
          selectInput.SendKeys(Keys.Enter);
      }

    //Hobiler arasındna Sports ve Music alanlarını seçelim.
    IWebElement checkSports = driver.FindElement(By.CssSelector("label[for='hobbies-checkbox-1']"));
    IWebElement checkMusic = driver.FindElement(By.CssSelector("label[for='hobbies-checkbox-3']"));
    checkSports.Click();
    checkMusic.Click();



    //Resim yükleme alanına bilgisayarımızdaki bir görüntüyü yükleyelim.
    IWebElement uploadPicture = driver.FindElement(By.Id("uploadPicture"));
    
    //aşağıdaki satırda, siz kendi bilgisayarınızdaki herhangi bir dosyanın path bilgisini vermelisiniz.
    uploadPicture.SendKeys("C:\\Users\\FurkanPC\\Desktop\\image1.jpg");



    IWebElement txtAddress = driver.FindElement(By.Id("currentAddress"));
    txtAddress.SendKeys("Ankara, İstanbul, Türkiye");

    //State alanına tıkladığımızda liste açılacak
    IWebElement stateField = driver.FindElement(By.Id("state"));
    stateField.Click();


		//DemoQA’daki State alanı klasik <select> etiketi değil; React tabanlı özel bir dropdown ile tanımlanmış. Biz state alanına tıklamadığımız seçenekleri sayfada görünmez. Bu yüzden klasik yöntemle bunu yapamayız ve ByXPath üzerinden yapabilriiz.
		//XPath içindeki // ifadesi, HTML ağacında (DOM) herhangi bir seviyedeki elementi aramak anlamına gelir
    // State listesinden 'Haryana' seçeneğini seçelim (içerik "Haryana" olan div)
    // contains() fonksiyonunu kullanmak için XPath kullanmak gerekir
    // div: Tüm sayfa (DOM) içinde herhangi bir seviyede bulunan div etiketlerini seçer. Yani sayfadaki herhangi bir div olabilir diyoruz
    // contains(text(),'Haryana'): Seçilen div etiketinin içindeki metnin, "Haryana" kelimesini içerip içermediğini kontrol eder.
    IWebElement haryanaOption = driver.FindElement(By.XPath("//div[contains(text(),'Haryana')]"));
    haryanaOption.Click();



    //Yukardakine benzer mantıkla bu sefer city'den seçim yaptıralım
    IWebElement cityField = driver.FindElement(By.Id("city"));
    cityField.Click();

    //açılan seçeneklerden "Panipat" ı seçelim
    IWebElement panipatOption = driver.FindElement(By.XPath("//div[contains(text(),'Panipat')]"));
    panipatOption.Click();



    //Formu göndermek için Submit butonuna tıklamalıyız.
    IWebElement btnSubmit = driver.FindElement(By.Id("submit"));
    btnSubmit.Click();

    Thread.Sleep(2000);

    // Başarılı popup’taki başlığı alıp ekranda yazdıralım
    IWebElement successHeader = driver.FindElement(By.Id("example-modal-sizes-title-lg"));
    TestContext.WriteLine("Form Gönderme Sonucu : "+successHeader.Text);


    Thread.Sleep(5000);


    driver.Quit();

}
}