using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GrupoAContext>();
builder.Services.AddScoped(typeof(IGenericRepositorio<>), typeof(EfGenericRepositorio<>));
builder.Services.AddControllersWithViews(); // Adding controller services with views to the application. 
builder.Services.AddAuthorization(); // Adding authorization services to the application

// Build application.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();