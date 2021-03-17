using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsUnlimited.CartShared.Entities
{
    public class CartItem
    {
        public string SessionId { get; set; }
        public string CarId { get; set; }
        public int Count { get; set; }
    }
}
