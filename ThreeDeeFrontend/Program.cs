using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using ThreeDeeFrontend.Components;
using ThreeDeeFrontend.ViewModels;
using ThreeDeeInfrastructure.Repositories;
using ThreeDeeInfrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ThreeDeeApplication.Models;
using ThreeDeeFrontend.Areas.Identity;
using ThreeDeeFrontend.Data;
using ThreeDeeFrontend.Services;
using ThreeDeeInfrastructure.ResponseModels;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddSingleton<IEndpointService, EndpointService>();
builder.Services.AddSingleton<IRepository<FileModel, FileModel>, Repository<FileModel, FileModel>>();
builder.Services.AddSingleton<IRepository<GCodeSettingsModel, GCodeSettingsModel>, Repository<GCodeSettingsModel, GCodeSettingsModel>>();
builder.Services.AddScoped<TopMenuViewModel>();
builder.Services.AddScoped<IJsInteropService<ModelRenderer>, JsInteropService<ModelRenderer>>();
builder.Services.AddScoped<IThemeProviderService, ThemeProviderService>();
builder.Services.AddScoped<IGCodeSettingsRepository, GCodeSettingsRepository>();
builder.Services.AddScoped<AuthenticationValidator>();

builder.Services.AddScoped<IFilesGridViewModel, FilesGridViewModel>();


var appSettingsFilePath = builder.Environment.EnvironmentName == "Production" ? "appsettings.json" : "appsettings.Development.json";

builder.Services.AddOAuthProviders(appSettingsFilePath);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();