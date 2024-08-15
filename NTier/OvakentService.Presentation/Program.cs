using OvakentService.BusinessLogic.Abstract;
using OvakentService.BusinessLogic.Concrete;
using OvakentService.DataAccessLayer.Abstract;
using OvakentService.DataAccessLayer.Concrete;
using OvakentService.DataAccessLayer.EntityFramework;
using OvakentService.EntityLayer.Concrete;
using OvakentService.Presentation.Validators.IdentityValidators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DefaultContext>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<DefaultContext>().AddErrorDescriber<CustomIdentityValidator>();
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = $"/Login/Index/";
    opt.LogoutPath = $"/Logout/Index/";
    opt.ExpireTimeSpan = TimeSpan.FromDays(30);
});
builder.Services.AddScoped<IAppUserDal, EfAppUserDal>();
builder.Services.AddScoped<IAppUserService, AppUserManager>();

builder.Services.AddScoped<ICarDal, EfCarDal>();
builder.Services.AddScoped<ICarService, CarManager>();

builder.Services.AddScoped<IArventoDal, EfArventoDal>();
builder.Services.AddScoped<IArventoService, ArventoManager>();
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Profile}/{action=Index}/{id?}");

app.Run();
