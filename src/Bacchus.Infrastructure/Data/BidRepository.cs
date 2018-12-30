using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bacchus.Core.Entities;
using Bacchus.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bacchus.Infrastructure.Data
{
    public class BidRepository: EfRepository<Bid>
    {
        public BidRepository(BidContext dbContext): base(dbContext)
        {
        }
    }
}