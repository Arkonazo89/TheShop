using TheShop.Core;

namespace TheShop.Infrastructure
{
    public interface IShopService
    {
        void OrderArticle(int articleId, int maxExpectedPrice, int buyerId);

        void SellArticle(Article article, int buyerId);
        void PrintArticle(int articleId);
    }
}