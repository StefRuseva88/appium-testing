using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace Appium_Testing
{
    public class Tests
    {
        private AndroidDriver driver;
        private AppiumLocalService service;

        [OneTimeSetUp]
        public void Setup()
        {
            service = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();
            service.Start();

            AppiumOptions options = new AppiumOptions();
            options.App = @"C:\Users\Stef\Desktop\Appium\com.example.androidappsummator.apk";
            options.PlatformName = "Android";
            options.AutomationName = "UiAutomator2";
            options.DeviceName = "Pixel_7_API_34";
            options.PlatformVersion = "14.0";

            driver = new AndroidDriver(service, options);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver?.Quit();
            service?.Dispose();
            service?.Dispose();
        }

        [Test]
        public void TestValidSumator()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("2");

            var secondInput = driver.FindElement(MobileBy.XPath("//android.widget.EditText[@resource-id=\"com.example.androidappsummator:id/editText2\"]"));
            secondInput.Clear();
            secondInput.SendKeys("5");

            var sumButton = driver.FindElement(MobileBy.ClassName("android.widget.Button"));
            sumButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));
            Assert.That(result.Text, Is.EqualTo("7"));
        }

        [Test]
        public void TestInvalidSumator()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("2");

            var secondInput = driver.FindElement(MobileBy.XPath("//android.widget.EditText[@resource-id=\"com.example.androidappsummator:id/editText2\"]"));
            secondInput.Clear();
            secondInput.SendKeys("5");

            var sumButton = driver.FindElement(MobileBy.ClassName("android.widget.Button"));
            sumButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));
            Assert.That(result.Text, Is.Not.EqualTo("8"));
        }

        [Test]
        public void TestWithValidData()
        {
            IWebElement firstField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText1"));
            firstField.Clear();
            firstField.SendKeys("2");

            IWebElement secondField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText2"));
            secondField.Clear();
            secondField.SendKeys("5");

            IWebElement sumButton = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/buttonCalcSum"));
            sumButton.Click();

            IWebElement resultField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editTextSum"));
            Assert.That(resultField.Text, Is.EqualTo("7"));
        }

        [Test]
        public void TestWithInvalidData_ClickOnlySumButton()
        {
            IWebElement firstField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText1"));
            firstField.Clear();

            IWebElement secondField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText2"));
            secondField.Clear();

            IWebElement sumButton = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/buttonCalcSum"));
            sumButton.Click();

            IWebElement resultField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editTextSum"));
            Assert.That(resultField.Text, Is.EqualTo("error"));
        }

        [Test]
        public void TestWithInvalidData_FillOnlyFirstField()
        {
            IWebElement firstField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText1"));
            firstField.Clear();
            firstField.SendKeys("2");

            IWebElement secondField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText2"));
            secondField.Clear();

            IWebElement sumButton = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/buttonCalcSum"));
            sumButton.Click();

            IWebElement resultField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editTextSum"));
            Assert.That(resultField.Text, Is.EqualTo("error"));
        }

        [Test]
        public void TestWithInvalidData_FillOnlySecondField()
        {
            IWebElement firstField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText1"));
            firstField.Clear();

            IWebElement secondField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText2"));
            secondField.Clear();
            secondField.SendKeys("5");

            IWebElement sumButton = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/buttonCalcSum"));
            sumButton.Click();

            IWebElement resultField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editTextSum"));
            Assert.That(resultField.Text, Is.EqualTo("error"));
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
            IWebElement firstField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText1"));
            firstField.Clear();
            firstField.SendKeys(input1);

            IWebElement secondField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText2"));
            secondField.Clear();
            secondField.SendKeys(input2);

            IWebElement sumButton = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/buttonCalcSum"));
            sumButton.Click();

            IWebElement resultField = driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editTextSum"));
            Assert.That(resultField.Text, Is.EqualTo(expectedResults));
        }
    }
}