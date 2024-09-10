using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium_Testing
{
    public class SumatorPage
    {
        private readonly AndroidDriver driver;

        public SumatorPage(AndroidDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement firstField => driver.FindElement(MobileBy.Id
               ("editText1"));

        public IWebElement secondField => driver.FindElement(MobileBy.Id
                ("editText2"));

        public IWebElement sumButton => driver.FindElement(MobileBy.Id
                ("buttonCalcSum"));

        public IWebElement resultField => driver.FindElement(MobileBy.Id
                ("editTextSum"));

        public string GetResult(string firstNum, string secondNum)
        {
            firstField.Clear();
            secondField.Clear();

            firstField.SendKeys(firstNum);
            secondField.SendKeys(secondNum);
            sumButton.Click();

            return resultField.Text;
        }

        public void ClearFields()
        {
            firstField.Clear();
            secondField.Clear();
        }
    }
}
