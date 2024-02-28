using SupermercadoMDDII.Controllers;
using SupermercadoMDDII.Dependencia;
using SupermercadoMDDII.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.InyectarDependencia(builder.Configuration);
builder.Services.AddScoped<ProductoController>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddMvc();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MyAppSession";
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

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
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.UseSession();

app.Run();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


