using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.Models;

namespace ProductApi.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product Create(Product product);
        Product Update(int id, Product product);
        bool Delete(int id);
        decimal CalculateTotal(Product product);
    }
    
}