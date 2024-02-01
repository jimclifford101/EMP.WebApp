using EMP.Core.DBRepos;
using EMP.Core.DBRepos.Interfaces;
using EMP.Core.DBRepos.TestData;
using EMP.Core.UseCases;
using EMP.Core.UseCases.Interfaces;
using EMP.WebApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var constr = builder.Configuration.GetConnectionString("myConxStrAppSettings"); 
builder.Services.AddDbContextFactory<CoreDBContext>(options => { options.UseSqlServer(constr); });

/*

// Add the whole configuration object here.  assembly: GetType().Assembly
var fileProvider = new EmbeddedFileProvider(GetType().Assembly);
var configuration = new ConfigurationBuilder()
  .AddJsonFile(provider: fileProvider,
   path: "appsettings.json",
   optional: true,
   reloadOnChange: false)
  .Build();
//services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddSingleton<IConfiguration>(configuration);

*/

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

if (1 == 2)
{
    builder.Services.AddSingleton<IPlStateRepository, PlStateRepositoryTestData>();
    builder.Services.AddSingleton<IEmpRepository, EmpRepositoryTestData>();
    builder.Services.AddSingleton<IOrgRepository, OrgRepositoryTestData>();
    builder.Services.AddSingleton<IEmpHistRepository, EmpHistRepositoryTestData>();
}
else
{
    builder.Services.AddTransient<IPlStateRepository, PlStateRepository>();
    builder.Services.AddTransient<IEmpRepository, EmpRepository>();
    builder.Services.AddTransient<IOrgRepository, OrgRepository>();

    builder.Services.AddTransient<IEmpHistRepository, EmpHistRepository>();
}

builder.Services.AddTransient<IViewPlStateAllUseCase, ViewPlStateAllUseCase>();
builder.Services.AddTransient<IViewPlStateByNameUseCase, ViewPlStateByNameUseCase>();
builder.Services.AddTransient<IViewPlStateByIdUseCase, ViewPlStateByIdUseCase>();
builder.Services.AddTransient<IAddPlStateUseCase, AddPlStateUseCase>();
builder.Services.AddTransient<IEditPlStateUseCase, EditPlStateUseCase>();
builder.Services.AddTransient<IDeletePlStateUseCase, DeletePlStateUseCase>();

builder.Services.AddTransient<IViewEmpAllUseCase, ViewEmpAllUseCase>();
builder.Services.AddTransient<IViewEmpByNameUseCase, ViewEmpByNameUseCase>();
builder.Services.AddTransient<IViewEmpByIdUseCase, ViewEmpByIdUseCase>();
builder.Services.AddTransient<IAddEmpUseCase, AddEmpUseCase>();
builder.Services.AddTransient<IEditEmpUseCase, EditEmpUseCase>();
builder.Services.AddTransient<IDeleteEmpUseCase, DeleteEmpUseCase>();

builder.Services.AddTransient<ISearchEmpUseCase, SearchEmpUseCase>();

builder.Services.AddTransient<IViewOrgAllUseCase, ViewOrgAllUseCase>();
builder.Services.AddTransient<IViewOrgByNameUseCase, ViewOrgByNameUseCase>();
builder.Services.AddTransient<IViewOrgByIdUseCase, ViewOrgByIdUseCase>();
builder.Services.AddTransient<IAddOrgUseCase, AddOrgUseCase>();
builder.Services.AddTransient<IEditOrgUseCase, EditOrgUseCase>();
builder.Services.AddTransient<IDeleteOrgUseCase, DeleteOrgUseCase>();
builder.Services.AddTransient<ISearchOrgUseCase, SearchOrgUseCase>();

builder.Services.AddTransient<IViewEmpHistAllUseCase, ViewEmpHistAllUseCase>();
builder.Services.AddTransient<IViewEmpHistByNameUseCase, ViewEmpHistByNameUseCase>();
builder.Services.AddTransient<IViewEmpHistByIdUseCase, ViewEmpHistByIdUseCase>();
builder.Services.AddTransient<IAddEmpHistUseCase, AddEmpHistUseCase>();
builder.Services.AddTransient<IEditEmpHistUseCase, EditEmpHistUseCase>();
builder.Services.AddTransient<IDeleteEmpHistUseCase, DeleteEmpHistUseCase>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
