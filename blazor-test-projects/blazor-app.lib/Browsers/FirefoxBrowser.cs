using blazor_app.lib.Interfaces;
using Microsoft.Playwright;

namespace blazor_app.lib.Browsers
{
    public class FirefoxBrowser : IPlaywrightBrowser
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

        public async Task Setup(bool useOptions = false)
        {
            driver = await Playwright.CreateAsync();
            browser = useOptions
                ? await driver.Firefox.LaunchAsync(LaunchOptions)
                : await driver.Firefox.LaunchAsync();
            context = await browser.NewContextAsync(new BrowserNewContextOptions { IgnoreHTTPSErrors = true });
            page = await context.NewPageAsync();
        }
    }
}
