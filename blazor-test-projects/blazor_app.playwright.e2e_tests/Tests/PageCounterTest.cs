using Microsoft.Playwright;

namespace blazor_app.playwright.e2e_tests.Tests
{
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
            // Test in a number of ways following Playwright best practices

            // Test getting element by Aria Role
            await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Counter"})).ToBeVisibleAsync();

            // Test getting by data-testid
            await Expect(Page.GetByTestId("page-header")).ToHaveTextAsync("Counter");
        }

        [Test, Order(3)]
        public async Task Counter_CountShouldStartAtZero()
        {
            //Initial value when the page loads should be 0
            var locator = Page.GetByTestId("current-count");
            await Expect(locator).ToHaveTextAsync("Current count: 0");
        }

        [TestCase(4)] //Both test cases get run in the same session, so the first test case run counts as a click here
        public async Task Counter_CurrentCountIncrements(int numberOfClicks)
        {
            for (int i = 0; i < numberOfClicks; i++)
            {
                //loop through the test case number of clicks
                //Clicking the count button should increment the counter text
                await Page.GetByTestId("btn-count").ClickAsync();
            }

            var locator = Page.GetByTestId("current-count");
            await Expect(locator).ToHaveTextAsync($"Current count: {numberOfClicks}");
        }
    }
}
