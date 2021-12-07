using Microsoft.Playwright;
using System.Threading.Tasks;

namespace blazor_app.playwright.e2e_tests
{
    public class FirefoxTestBrowser : IPlaywrightBrowser
    {
        public IPlaywright PlaywrightDriver { get; set; }

        public IBrowser Browser { get; set; }

        public IPage Page { get; set; }

        public BrowserTypeLaunchOptions LaunchOptions => new()
        {
            Headless = false,
            SlowMo = 50
        };

        public async Task Setup()
        {
            PlaywrightDriver = await Playwright.CreateAsync();
            Browser = await PlaywrightDriver.Firefox.LaunchAsync();
            Page = await Browser.NewPageAsync();
        }
    }
}
