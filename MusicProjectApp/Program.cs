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
/*
 * it can have multiple meanings, but in general what they probably
 * mean is a CI/CD pipeline which is sort of a process where code goes
 * from repository all the way to production, meaning there is a tool
 * that takes the code, builds it, runs tests, then some other tool may
 * be runs other tests and another tool deploys the code to the cloud,
 * or to your own server. And this set of tools and processes is called a pipeline.
 */
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