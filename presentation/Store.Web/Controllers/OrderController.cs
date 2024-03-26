using Microsoft.AspNetCore.Mvc;
using Store.Web.Models;

namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IOrderRepository orderRepository;

        public OrderController(IBookRepository bookRepository, 
                              IOrderRepository orderRepository)
        {
            this.bookRepository = bookRepository;
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if(HttpContext.Session.TryGetCart(out var cart))
            {
                var order = orderRepository.GetById(cart.OrderId);
                var model = Map(order);
                return View(model);
            }

                return View("Empty");
        }

        private OrderModel Map(Order order)
        {
            var bookIds = order.Items.Select(item => item.BookId);
            var books = bookRepository.GetAllByIds(bookIds);
            var itemModels = from item in order.Items
                             join book in books
                             on item.BookId equals book.Id
                             select new OrderItemModel
                             {
                                 BookId = book.Id,
                                 Title = book.Title,
                                 Author = book.Author,
                                 Count = item.Count,
                                 Price = item.Price
                             };

            return new OrderModel
            {
                Id = order.Id,
                Items = itemModels.ToArray(),
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice,
            };
        }


        [HttpPost]
        public IActionResult AddItem(int bookId, int count = 1)
        {
            (Order order, CartModel cart) = GetOrCreateOrderAndCart();

            var book = bookRepository.GetById(bookId);
            order.AddOrUpdateItem(book, count);
            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Book", new { id = bookId });
        }

        [HttpPost]
        public IActionResult RemoveItem(int bookId)
        {
            (Order order, CartModel cart) = GetOrCreateOrderAndCart();

            order.RemoveItem(bookId);

            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateItem(int bookId, int count)
        {
            (Order order, CartModel cart) = GetOrCreateOrderAndCart();

            var item = order.GetItem(bookId);
            item.Count = count;

            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Book", new { id = bookId });
        }

        private (Order, CartModel) GetOrCreateOrderAndCart()
        {
            CartModel cart;
            Order order;

            if(!HttpContext.Session.TryGetCart(out cart))
            {
                order = orderRepository.Create();
                cart = new CartModel(order.Id);
            }
            else
            {
                order = orderRepository.GetById(cart.OrderId);
            }

            return (order, cart);
        } 

        private void SaveOrderAndCart(Order order, CartModel cart)
        {
            orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(cart);
        }
    }
}
