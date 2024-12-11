using Doctors_Web_Forum.BLL.IServices;
using Doctors_Web_Forum.BLL.Services;
using Doctors_Web_Forum.DAL.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Add Service DbContext
builder.Services.AddDbContext<DataDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSQL")));


// SignUp Service Scoped



builder.Services.AddScoped<ITopicService,TopicService>();



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
