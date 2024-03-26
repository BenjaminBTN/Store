using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Order
    {
        public int Id { get; }
        private List<OrderItem> items;
        public IReadOnlyCollection<OrderItem> Items => items;
        public int TotalCount => items.Sum(item => item.Count);
        public decimal TotalPrice => items.Sum(item => item.Price * item.Count);

        //конструктор
        public Order(int id, IEnumerable<OrderItem> items)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            Id = id;
            this.items = new List<OrderItem>(items);
        }

        //получение позиции заказа
        public OrderItem GetItem(int bookId)
        {
            var item = items.SingleOrDefault(item => item.BookId == bookId);

            if(item == null)
                ThrowBookException("Книга не найдена", bookId);

            return item;
        }

        //добавление позиции в заказ
        public void AddOrUpdateItem(Book book, int count)
        {
            if(book  == null) 
                throw new ArgumentNullException(nameof(book));

            var index = items.FindIndex(item => item.BookId == book.Id);

            if(index == -1)
                items.Add(new OrderItem(book.Id, count, book.Price));
            else
                items[index].Count += count;
        }

        //удаление всей позиции
        public void RemoveItem(int bookId)
        {
            var index = items.FindIndex(item => item.BookId == bookId);

            if(index == -1)
                ThrowBookException("Позиция заказа не найдена", bookId);

            items.RemoveAt(index);
        }

        //метод выбрасывания исключений
        private void ThrowBookException(string message, int bookId)
        {
            var exception = new InvalidOperationException(message);
            exception.Data["BookId"] = bookId;

            throw exception;
        }
    }
}
