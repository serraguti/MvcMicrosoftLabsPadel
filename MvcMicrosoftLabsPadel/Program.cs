using Microsoft.EntityFrameworkCore;
using MvcMicrosoftLabsPadel.Data;
using MvcMicrosoftLabsPadel.Repositories;
using MvcMicrosoftLabsPadel.Services;

var builder = WebApplication.CreateBuilder(args);

//AÑADIMOS EL REPOSITORIO
builder.Services.AddTransient<RepositoryUsuarios>();
//AÑADIMOS EL SERVICIO AZURE STORAGE
builder.Services.AddTransient<ServiceAzureStorage>();
//AÑADIMOS EL SERVICIO AZURE LOGIC APPS
builder.Services.AddTransient<ServiceLogicApps>();
//RECUPERAMOS LA CADENA DE CONEXION
string connectionString =
    builder.Configuration.GetConnectionString("AzureSqlServer");
//INYECTAMOS EL DBCONTEXT DENTRO DE LOS SERVICIOS
builder.Services.AddDbContext<UsuariosContext>
    (options => options.UseSqlServer(connectionString));
// Add services to the container.
builder.Services.AddControllersWithViews();
//LA INYECCION ANTES DE ESTA LINEA
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
