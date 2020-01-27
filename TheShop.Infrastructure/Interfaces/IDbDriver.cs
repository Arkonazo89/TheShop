using TheShop.Core;

namespace TheShop.Infrastructure
{
    public interface IDbDriver
    {
        Article GetArticle(int articleId);
        Article GetArticleWithMaxExpectedPriceInInventory(int articleId, int maxExpectedPrice);
        void SaveArticle(Supplier s, Article a);
        void SaveSupplier(Supplier s);
        int SellArticle(Article a, int buyerUserId);
    }
}