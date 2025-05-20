using WebsiteBanHang.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. ??ng ký MVC
builder.Services.AddControllersWithViews();

// 2. ??ng ký 2 repository c?a b?n vào DI container
builder.Services.AddSingleton<IProductRepository, MockProductRepository>();
builder.Services.AddSingleton<ICategoryRepository, MockCategoryRepository>();

var app = builder.Build();

// 3. C?u hình middleware (gi? Https n?u có, ho?c b? n?u không c?n)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// N?u b?n không dùng [Authorize], có th? comment dòng này
// app.UseAuthorization();

// 4. ??nh ngh?a route m?c ??nh
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}"
);

app.Run();
