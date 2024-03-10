using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Order_WithNullItems_ThrowEx ()
        {
            Assert.Throws<ArgumentNullException>(() => new Order(1, null));
        }

        [Fact]
        public void TotalCount_WithEmptyItems_ReturnsZero()
        {
            var order = new Order(1, new OrderItem[0]);
            Assert.Equal(0, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithEmptyItems_ReturnsZero()
        {
            var order = new Order(1, new OrderItem[0]);
            Assert.Equal(0m, order.TotalPrice);
        }

        [Fact]
        public void TotalCount_WithNonEmptyItems_CalculateTotalCount()
        {
            var item1 = new OrderItem(1, 2, 10m);
            var item2 = new OrderItem(2, 3, 50m);

            var order = new Order(1, new OrderItem[]
            {
                item1, item2
            });

            Assert.Equal(2 + 3, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithNonEmptyItems_CalculateTotalPrice()
        {
            var item1 = new OrderItem(1, 2, 10m);
            var item2 = new OrderItem(2, 3, 50m);

            var order = new Order(1, new OrderItem[]
            {
                item1, item2
            });

            Assert.Equal((2 * 10) + (3 * 50), order.TotalPrice);
        }
    }
}
