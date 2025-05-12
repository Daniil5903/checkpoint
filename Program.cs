using checkpoint.Data;
using checkpoint.Hubs;
using checkpoint.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("checkpointDb")));
// Добавляем Identity
builder.Services.AddDefaultIdentity<AuthUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();
// Миграции при старте
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseRouting();
// включаем аутентификацию и авторизацию
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapHub<PassHub>("/passHub");
app.Run();
