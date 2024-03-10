using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests
{
    public class OrderItemTests
    {
        [Fact]
        public void OrderItem_WithZeroCount_ThrowEx()
        {
            int count = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() => new OrderItem(1, count, 0m));
        }

        [Fact]
        public void OrderItem_WithNegativeCount_ThrowEx()
        {
            int count = -1;
            Assert.Throws<ArgumentOutOfRangeException>(() => new OrderItem(1, count, 0m));
        }

        [Fact]
        public void OrderItem_WithPozitiveCount_SetParams()
        {
            int count = 2;
            var item = new OrderItem(1, count, 3m);

            Assert.Equal(count, item.Count);
            Assert.Equal(1, item.BookId);
            Assert.Equal(3m, item.Price);
        }
    }
}
