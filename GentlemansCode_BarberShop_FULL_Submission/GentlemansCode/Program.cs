using GentlemansCode.Data;
using Microsoft.EntityFrameworkCore;
var builder=WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(o=>o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
var app=builder.Build();
using(var scope=app.Services.CreateScope()){var db=scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();db.Database.EnsureCreated();DbSeeder.Seed(db);}
if(!app.Environment.IsDevelopment()){app.UseExceptionHandler("/Home/Index");app.UseHsts();}
app.UseHttpsRedirection();app.UseStaticFiles();app.UseRouting();
app.MapControllerRoute(name:"default",pattern:"{controller=Home}/{action=Index}/{id?}");
app.Run();
