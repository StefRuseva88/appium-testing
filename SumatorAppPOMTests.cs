using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;

namespace Appium_Testing
{
    public class SumatorAppPOMTests
    {
        private AndroidDriver driver;
        private AppiumLocalService service;
        private SumatorPage sumatorPage;

        [OneTimeSetUp]
        public void Setup()
        {
            service = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();
            service.Start();

            AppiumOptions options = new AppiumOptions();
            options.App = @"C:\Users\Stef\Desktop\com.example.androidappsummator.apk";
            options.PlatformName = "Android";
            options.AutomationName = "UiAutomator2";
            options.DeviceName = "Pixel_7_API_34";
            options.PlatformVersion = "14.0";

            driver = new AndroidDriver(service, options);

            sumatorPage = new SumatorPage(driver);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver?.Quit();
            service?.Dispose();

        }

        [Test]
        public void TestValidSumator()
        {
            var result = sumatorPage.GetResult("2", "5");
            Assert.That(result, Is.EqualTo("7"));
        }

        [Test]
        public void TestInvalidSumator_ClickOnlySumButton()
        {
            sumatorPage.ClearFields();
            sumatorPage.sumButton.Click();
            Assert.That(sumatorPage.resultField.Text, Is.EqualTo("error")); 
        }

        [Test]
        public void TestInvalidSumator_FillOnlyFirstField()
        {
            sumatorPage.ClearFields();
            sumatorPage.firstField.SendKeys("2");
            sumatorPage.sumButton.Click();
            Assert.That(sumatorPage.resultField.Text, Is.EqualTo("error"));
        }

        [Test]
        public void TestInvalidSumator_FillOnlySecondField()
        {
            sumatorPage.ClearFields();
            sumatorPage.secondField.SendKeys("2");
            sumatorPage.sumButton.Click();
            Assert.That(sumatorPage.resultField.Text, Is.EqualTo("error"));
        }

        [Test]
        [TestCase("10", "10", "20")]
        [TestCase("0", "0", "0")]
        [TestCase("100", "100", "200")]
        [TestCase("1000", "1000", "2000")]
        [TestCase("0", "1000", "1000")]
        [TestCase("10.9", "10.1", "21.0")]
        public void TestWithValidData_Parametrized(string input1, string input2, string expectedResults)
        {
            var result = sumatorPage.GetResult(input1, input2);
            Assert.That(result, Is.EqualTo(expectedResults));
        }
    }
}
