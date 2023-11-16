using LabWebAPI.Data;
using LabWebAPI.Interfaces;
using LabWebAPI.Middlewares;
using LabWebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// My Service
builder.Services.AddTransient<Seed>(); //? inject seed service
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //? inject automapper service
builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>(); //? inject software repository
builder.Services.AddScoped<IRoomRepository, RoomRepository>(); //? inject room repository
builder.Services.AddScoped<ILabUserRepository, LabUserRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext Services
builder.Services.AddDbContext<DataContext>(options =>
{
    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "db", "LabManagerDatabase.db");
    options.UseSqlite($"Data Source={dbPath}");
});

var app = builder.Build();

//? Use seed services
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseMiddleware<AuthValidationMiddleware>(); //? inject my middleware

app.UseAuthorization();

app.MapControllers();

app.Run();
