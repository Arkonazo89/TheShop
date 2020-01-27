using System;

namespace TheShop.Core
{
    public class Article
    {
        public int Id;
        public string Name;
        public int Price;
        public bool IsSold;
        public DateTime? SoldDate;
        public int? BuyerUserId;

        public Article(int id, string name, int price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }
    }
}
