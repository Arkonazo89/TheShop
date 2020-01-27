using TheShop.Infrastructure;

namespace TheShop.App
{
    public class Application : IApplication
    {
        IShopService shopService;
        public Application(IShopService shopService)
        {
            this.shopService = shopService;
        }

        public void Run()
        {
            //order and sell
            shopService.OrderArticle(1, 500, 10);

            //print existing article on console
            shopService.PrintArticle(2);

            //try to print non existing article on console
            shopService.PrintArticle(12);
        }
    }
}
