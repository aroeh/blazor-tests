using Microsoft.Playwright;

namespace blazor_app.playwright.e2e_tests.Tests
{
    /// <summary>
    /// The page test is available in newer versions of Playwright
    /// It relies on the testFixture objects to be set either in the .runsettings or via environment variables in the command line
    /// For multi-browser support you will need to run the test each time
    /// either pointing to a different browser specific runsettings file or specifying in the cli
    /// dotnet test -- Playwright.BrowserName=chromium Playwright.LaunchOptions.Headless=false Playwright.LaunchOptions.Channel=msedge
    /// dotnet test -- Playwright.BrowserName=chromium Playwright.LaunchOptions.Headless=false Playwright.LaunchOptions.Channel=chrome
    /// dotnet test -- Playwright.BrowserName=firefox Playwright.LaunchOptions.Headless=false
    /// 
    /// The best practice is to choose one approach and not multiple
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class PageCounterTest : PageTest
    {
        private const string route = "counter";
        private readonly string componentUrl = $"{TestContext.Parameters["appUrl"]}{route}";

        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions
            {
                BaseURL = TestContext.Parameters["appUrl"],
                IgnoreHTTPSErrors = true
            };
        }

        [SetUp]
        public async Task Setup()
        {
            await Page.GotoAsync(route);
        }

        [Test, Order(1)]
        public async Task AppNavigatedToCounter()
        {
            //The Expect API is only available with the test class inherits from PageTest
            await Expect(Page).ToHaveURLAsync(componentUrl);
        }

        [Test, Order(2)]
        public async Task Counter_HasPageHeader()
        {
            var locator = Page.Locator("id=page-header");
            await Expect(locator).ToHaveTextAsync("Counter");
        }

        [Test, Order(3)]
        public async Task Counter_CountShouldStartAtZero()
        {
            //Initial value when the page loads should be 0
            var locator = Page.Locator("id=current-count");
            await Expect(locator).ToHaveTextAsync("Current count: 0");
        }

        [TestCase(4)] //Both test cases get run in the same session, so the first test case run counts as a click here
        public async Task Counter_CurrentCountIncrements(int numberOfClicks)
        {
            for (int i = 0; i < numberOfClicks; i++)
            {
                //loop through the test case number of clicks
                //Clicking the count button should increment the counter text
                await Page.ClickAsync("id=btn-count");
            }

            var locator = Page.Locator("id=current-count");
            await Expect(locator).ToHaveTextAsync($"Current count: {numberOfClicks}");
        }
    }
}
