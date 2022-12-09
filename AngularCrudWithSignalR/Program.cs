using AngularCrudWithSignalR.Data.DbCon;
using AngularCrudWithSignalR.Factories;
using AngularCrudWithSignalR.Hubs;
using AngularCrudWithSignalR.Services;
using Data.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Custom Code For Service

var connectionString = builder.Configuration.GetValue<string>("AngularCrudConnectionString");
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IDownloadService, DownloadService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IGenericAttributeService, GenericAttributeService>();
builder.Services.AddScoped<ICustomerfactory, Customerfactory>();

builder.Services.AddSignalR();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

#endregion





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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
#region Custom Code

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<SignalRHub>("/signalrhub");
});

#endregion

app.Run();
