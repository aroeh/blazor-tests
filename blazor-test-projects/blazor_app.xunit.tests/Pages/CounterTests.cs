using blazor_app.Pages;

namespace blazor_app.xunit.tests.Pages
{
    public class CounterTests
    {
        private const string counter = "Counter";
        private const string currentCount = "Current count: ";

        [Fact]
        public void Counter_H1Header_MarkupMatchesCounter()
        {
            //Arrange
            using var ctx = new TestContext();

            //Example Mock Service
            //ctx.Services.AddSingleton<IWeatherForecaseService>(new WeatherForecastService);

            var cut = ctx.RenderComponent<Counter>();

            //Act
            //Can find an element in a couple of ways: by element ex h1, or by id which requires #<id>
            var paramElem = cut.Find("#page-header");
            var paramElemText = paramElem.TextContent;

            //Assert
            paramElemText.MarkupMatches(counter);
        }

        [Fact]
        public void Counter_InitialValue_Is0()
        {
            //Arrange
            var startingCount = 0;
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<Counter>();

            //Act
            var paramElem = cut.Find("#current-count");
            var paramElemText = paramElem.TextContent;

            //Assert
            paramElemText.MarkupMatches($"{currentCount}{startingCount}");
        }

        [Fact]
        public void Counter_IncrementBy1_MarkupMatches()
        {
            //Arrange
            var numberOfClicks = 1;
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<Counter>();

            //You can get the instance of the component and gain access to the public properties and methods
            //cut.Instance.<InstanceObject>  ex: cut.Instance.currentCount  if currentCount is public

            //Act
            var paramElem = cut.Find("#current-count");
            cut.Find("#btn-count").Click();
            var paramElemText = paramElem.TextContent;

            //Assert
            paramElemText.MarkupMatches($"{currentCount}{numberOfClicks}");
        }
    }
}
