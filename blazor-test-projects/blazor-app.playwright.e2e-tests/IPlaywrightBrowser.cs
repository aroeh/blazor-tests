using Microsoft.Playwright;
using System.Threading.Tasks;

namespace blazor_app.playwright.e2e_tests
{
    public interface IPlaywrightBrowser
    {
        IPlaywright PlaywrightDriver { get; }

        IBrowser Browser { get; }

        IBrowserContext Context { get; }

        IPage Page { get; }

        BrowserTypeLaunchOptions LaunchOptions { get; }

        Task Setup();
    }
}
