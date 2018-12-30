using System;

namespace Bacchus.Core.Exceptions
{
    public class AuctionNotFoundException: ApplicationException
    {
        public AuctionNotFoundException(Guid productId) : base($"No Auction found with id {productId}")
        {
        }

        protected AuctionNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public AuctionNotFoundException(string message) : base(message)
        {
        }

        public AuctionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
    }
}
