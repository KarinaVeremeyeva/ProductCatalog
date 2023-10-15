using ProductCatalog.Web;
using ProductCatalog.Web.Services;

var builder = WebApplication.CreateBuilder(args);
var webUriString = builder.Configuration.GetSection("UriSettings:WebApiUri").Value;

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IUserApiService, UserApiService>(client =>
{
    client.BaseAddress = new Uri(webUriString);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IProductApiService, ProductApiService>(client =>
{
    client.BaseAddress = new Uri(webUriString);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<ICategoryApiService, CategoryApiService>(client =>
{
    client.BaseAddress = new Uri(webUriString);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddAutoMapper(typeof(WebMappingProfile));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
