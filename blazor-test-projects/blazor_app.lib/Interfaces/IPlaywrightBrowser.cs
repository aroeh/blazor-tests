using Microsoft.Playwright;

namespace blazor_app.lib.Interfaces
{
    public interface IPlaywrightBrowser
    {
        /// <summary>
        /// Playwright driver instance used to control the browser
        /// </summary>
        IPlaywright PlaywrightDriver { get; }

        /// <summary>
        /// Browser instance started by the driver
        /// </summary>
        IBrowser Browser { get; }

        /// <summary>
        /// Browser context used for running tests
        /// </summary>
        IBrowserContext Context { get; }

        /// <summary>
        /// Browser Page for running apps and testing content
        /// </summary>
        IPage Page { get; }

        bool RunHeadless { get; }

        /// <summary>
        /// Launch options the driver will use
        /// </summary>
        BrowserTypeLaunchOptions LaunchOptions { get; }

        /// <summary>
        /// Customized Browser Context Options for how to handle requests, session, etc...
        /// </summary>
        BrowserNewContextOptions ContextOptions { get; }

        /// <summary>
        /// Setup method for instanting playwright instances and objects
        /// </summary>
        /// <param name="headless">Default is false</param>
        /// <returns></returns>
        Task Setup(bool headless);
    }
}
