using GymManagementApp.Data;
using GymManagementApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Konfiguracja SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfiguracja Identity
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Rejestracja serwisów w kontenerze DI
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGymUserService, GymUserService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Middleware zapamiêtywania ostatniej wizyty
app.UseMiddleware<GymManagementApp.Middleware.LastVisitMiddleware>();

// Middleware logowania ¿¹dañ
app.Use(async (context, next) =>
{
    var requestContent = new System.Text.StringBuilder();
    requestContent.AppendLine("=== Request Info ===");
    requestContent.AppendLine($"Method = {context.Request.Method.ToUpper()}");
    requestContent.AppendLine($"Path = {context.Request.Path}");

    requestContent.AppendLine("-- Headers --");
    foreach (var (key, value) in context.Request.Headers)
    {
        requestContent.AppendLine($"Header = {key}, Value = {value}");
    }

    requestContent.AppendLine("-- Body --");
    context.Request.EnableBuffering();

    using (var requestReader = new System.IO.StreamReader(context.Request.Body, leaveOpen: true))
    {
        var content = await requestReader.ReadToEndAsync();
        requestContent.AppendLine($"Body = {content}");
        context.Request.Body.Position = 0; // Resetowanie pozycji strumienia
    }

    Console.WriteLine(requestContent.ToString());
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

// Konfiguracja domyœlnej trasy
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
