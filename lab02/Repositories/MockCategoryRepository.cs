using WebsiteBanHang.Models;  // để biết Category
using System.Collections.Generic;

namespace WebsiteBanHang.Repositories
{
    public class MockCategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categoryList;

        public MockCategoryRepository()
        {
            _categoryList = new List<Category>
            {
                new Category { Id = 1, Name = "Laptop" },
                new Category { Id = 2, Name = "Desktop" },
            };
        }

        public IEnumerable<Category> GetAllCategories()
            => _categoryList;
    }
}
