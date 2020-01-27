using System.Collections.Generic;

namespace TheShop.Core
{
    public class Supplier
    {
        public int Id;
        public List<Article> Articles;

        public Supplier(int id)
        {
            Id = id;
            Articles = new List<Article>();
        }
    }
}
