using blazor_app.Pages;
using Bunit;
using Xunit;

namespace blazor_app.xunit.unit_tests.Pages
{
    public class CounterTests
    {
        private const string h1Elem = "h1";
        private const string pElem = "p";
        private const string counter = "Counter";
        private const string currentCount = "Current count: ";

        [Fact]
        public void Counter_H1Header_MarkupMatchesCounter()
        {
            //Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<Counter>();

            //Act
            var paramElem = cut.Find(h1Elem);
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
            var paramElem = cut.Find(pElem);
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

            //Act
            var paramElem = cut.Find(pElem);
            cut.Find("button").Click();
            var paramElemText = paramElem.TextContent;

            //Assert
            paramElemText.MarkupMatches($"{currentCount}{numberOfClicks}");
        }
    }
}
