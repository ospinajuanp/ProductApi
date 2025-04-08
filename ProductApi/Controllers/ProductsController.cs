using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<PagedResult<Product>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var allProducts = _productService.GetAll().ToList();

            var totalItems = allProducts.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedProducts = allProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedResult<Product>
            {
                Data = pagedProducts,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };

            return Ok(result);
        }


        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> Create([FromBody] Product product)
        {
            var created = _productService.Create(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public ActionResult<Product> Update(int id, [FromBody] Product product)
        {
            var updated = _productService.Update(id, product);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!_productService.Delete(id))
                return NotFound();

            return NoContent();
        }

        // GET: api/products/total/{id}
        // Endpoint para calcular el total del producto (aplica descuento según la regla de negocio)
        [HttpGet("total/{id}")]
        public ActionResult<decimal> GetTotal(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();

            var total = _productService.CalculateTotal(product);
            return Ok(total);
        }
    }
}