using App.Application.Extentions;
using App.Domain.Entities.Identity;
using App.Infrastructure.Extentions;
using App.Infrastructure.Seeder;
using App.Presentation.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Main Services
builder.Services.AddPresentationServicesRegistration(builder.Configuration);

//AppDbContext,Repositories
builder.Services.AddInfrastructureServicesRegistration(builder.Configuration);

//Business Services
builder.Services.AddApplicationServicesRegistration(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    await RoleSeeder.SeedAsync(roleManger);
    await UserSeeder.SeedAsync(userManger);
}

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

var requestlocalizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(requestlocalizationOptions.Value);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LogIn}/{id?}");

app.Run();
