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
        public void Order_WithNullItems_ThrowsEx ()
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

        [Fact]
        public void GetItem_WithExistingItem_ReturnsItem()
        {
            Order order = new(1, new[]
            {
                new OrderItem(1, 1, 3m),
                new OrderItem(3, 2, 2m)
            });

            Assert.Equal(2m, order.GetItem(3).Price);
        }

        [Fact]
        public void GetItem_WithNonExistingItem_ThrowsEx()
        {
            Order order = new(1, new[]
            {
                new OrderItem(1, 1, 3m),
                new OrderItem(3, 2, 2m)
            });

            Assert.Throws<InvalidOperationException>(() =>  order.GetItem(2));
        }

        [Fact]
        public void AddOrUpdateItem_WithNull_ThrowsEx()
        {
            Order order = new(1, new[]
            {
                new OrderItem (1, 3, 3m),
                new (2, 5, 4m)
            });

            Book book = null;

            Assert.Throws<ArgumentNullException>(() => order.AddOrUpdateItem(book, 1));
        }

        [Fact]
        public void AddOrUpdateItem_WithNonExistingItem_AddNewItem()
        {
            Order order = new(1, new[]
            {
                new OrderItem (1, 3, 3m),
                new (2, 5, 4m)
            });

            Book book = new Book(3, "isbn", "author", "title", "des", 8m);

            order.AddOrUpdateItem(book, 1);

            Assert.Equal(8m, order.GetItem(3).Price);
        }

        [Fact]
        public void AddOrUpdateItem_WithExistingItem_IncreaseCount()
        {
            Order order = new(1, new[]
            {
                new OrderItem (1, 3, 3m),
                new (2, 5, 4m)
            });

            Book book = new Book(1, "isbn", "author", "title", "des", 0m);

            order.AddOrUpdateItem(book, 1);

            Assert.Equal(4, order.GetItem(1).Count);
        }

        [Fact]
        public void RemoveItem_WithExistingItem_RemovesItem()
        {
            Order order = new(1, new[]
            {
                new OrderItem (1, 3, 3m),
                new (2, 5, 4m)
            });

            order.RemoveItem(2);

            Assert.Throws<InvalidOperationException>(() => order.GetItem(2));
        }

        [Fact]
        public void RemoveItem_WithNonExistingItem_ThrowsEx()
        {
            Order order = new(1, new[]
            {
                new OrderItem (1, 3, 3m),
                new (2, 5, 4m)
            });

            Assert.Throws<InvalidOperationException>(() => order.RemoveItem(3));
        }
    }
}
