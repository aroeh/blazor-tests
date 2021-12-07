using Microsoft.Playwright;
using System.Threading.Tasks;

namespace blazor_app.playwright.e2e_tests
{
    public interface IPlaywrightBrowser
    {
        IPlaywright PlaywrightDriver { get; set; }

        IBrowser Browser { get; set; }

        IPage Page { get; set; }

        BrowserTypeLaunchOptions LaunchOptions { get; }

        Task Setup();
    }
}
