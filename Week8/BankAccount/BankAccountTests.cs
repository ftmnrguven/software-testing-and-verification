using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BankAccount
{
    public class BankAccountTests
    {

        BankAccount bankAccount; //
        ITestOutputHelper _output; //Testlerde Mesaj Göstermek İçin Kullanırız.
        public BankAccountTests(ITestOutputHelper output)
        {
            //Alttaki her bir unit test metodu çalışmadan öne bu constructor çalışır
            bankAccount = new BankAccount("Mehmet Yılmaz", 500);
            _output = output;
            _output.WriteLine("BankAccountTests Constructor Çalıştı ve Yeni Bir BankAccount Nesnesi Oluştu");
        }

        [Fact]
        public void CreateAccount_WithValidInitialBalance_SetsCorrectBalance()
        {
            //Arrange
            string accountHolder = "Ahmet Yılmaz";
            decimal initialBalance = 100;

            //Act
            BankAccount account = new BankAccount(accountHolder, initialBalance);


            //Assert
            Assert.Equal(initialBalance, account.Balance);
            Assert.Equal(accountHolder, account.AccountHolder);
            Assert.False(account.IsClosed);
            Assert.True(account.Balance > 0);
            Assert.NotInRange(account.Balance, 0, 5);
        }

        [Fact]
        public void Deposit_ValidAmount_IncreaseBalance()
        {
            //Arrange
            decimal increaseAmount = 500;
            decimal expectedResult = bankAccount.Balance + increaseAmount;

            //Act
            bankAccount.Deposit(increaseAmount);


            //Assert
            Assert.Equal(expectedResult, bankAccount.Balance);
        }


        [Fact]
        public void WithDraw_WithSufficientBalance_DecreasesBalance()
        {
            //Arrange
            decimal decAmount = 100;
            decimal expectedBalance = bankAccount.Balance - decAmount;


            //Act
            bankAccount.WithDraw(decAmount);

            //Assert
            Assert.Equal(expectedBalance, bankAccount.Balance);
            Assert.True(bankAccount.Balance >= 0);

        }


        [Fact]
        public void Deposit_NegativeAmount_ThrowsArgumentException()
        {
            //Arrange
            decimal amount = -100;

            //Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => bankAccount.Deposit(amount));
            Assert.Equal("Yatırılacak Miktar Pozitif Bir Değer Olmalıdır", exception.Message);
        }



        [Fact]
        public void Withdraw_InsufficientFunds_ThrowsInvalidOperationException()
        {
            decimal amount = bankAccount.Balance + 100;

            Exception exc = Assert.Throws<InvalidOperationException>(() => bankAccount.WithDraw(amount));
            Assert.Equal("Çekilecek Para Miktarı Hesaptaki Paradan Daha Fazla Olamaz", exc.Message);
        }

        [Fact]
        public void Deposit_IntoClosedAccount_ThrowsInvalidOperationException()
        {
            //Arrange
            decimal amount = 100;
            bankAccount.CloseAccount(); // hesabı kapattık
            string expectedMessage = "Kapalı Bir Hesaba Para Yatıramazsın";

            var exc = Assert.Throws<InvalidOperationException>(() => bankAccount.Deposit(amount));
            Assert.Equal(expectedMessage, exc.Message);

        }

        [Fact]
        public void CloseAccount_AlreadyClosed_ThrowsInvalidOperationException()
        {
            //Zaten Kapalı olan bir hesabı kapatmaya çalışacağız. 
            bankAccount.CloseAccount();//Hesabı kapattık
            string expectedMessage = "Kapalı Bir Hesabı Yine Kapatamazsın";


            var exc = Assert.Throws<InvalidOperationException>(() => bankAccount.CloseAccount());
            Assert.Equal(expectedMessage, exc.Message);

        }

    }
}
