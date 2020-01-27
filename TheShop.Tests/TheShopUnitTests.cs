using Moq;
using NUnit.Framework;
using TheShop.Infrastructure;

namespace TheShop.Tests
{
    public class TheShopUnitTests
    {
        [Test]
        public void PrintExistingArticleTest()
        {

                Mock<Logger> logger = new Mock<Logger>();
                Mock<DbDriver> dbDriver = new Mock<DbDriver>();

                ShopService shopService = new ShopService(dbDriver.Object, logger.Object);

                var consoleOutput = new ConsoleOutput();
                var articleId = 2;
                var expectedOutput = "Info: Found article with id: " + articleId + ".Article name is: Article 2 from supplier 2";

                shopService.PrintArticle(articleId);

                Assert.AreEqual(expectedOutput, consoleOutput.GetOutput());
        }

        [Test]
        public void PrintNonExistingArticleTest()
        {
            Mock<Logger> logger = new Mock<Logger>();
            Mock<DbDriver> dbDriver = new Mock<DbDriver>();

            ShopService shopService = new ShopService(dbDriver.Object, logger.Object);

            var consoleOutput = new ConsoleOutput();
            var articleId = 7;
            var expectedOutput = "Info: Could not find article with id: " + articleId;

            shopService.PrintArticle(articleId);

            Assert.AreEqual(expectedOutput, consoleOutput.GetOutput());
        }

        [Test]
        public void OrderAndSellArticleTest()
        {
            Mock<Logger> logger = new Mock<Logger>();
            Mock<DbDriver> dbDriver = new Mock<DbDriver>();

            ShopService shopService = new ShopService(dbDriver.Object, logger.Object);

            var consoleOutput = new ConsoleOutput();
            var articleId = 1;
            var maxExpectedPrice = 500;
            var expectedPrice = 457;
            var buyerId = 10;
            string[] expectedOutput = {"Debug: Trying to find cheapest available article with id = " + articleId,
                                       "Info: Found article with id = " + articleId,
                                       "Debug: Trying to sell article with id = " + articleId,
                                       "Info: Article with id = " + articleId + " has been sold for the price of: " + expectedPrice};

            shopService.OrderArticle(articleId, maxExpectedPrice, buyerId);

            string[] actualOutput = consoleOutput.GetOutput().Replace("\n","").Split('\r');

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void OrderAndSellNonExistingArticleTest()
        {
            Mock<Logger> logger = new Mock<Logger>();
            Mock<DbDriver> dbDriver = new Mock<DbDriver>();

            ShopService shopService = new ShopService(dbDriver.Object, logger.Object);

            var consoleOutput = new ConsoleOutput();
            var articleId = 8;
            var maxExpectedPrice = 500;
            var buyerId = 10;
            string[] expectedOutput = {"Debug: Trying to find cheapest available article with id = " + articleId,
                                       "Info: Could not find available article with id = " + articleId};

            shopService.OrderArticle(articleId, maxExpectedPrice, buyerId);

            string[] actualOutput = consoleOutput.GetOutput().Replace("\n", "").Split('\r');

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void OrderAndSellArticleMoreExpensiveThanMaxPriceTest()
        {
            Mock<Logger> logger = new Mock<Logger>();
            Mock<DbDriver> dbDriver = new Mock<DbDriver>();

            ShopService shopService = new ShopService(dbDriver.Object, logger.Object);

            var consoleOutput = new ConsoleOutput();
            var articleId = 1;
            var maxExpectedPrice = 300;
            var buyerId = 10;
            string[] expectedOutput = {"Debug: Trying to find cheapest available article with id = " + articleId,
                                       "Info: Could not find available article with id = " + articleId};

            shopService.OrderArticle(articleId, maxExpectedPrice, buyerId);

            string[] actualOutput = consoleOutput.GetOutput().Replace("\n", "").Split('\r');

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void OrderAndSellArticleTwoTimesTest()
        {
            Mock<Logger> logger = new Mock<Logger>();
            Mock<DbDriver> dbDriver = new Mock<DbDriver>();

            ShopService shopService = new ShopService(dbDriver.Object, logger.Object);

            var consoleOutput = new ConsoleOutput();
            var articleId = 2;
            var maxExpectedPrices = 500;
            var expectedPrice = 459;
            var buyerId = 10;
            string[] expectedOutput = {"Debug: Trying to find cheapest available article with id = " + articleId,
                                       "Info: Found article with id = " + articleId,
                                       "Debug: Trying to sell article with id = " + articleId,
                                       "Info: Article with id = " + articleId + " has been sold for the price of: " + expectedPrice,
                                       "Debug: Trying to find cheapest available article with id = " + articleId,
                                       "Info: Could not find available article with id = " + articleId};

            shopService.OrderArticle(articleId, maxExpectedPrices, buyerId);
            shopService.OrderArticle(articleId, maxExpectedPrices, buyerId);

            string[] actualOutput = consoleOutput.GetOutput().Replace("\n", "").Split('\r');

            Assert.AreEqual(expectedOutput, actualOutput);
        }

    }
}
