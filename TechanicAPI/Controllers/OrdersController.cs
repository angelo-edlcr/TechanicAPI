using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechanicAPI.Models;
using TechanicAPI.Repository;

namespace TechanicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<User> _userRepository;


        public OrdersController(IRepository<Order> orderRepository, IRepository<Product> productRepository, IRepository<User> userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _orderRepository.GetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _orderRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Order order)
        {
            if (await CustomerExists(order.CustomerId))
            {
                try
                {
                    if (await UpdateStock(order)) 
                    {
                        order.total= await GetTotal(order);
                        return Ok(await _orderRepository.Insert(order));
                    };
                    return Ok("La cantidad que intenta consumir excede a la disponible en el inventario");
                }
                catch (Exception)
                {
                    return Ok("Ha ocurrido un error con su orden");
                }
            }
            return Ok("Ha ocurrido un error con su orden");
        }

        [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            if (await CustomerExists(order.CustomerId))
            {
                try
                {
                    if (await UpdateStock(order)) 
                    {
                        order.total = await GetTotal(order);
                        return Ok(await _orderRepository.Update(order));
                    };
                }
                catch (Exception)
                {
                    return Ok("Ha ocurrido un error con su orden");
                }
            }

            return Ok("La cantidad que intenta consumir excede a la disponible en el inventario");
        }

        async Task<bool> CustomerExists(int id)
        {
            if (await _userRepository.GetById(id) != null) return true;

            return false;
        }

        async Task<bool> UpdateStock(Order order)
        {
            try
            {
                var product = await GetProduct(order.productId);
                if (order.quantity <= product.Stock)
                {
                    product.Stock = product.Stock - order.quantity;
                    await _productRepository.Update(product);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;

        }

        async Task<Product> GetProduct(int id)
        {
           return await _productRepository.GetById(id);
        }

        async Task<double> GetTotal(Order order)
        {
            var product = await GetProduct(order.productId);

            return product.Price * order.quantity;
        }

    }
}
