﻿using blazor_app.lib.Interfaces;
using Microsoft.Playwright;

namespace blazor_app.lib.Browsers
{
    public class WebkitBrowser : IPlaywrightBrowser
    {
        private IPlaywright driver;
        public IPlaywright PlaywrightDriver => driver;

        private IBrowser browser;
        public IBrowser Browser => browser;

        private IBrowserContext context;
        public IBrowserContext Context => context;

        private IPage page;
        public IPage Page => page;

        public bool RunHeadless { get; private set; }

        public BrowserTypeLaunchOptions LaunchOptions => new()
        {
            Headless = RunHeadless,
            SlowMo = 500
        };

        public BrowserNewContextOptions ContextOptions => new()
        {
            IgnoreHTTPSErrors = true
        };

        public async Task Setup(bool headless)
        {
            RunHeadless = headless;
            driver = await Playwright.CreateAsync();
            browser = await driver.Webkit.LaunchAsync(LaunchOptions);
            context = await browser.NewContextAsync(ContextOptions);
            page = await context.NewPageAsync();
        }
    }
}
