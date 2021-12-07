using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace blazor_app.playwright.e2e_tests.Pages
{
    [TestFixture(typeof(ChromiumTestBrowser))]
    [TestFixture(typeof(FirefoxTestBrowser))]
    public class CounterTests <TBrowser> where TBrowser : IPlaywrightBrowser, new()
    {
        private IPlaywrightBrowser _browser;

        [SetUp]
        public async Task Setup()
        {
            _browser = new TBrowser();
            await _browser.Setup();
        }
        
        [Test]
        public async Task CaptureScreenshot()
        {
            //https://medium.com/version-1/playwright-a-modern-end-to-end-testing-for-web-app-with-c-language-support-c55e931273ee

            await _browser.Page.GotoAsync("https://www.google.com");
            await _browser.Page.ScreenshotAsync(new PageScreenshotOptions { Path = "Chromium_screenshot.png" });
            Assert.Pass();
        }
    }
}
