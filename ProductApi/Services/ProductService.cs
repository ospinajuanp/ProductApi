using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.Models;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        // Simulación de un repositorio en memoria
        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;

        public IEnumerable<Product> GetAll() => _products;

        public Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public Product Create(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
            return product;
        }

        public Product Update(int id, Product product)
        {
            var existing = GetById(id);
            if (existing == null) return null;

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Quantity = product.Quantity;
            return existing;
        }

        public bool Delete(int id)
        {
            var product = GetById(id);
            if (product == null) return false;
            return _products.Remove(product);
        }

        public decimal CalculateTotal(Product product)
        {
            // Total sin descuento
            decimal total = product.Price * product.Quantity;

            if (product.Quantity > 10)
            {
                total -= total * 0.10m; // Aplica 10% de descuento
            }

            return total;
        }
    }
}