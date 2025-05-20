// Models/Product.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    public string Description { get; set; } = string.Empty;

    [Display(Name = "Danh mục")]
    [Required(ErrorMessage = "Bạn phải chọn danh mục")]
    public int CategoryId { get; set; }

    // Navigation property không bắt buộc
    public Category? Category { get; set; }

    [Display(Name = "Ảnh sản phẩm")]
    public string? ImageUrl { get; set; }
}

