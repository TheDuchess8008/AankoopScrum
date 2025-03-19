using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Repositories;
using PrulariaAankoopService.Services;
using PrulariaAankoopUI;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddDbContext<PrulariaComContext>(
        options => options.UseMySQL(
         builder.Configuration.GetConnectionString("PrulariaComConnection"),
                           x => x.MigrationsAssembly("PrulariaAankoopData")));

builder.Services.AddScoped<ArtikelenService>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ActiecodesService>();
builder.Services.AddTransient<IActiecodesRepository, SQLActiecodesRepository>();
builder.Services.AddTransient<ArtikelenService>();
builder.Services.AddTransient<IArtikelenRepository, SQLArtikelenRepository>();
builder.Services.AddTransient<CategorieenService>();
builder.Services.AddTransient<ICategorieenRepository, SQLCategorieenRepository>();
builder.Services.AddTransient<LeveranciersService>();
builder.Services.AddTransient<ILeveranciersRepository, SQLLeveranciersRepository>();
builder.Services.AddTransient<SecurityService>();
builder.Services.AddTransient<ISecurityRepository, SQLSecurityRepository>();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo> {
                    new CultureInfo("nl-BE"),
                };
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.DefaultRequestCulture = new RequestCulture("nl-BE");

});

builder.Services.AddLocalization();
builder.Services.AddControllersWithViews((options) =>
{
    options.ModelBinderProviders.Insert(0, new CustomBinderProvider());
});

builder.Services.AddTransient<ActiecodesService>();
builder.Services.AddTransient<IActiecodesRepository, SQLActiecodesRepository>();
builder.Services.AddTransient<ArtikelenService>();
builder.Services.AddTransient<IArtikelenRepository, SQLArtikelenRepository>();
builder.Services.AddTransient<CategorieenService>();
builder.Services.AddTransient<ICategorieenRepository, SQLCategorieenRepository>();
builder.Services.AddTransient<LeveranciersService>();
builder.Services.AddTransient<ILeveranciersRepository, SQLLeveranciersRepository>();
builder.Services.AddTransient<SecurityService>();
builder.Services.AddTransient<ISecurityRepository, SQLSecurityRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.UseRequestLocalization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
