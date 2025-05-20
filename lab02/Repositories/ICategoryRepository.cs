using System.Collections.Generic;
using WebsiteBanHang.Models;    // Namespace chứa class Category

namespace WebsiteBanHang.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
    }
}
