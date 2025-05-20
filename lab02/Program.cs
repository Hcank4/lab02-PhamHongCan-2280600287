using WebsiteBanHang.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. ??ng k� MVC
builder.Services.AddControllersWithViews();

// 2. ??ng k� 2 repository c?a b?n v�o DI container
builder.Services.AddSingleton<IProductRepository, MockProductRepository>();
builder.Services.AddSingleton<ICategoryRepository, MockCategoryRepository>();

var app = builder.Build();

// 3. C?u h�nh middleware (gi? Https n?u c�, ho?c b? n?u kh�ng c?n)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// N?u b?n kh�ng d�ng [Authorize], c� th? comment d�ng n�y
// app.UseAuthorization();

// 4. ??nh ngh?a route m?c ??nh
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}"
);

app.Run();
