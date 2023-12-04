using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Diet_Diary.Data;
using Diet_Diary.Models;
using Diet_Diary.Services;
using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityMongoDbProvider<MongoUser, MongoRole>(identity =>
{
    identity.Password.RequiredLength = 8;
    // other options
},
mongo =>
{
    mongo.ConnectionString = "secret connection string";
    // other options
})
.AddDefaultUI();


builder.Services.AddControllersWithViews();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<ProductsService>();
builder.Services.AddSingleton<UserProductsService>();
builder.Services.AddSingleton<MealsService>();
builder.Services.AddSingleton<ServedMealsService>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
