using OvakentService.BusinessLogic.Abstract;
using OvakentService.BusinessLogic.Concrete;
using OvakentService.DataAccessLayer.Abstract;
using OvakentService.DataAccessLayer.Concrete;
using OvakentService.DataAccessLayer.EntityFramework;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DefaultContext>();

builder.Services.AddScoped<IAppUserDal, EfAppUserDal>();
builder.Services.AddScoped<IAppUserService, AppUserManager>();

builder.Services.AddScoped<ICarDal, EfCarDal>();
builder.Services.AddScoped<ICarService, CarManager>();

builder.Services.AddScoped<IArventoDal, EfArventoDal>();
builder.Services.AddScoped<IArventoService, ArventoManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
