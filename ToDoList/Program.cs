using ToDoList.Core;
using ToDoList.Core.Services.Interfaces;
using ToDoList.Web._services;
using ToDoListServices.Core.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

//Register to store a session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Call your service registration extension method
builder.Services.AddToDoListDalServices();

//Add DI for Web
builder.Services.AddScoped<ISessionService, SessionService>();

//Add other DI
builder.Services.AddScoped<IToDoItemService, ToDoItemService>();
builder.Services.AddScoped<IUserService, UserService>();

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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
