namespace blazor_app.playwright.e2e_tests.Tests
{
    [TestFixture(typeof(EdgeBrowser))]
    [TestFixture(typeof(ChromeBrowser))]
    [TestFixture(typeof(FirefoxBrowser))]
    public class CounterTests<TBrowser> where TBrowser : IPlaywrightBrowser, new()
    {
        private IPlaywrightBrowser browser;
        private const string route = "counter";
        private string componentUrl = string.Empty;

        [OneTimeSetUp]
        public async Task Setup()
        {
            bool headlessBrowser;
            bool.TryParse(TestContext.Parameters["headless"], out headlessBrowser);
            componentUrl = $"{TestContext.Parameters["appUrl"]}{route}";
            browser = new TBrowser();
            await browser.Setup(headlessBrowser);
            await browser.Page.GotoAsync(componentUrl);
        }

        [Test, Order(1)]
        public async Task AppNavigatedToCounter()
        {
            Assert.AreEqual(componentUrl, browser.Page.Url);
        }

        [Test, Order(2)]
        public async Task Counter_HasPageHeader()
        {
            var headerText = await browser.Page.TextContentAsync("id=page-header");

            Assert.AreEqual("Counter", headerText);
        }

        [Test, Order(3)]
        public async Task Counter_CountShouldStartAtZero()
        {
            var counterText = await browser.Page.TextContentAsync("id=current-count");

            //Initial value when the page loads should be 0
            Assert.AreEqual("Current count: 0", counterText);
        }

        [TestCase(4)] //Both test cases get run in the same session, so the first test case run counts as a click here
        public async Task Counter_CurrentCountIncrements(int numberOfClicks)
        {
            for (int i = 0; i < numberOfClicks; i++)
            {
                //loop through the test case number of clicks
                //Clicking the count button should increment the counter text
                await browser.Page.ClickAsync("id=btn-count");
            }

            var counterText = await browser.Page.TextContentAsync("id=current-count");

            //Initial value when the page loads should be 0
            Assert.AreEqual($"Current count: {numberOfClicks}", counterText);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await browser.Page.CloseAsync();
            await browser.Context.CloseAsync();
            await browser.Browser.CloseAsync();
            await browser.Browser.DisposeAsync();
            browser.PlaywrightDriver.Dispose();
        }
    }
}
