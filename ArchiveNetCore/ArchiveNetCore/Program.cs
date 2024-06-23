using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ArchiveNetCore.Data;
using ArchiveNetCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ArchiveNetCore.Utils;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ArchiveNetCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ArchiveNetCoreContext") ?? throw new InvalidOperationException("Connection string 'ArchiveNetCoreContext' not found.")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configurez ici les options si nécessaire
})
.AddEntityFrameworkStores<ArchiveNetCoreContext>()
.AddDefaultTokenProviders();

// Ajout du service Utility
builder.Services.AddScoped<ArchiveNetCore.Utils.Utility>();

// Add services to the container.
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

