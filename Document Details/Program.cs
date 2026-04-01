//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllersWithViews();
//var app = builder.Build();

//if (builder.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

//app.UseStaticFiles();
//app.UseRouting();
//app.MapControllers();

//app.Run();
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//add services into Ioc container
builder.Services.AddSingleton<ICountriesService, CountriesService>();
builder.Services.AddSingleton<IPersonService, PersonsService>();
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.Run();