using System;
using TheShop.Core;

namespace TheShop.Infrastructure
{
    public class ShopService : IShopService
    {
        private Supplier supplier1;
        private Supplier supplier2;
        private Supplier supplier3;

        private Article article1;
        private Article article2;
        private Article article3;
        private Article article4;

        ILogger  logger;
        IDbDriver dbDriver;

        public ShopService(IDbDriver dbDriver, ILogger logger)
        {
            this.dbDriver = dbDriver;
            this.logger = logger;

            article1 = new Article(1, "Article 1 from supplier 1", 458);
            article2 = new Article(2, "Article 2 from supplier 2", 459);
            article3 = new Article(3, "Article 3 from supplier 3", 460);
            article4 = new Article(1, "Article 1 from supplier 3", 457);

            supplier1 = new Supplier(1);
            dbDriver.SaveSupplier(supplier1);
            dbDriver.SaveArticle(supplier1, article1);

            supplier2 = new Supplier(2);
            dbDriver.SaveSupplier(supplier2);
            dbDriver.SaveArticle(supplier2, article2);

            supplier3 = new Supplier(3);
            dbDriver.SaveSupplier(supplier3);
            dbDriver.SaveArticle(supplier3, article3);
            dbDriver.SaveArticle(supplier3, article4);

        }

        public void OrderArticle(int articleId, int maxExpectedPrice, int buyerId)
        {
            Article article = null;

            #region ordering article

            try
            {
                logger.Debug("Trying to find cheapest available article with id = " + articleId);
                article = dbDriver.GetArticleWithMaxExpectedPriceInInventory(articleId, maxExpectedPrice);
            }
            catch (Exception ex)
            {
                logger.Error("Could not retrieve article. Exception occured: " + ex);
            }

            #endregion

            #region selling article

            if (article == null)
            {
                logger.Info("Could not find available article with id = " + articleId);
            }
            else
            {
                logger.Info("Found article with id = " + articleId);
                SellArticle(article, buyerId);
            }
            #endregion
        }

        public void SellArticle(Article article, int buyerId)
        {
            try
            {
                logger.Debug("Trying to sell article with id = " + article.Id);
                var soldPrice = dbDriver.SellArticle(article, buyerId);
                logger.Info("Article with id = " + article.Id + " has been sold for the price of: " + soldPrice);
            }
            catch (Exception ex)
            {
                logger.Error("Could not sell article. Exception occured: " + ex);
            }
        }

        public void PrintArticle(int articleId)
        {
            Article article = null;

            try
            {
                article = dbDriver.GetArticle(articleId);
            }
            catch (Exception ex)
            {
                logger.Error("Could not find article. Exception occured: " + ex);
            }

            if (article != null)
            {
                logger.Info("Found article with id: " + articleId + ".Article name is: " + article.Name);
            }
            else
            {
                logger.Info("Could not find article with id: " + articleId);
            } 
        }
    }
}
