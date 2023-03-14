# Introduction 
This project is a demonstration of learning for testing frameworks specific to Blazor.  The premise was to start with a simple UI project and add both unit and automated ui tests to the project.

## Goals
1. Learn bUnit basics
    - Libraries and dependencies
    - Project Setup
    - Gather Code Coverage
2. Learn the Playwright e2e testing framework
    - Libraries and dependencies
    - Project Setup with C# as the language of choice
    - Get automated tests running and passing
    - Cross Browser Testing
4. Bonus - Define a clean architecture and project structure for the app

# Software Dependencies
- [Visual Studio 2022](https://visualstudio.microsoft.com/) version 17.0.2 - Any edition (I used Community)
- [Visual Studio Code](https://visualstudio.microsoft.com/#vscode-section) version 1.62.3
- [Powershell](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-windows?view=powershell-7.3)
- [Microsoft Edge](https://www.microsoft.com/en-us/edge/download?form=MA13FJ)
- [Google Chrome](https://www.google.com/chrome/)
- [Mozilla Firefox](https://www.mozilla.org/en-US/)
    

# Key Libraries and Downloads
- .Net 6.0
- Playwright v1.31
- dotnet report generator

# Getting Started
1.	Ensure that you have all required tools installed - Visual Studio or VS Code
2.	Install optional tools if desired
3.	Install .Net 6.0 framework if it is not already installed on the workstation
4.  Install the latest version of the Playwright CLI: 
```
dotnet tool install --global Microsoft.Playwright.CLI
```
5.  Build the solution and all projects
6.  Run the command to install Playwright browsers and web drivers: `pwsh bin\Debug\netX\playwright.ps1 install`
```
pwsh bin\Debug\net6.0\playwright.ps1 install
```
7.  Optional - For Code Coverage Install the Report Generator:
```
dotnet tool install -g dotnet-reportgenerator-globaltool
```

All other files and libraries will be included in the repository, so there is no need to download them or set them up

# Build
The solution will build and compile after restoring packages.  Use the menu to build or the keyboard shortcut (often F6).  You can also build the project via the command line using the command
```
dotnet build
```

# Run
There are 3 projects in the solution:
- blazor-app
- blazor-app.playwright.e2e-tests
- blazor-app.xunit.unit-tests

blazor-app should be the startup project and can be run through the IDE or via command line using the command
```
dotnet run
```

## Unit Tests
Unit tests are utilizing the XUnit project template and runner adapters.  The unit test project was created using the bUnit project template

There are a few options to run the tests:
1. Using the Visual Studio Text Explorer
2. Command line
```
dotnet test
```

When writing unit tests, it is key that each test create a new instance of the text context to render the component.  There does not appear to be a way to set the context as a test class variable.

### Collect Code Coverage
The version of Visual studio will determine ways to get code coverage.  By far the easiest way is if using Visual Studio Enterprise as that has the tools and reporting built in.  If using any other version of Visual Studio the following steps will be needed for XUnit

1. Install the report generator as a global tool
```
dotnet tool install -g dotnet-reportgenerator-globaltool
```
2. In the directory containing the .sln file run the following command to generate a cobertura xml file
```
dotnet test --collect:"XPlat Code Coverage"
```
3. To generate a user friendly report run the following command
```
reportgenerator -reports:"<Path>\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

In the root directory containing the .sln file this will create a directory named coveragereport.  Inside coveragereport there will be html files that can be opened and viewed in the browser.

## End to End Tests (e2e)
e2e tests are a demonstration of the playwright framework for automated UI tests.  The app must be running for the e2e tests to work.

### Architecture and Project Considerations
The playwright project demonstrates two different approaches to automated tests and configuration for multi-browser testing.
- Custom Browser TestFixture
- Page Test and TestFixture defaults and overrides

It is recommended to choose one approach or the other for consistency

#### Custom Browser TestFixture
The file CounterTests.cs represents this approach.  At the top of the file we can see that it is tagged with the [TestFixture(typeof(Browser))] attribute.  This pattern relies on an interface to setup the browser instances in code and relies on the interface for generically running tests against different browsers.

This approach has some advantages
- Works well with multi-browser and test parameters in runsettings files
- runsettings only need to cover NUnit or test parameter values

There are some slight drawbacks
- Requires duplicate code in every test class for the browser setup and teardown
- The setup is slightly more complicated with the test class interface inheritance

#### Page Test and TestFixture defaults and overrides
The page approach appears to have been introduced around Playwright version 1.31 and is slightly newer.  This approach utilzes the default classes for Playwright as the test fixture.  It is not by default a multi-browser approach, but can be used for multi-browser testing.  In the solution, the file PageCounterTests.cs represeents this appraoch.

This approach has some advantages
- No need for custom test fixtures
- Default test fixture confiration can be overriden in runsettings or the cli
- Makes the test file more simple and removes some duplicate code

There are some slight drawbacks
- The CLI parameters for overriding test fixtures do not work with runsettings - only one or the other can be used
- The default test fixture uses Edge, which may not cover all needs
- To get multi-browser testing, the CLI must be used
- Depending on environment configuration needs, many runsettings might need to be created which could be very tedious to maintain

Example cli commands
```
dotnet test -- Playwright.BrowserName=chromium Playwright.LaunchOptions.Headless=false Playwright.LaunchOptions.Channel=msedge
```

```
dotnet test -- Playwright.BrowserName=chromium Playwright.LaunchOptions.Headless=false Playwright.LaunchOptions.Channel=chrome
```

```
dotnet test -- Playwright.BrowserName=firefox Playwright.LaunchOptions.Headless=false
```

```
dotnet test --settings=edge.runsettings
```

```
dotnet test --settings=chrome.runsettings
```

```
dotnet test --settings=firefox.runsettings
```

### Runsettings
Within Visual Studio, only the file named ".runsettings" will be auto detected and only when it is in the root directory of the solution.  In this case it is auto detected within the blazor-test-projects directory
Visual Studio must be configuted to auto Detect the file
`Test > Configure Run Settings > Auto Detect runsettings Files`

You can also manually select a different run settings file within the same menu
`Test > Configure Run Settings > Select Solution Wide runsettings File`

For more information on runsettings visit [Run Settings](https://docs.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2019#create-a-run-settings-file-and-customize-it)

Runsettings can also be set through the cli via the -s or --settings parameter of the dotnet test command.  This could be useful if needing to run the tests using a headless browser, like would be needed in a build pipeline.
`dotnet test --settings=<filename>`

```
dotnet test --settings=.runsettings
```

Visit [dotnet test cli](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test) for more information

# References
- [Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/test?view=aspnetcore-6.0)
- [bUnit Blazor Unit tests](https://bunit.dev/docs/getting-started/)
- [Playwright Automation Framework](https://playwright.dev/dotnet/docs/intro)
- [Collect Code Coverage](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows)
- [dotnet test cli](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test)