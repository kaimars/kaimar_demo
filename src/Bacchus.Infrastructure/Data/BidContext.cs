using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Bacchus.Core.Entities;

namespace Bacchus.Infrastructure.Data
{
    public class BidContext: DbContext
    {
        public DbSet<Bid> Bids { get; set; }

        public BidContext(DbContextOptions<BidContext> options) : base(options)
        {
        }
    }
}