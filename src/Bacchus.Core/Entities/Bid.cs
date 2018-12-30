using System;

namespace Bacchus.Core.Entities
{
    public class Bid
    {

        public int Id { get; set; }
        public String Username { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Price { get; set; }

        public Bid()
        {
        }

        public Bid(String username, Guid productId, DateTime createdOn, decimal price)
        {
            this.Username = username;
            this.ProductId = productId;
            this.CreatedOn = createdOn;
            this.Price = price;
        }
    }
}