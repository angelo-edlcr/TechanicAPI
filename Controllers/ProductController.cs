using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechanicAPI.Models;
using TechanicAPI.Repository;


namespace TechanicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;
        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Product entity)
        {
            return Ok(await _productRepository.Insert(entity));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product entity)
        {
            return Ok(await _productRepository.Update(entity));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productRepository.GetById(id));
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _productRepository.Delete(id));
        }
    }
}
