// Repositories/MockProductRepository.cs
using System.Collections.Generic;
using System.Linq;

public class MockProductRepository : IProductRepository
{
    private List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "iPhone 15", Price = 35000000, Description = "iPhone 15 Pro Max", CategoryId = 1, ImageUrl = "/images/iphone15.jpg" },
        new Product { Id = 2, Name = "iPhone 16", Price = 50000000, Description = "iPhone 16", CategoryId = 1, ImageUrl = "/images/iphone16.jpg" }
    };

    public IEnumerable<Product> GetAll() => _products;

    public Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product)
    {
        product.Id = _products.Count + 1;
        _products.Add(product);
    }

    public void Update(Product product)
    {
        var existing = GetById(product.Id);
        if (existing != null)
        {
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            existing.CategoryId = product.CategoryId;
            existing.ImageUrl = product.ImageUrl;
        }
    }

    public void Delete(int id)
    {
        var product = GetById(id);
        if (product != null) _products.Remove(product);
    }
}
