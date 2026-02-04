using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrganizationMVC.BLL.Interfaces;
using OrganizationMVC.BLL.Services;
using OrganizationMVC.DAL;
using OrganizationMVC.DAL.Interfaces;
using OrganizationMVC.DAL.Repositories;
using OrganizationMVC.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddValidatorsFromAssemblyContaining<OrganizationDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
        .CreateLogger("DbMigration");

    const int maxAttempts = 10;
    var delay = TimeSpan.FromSeconds(2);
    var attempt = 1;

    while (true)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch (Exception ex) when (attempt < maxAttempts)
        {
            logger.LogWarning(ex, "Database not ready, retry {Attempt}/{MaxAttempts}", attempt, maxAttempts);
            Thread.Sleep(delay);
            attempt++;
        }
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
