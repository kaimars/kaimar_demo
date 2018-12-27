using System;

namespace Bacchus.Core.Entities
{
    public class Bid
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}