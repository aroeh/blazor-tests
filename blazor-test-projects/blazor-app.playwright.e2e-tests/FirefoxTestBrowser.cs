using Microsoft.Playwright;
using System.Threading.Tasks;

namespace blazor_app.playwright.e2e_tests
{
    public class FirefoxTestBrowser : IPlaywrightBrowser
    {
        private IPlaywright driver;
        public IPlaywright PlaywrightDriver => driver;

        private IBrowser browser;
        public IBrowser Browser => browser;

        private IBrowserContext context;
        public IBrowserContext Context => context;

        private IPage page;
        public IPage Page => page;

        public BrowserTypeLaunchOptions LaunchOptions => new()
        {
            Headless = false,
            SlowMo = 50
        };

        public async Task Setup()
        {
            driver = await Playwright.CreateAsync();
            browser = await driver.Firefox.LaunchAsync();
            context = await browser.NewContextAsync(new BrowserNewContextOptions { IgnoreHTTPSErrors = true });
            page = await context.NewPageAsync();
        }
    }
}
