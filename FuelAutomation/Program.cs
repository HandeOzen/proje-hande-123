using Business.Abstract;
using Business.Concrete;
using Data.Abstract;
using Data.Concrete;
using Data.Contexts;
using Data.Seed;
using FluentValidation.AspNetCore;
using FuelAutomation.Entity;
using FuelAutomation.Identity;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>()); //assembly kodundan alsýn validationlarý

//IConfigurationRoot Configuration = new ConfigurationBuilder();

builder.Services.AddDbContext<ApplicationDbContext>(); 


builder.Services.AddIdentity<Users, IdentityRole>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<Users>, UserClaimsPrincipalFactory<Users, IdentityRole>>();
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("RequireAdmin",
    policy => policy.RequireRole("Admin"));


    options.AddPolicy("RequireStaff",


        policy => policy.RequireRole("Staff"));

});
builder.Services.Configure<IdentityOptions>(options =>

{
  
}


    );
builder.Services.ConfigureApplicationCookie(options =>

{
    options.LoginPath = "/Account/Login";

    options.AccessDeniedPath = "/Account/AccessDenied";
}


    );

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITanksService, TanksManager>();
builder.Services.AddScoped<IFuelTypesService, FuelTypesManager>();

builder.Services.AddScoped<ISalesService, SalesManager>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//SeedUser.Initialize(app.Services);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.MapControllerRoute(
                  name: "tankfill",
                  pattern: "{controller=Admin}/{action=TankFill}/{id?}"
                
              );




app.Run();
