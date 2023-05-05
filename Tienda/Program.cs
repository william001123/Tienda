using Tienda.Interfaces;
using Tienda.Interfaces.Servicios;
using Tienda.Models;
using Tienda.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region "Inyeccion de dependencias"

builder.Services.AddTransient<IConfig, clsConfig>();

//Servicios
builder.Services.AddTransient<IServicioBase<clsCategoria, int>, clsCategoriaServ>();
builder.Services.AddTransient<IServicioBase<clsEmpleado, int>, clsEmpleadoServ>();

builder.Services.AddTransient<IServicioBase<clsProducto, int>, clsProductoServ>();
builder.Services.AddTransient<IServicioBase<clsTipoUnidad, int>, clsTipoUnidadServ>();
builder.Services.AddTransient<IServicioBase<clsProveedor, int>, clsProveedorServ>();
builder.Services.AddTransient<IServicioObteIDEnc<clsUsuario, int, clsUsuario>, clsUsuarioServ>();
builder.Services.AddTransient<IServicioBase<clsOrden, int>, clsOrdenServ>();
builder.Services.AddTransient<IServicioObteIDEnc<clsOrdenDetalle, int, List<clsOrdenDetalle>>, clsOrdenDetalleServ>();
builder.Services.AddTransient<IServicioBase<clsRecepcion, int>, clsRecepcionServ>();
builder.Services.AddTransient<IServicioObteIDEnc<clsRecepcionDetalle, int, List<clsRecepcionDetalle>>, clsRecepcionDetalleServ>();
builder.Services.AddTransient<IServicioVenta<clsVenta, int>, clsVentaServ>();
builder.Services.AddTransient<IServicioObteIDEnc<clsVentaDetalle, int, List<clsVentaDetalle>>, clsVentaDetalleServ>();
builder.Services.AddTransient<IServicioMovimiento<clsMovimiento, int>, clsMovimientoServ>();
builder.Services.AddTransient<IServicioMoviDeta<clsMovimientoDetalle, int, List<clsMovimientoDetalle>>, clsMovimientoDetalleServ>();

#endregion

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Venta}/{action=vVentaTemp}/{id?}");

app.Run();
