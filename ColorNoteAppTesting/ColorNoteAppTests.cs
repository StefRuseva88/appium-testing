using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Service;

namespace ColorNoteAppTesting
{
    public class ColorNoteAppTests
    {
        private AndroidDriver driver;
        private AppiumLocalService appiumLocalService;

        [OneTimeSetUp]
        public void Setup()
        {
            appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();

            appiumLocalService.Start();

            var androidOptions = new AppiumOptions()
            {
                AutomationName = "UiAutomator2",
                PlatformName = "Android",
                PlatformVersion = "14.0",
                DeviceName = "Pixel_7_API_34",
                App = @"C:\Users\Stef\Desktop\Notepad.apk"
            };

            androidOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);

            driver = new AndroidDriver(appiumLocalService, androidOptions);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            try
            {
                var skipTutorial = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/btn_start_skip"));
                skipTutorial.Click();
            }
            catch (NoSuchElementException)
            {
                // Skip tutorial
            }

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
            appiumLocalService?.Dispose();
        }

        [Test]
        public void TestCreateNewNote()
        {
            IWebElement newNoteBtn = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            newNoteBtn.Click();

            IWebElement noteText = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            noteText.Click();

            IWebElement noteTextField = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Test Note!");

            IWebElement backBtn = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backBtn.Click();
            backBtn.Click();

            IWebElement noteTitle = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));

            Assert.That(noteTitle, Is.Not.Null, "Note was not found!");
            Assert.That(noteTitle.Text, Is.EqualTo("Test Note!"), "Title don't mach!");
        }

        [Test]
        public void TestUpdateNote()
        {
            IWebElement newNoteBtn = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            newNoteBtn.Click();

            IWebElement noteText = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            noteText.Click();

            IWebElement noteTextField = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Test Note2!");

            IWebElement backBtn = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backBtn.Click();
            backBtn.Click();

            IWebElement note = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Test Note2!\")"));
            note.Click();

            IWebElement editBtn = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_btn"));
            editBtn.Click();

            noteTextField = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));

            noteTextField.Click();
            noteTextField.Clear();
            noteTextField.SendKeys("Test Note Edited!");

            IWebElement editedNote = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Test Note Edited!\")"));

            Assert.That(editedNote, Is.Not.Null, "Note was not found!");
            Assert.That(editedNote.Text, Is.EqualTo("Test Note Edited!"), "Note was not edited!");
        }

        [Test]
        public void TestDeleteNote()
        {
            IWebElement newNoteBtn = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            newNoteBtn.Click();

            IWebElement noteText = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            noteText.Click();

            IWebElement noteTextField = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Note For Deletion!");

            IWebElement backBtn = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backBtn.Click();
            backBtn.Click();

            IWebElement note = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Note For Deletion!\")"));
            note.Click();

            IWebElement menuBtn = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/menu_btn"));
            menuBtn.Click();

            IWebElement deleteBtn = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Delete\")"));
            deleteBtn.Click();

            IWebElement okBtn = driver.FindElement(MobileBy.Id("android:id/button1"));
            okBtn.Click();

            IWebElement addNoteMsg = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/empty_text"));

            Assert.That(addNoteMsg.Text, Is.EqualTo("Add note"), "Note was not deleted!");
        }
    }
}