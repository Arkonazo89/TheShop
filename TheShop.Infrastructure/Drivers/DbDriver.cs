using System.Collections.Generic;
using System.Linq;
using System;
using TheShop.Core;

namespace TheShop.Infrastructure
{
    public class DbDriver : IDbDriver
    {
        private List<Supplier> suppliers = new List<Supplier>();

        public void SaveSupplier(Supplier s)
        {
            suppliers.Add(s);           
        }

        public void SaveArticle(Supplier s, Article a)
        {
            s.Articles.Add(a);
        }

        public Article GetArticleWithMaxExpectedPriceInInventory(int articleId, int maxExpectedPrice)
        {
            var articles = suppliers.SelectMany(s => s.Articles).
                                     Where(a => a.Id == articleId && 
                                           a.Price <= maxExpectedPrice && 
                                           !a.IsSold);

            return articles.Where(a => a.Price == articles.Min(r => r.Price)).FirstOrDefault();
        }

        public Article GetArticle(int articleId)
        {
            return suppliers.SelectMany(s => s.Articles).
                             Distinct().
                             FirstOrDefault(a => a.Id == articleId);
        }

        public int SellArticle(Article a, int buyerUserId)
        {
            a.IsSold = true;
            a.SoldDate = DateTime.Now;
            a.BuyerUserId = buyerUserId;

            return a.Price;   
        }
    }
}
