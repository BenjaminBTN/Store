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

        public IActionResult AddItem(int id)
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

            var book = bookRepository.GetById(id);
            order.AddItem(book, 1);
            orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;
            HttpContext.Session.Set(cart);

            return RedirectToAction("Index", "Book", new { id });
        }

        public OrderModel Map(Order order)
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
    }
}
