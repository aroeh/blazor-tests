using NUnit.Framework;
using System.Threading.Tasks;

namespace blazor_app.playwright.e2e_tests.Pages
{
    [TestFixture(typeof(ChromiumTestBrowser))]
    [TestFixture(typeof(FirefoxTestBrowser))]
    //[TestFixture(typeof(WebkitTestBrowser))] //something with webkit always times out.  Need to figure out why
    public class CounterTests <TBrowser> where TBrowser : IPlaywrightBrowser, new()
    {
        private IPlaywrightBrowser browser;
        private const string route = "counter";
        private string componentUrl;

        [SetUp]
        public async Task Setup()
        {
            componentUrl = $"{TestContext.Parameters["appUrl"]}{route}";
            browser = new TBrowser();
            await browser.Setup();
            await browser.Page.GotoAsync(componentUrl);
        }
        
        [Test]
        public void AppNavigatedToCounter()
        {
            Assert.AreEqual(componentUrl, browser.Page.Url);
        }

        [Test]
        public async Task Counter_HasPageHeader()
        {
            var headerText = await browser.Page.TextContentAsync("id=page-header");

            Assert.AreEqual("Counter", headerText);
        }

        [Test]
        public async Task Counter_CurrentCountIncrements()
        {
            var counterText = await browser.Page.TextContentAsync("id=current-count");

            //Initial value when the page loads should be 0
            Assert.AreEqual("Current count: 0", counterText);

            //Clicking the count button should increment the counter text
            await browser.Page.ClickAsync("id=btn-count");

            //Get the updated text
            counterText = await browser.Page.TextContentAsync("id=current-count");

            //Value should 1 after a single click
            Assert.AreEqual("Current count: 1", counterText);
        }
    }
}
