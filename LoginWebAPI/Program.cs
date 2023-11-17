using LoginWebAPI.Data;
using LoginWebAPI.Helper;
using LoginWebAPI.Interfaces;
using LoginWebAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// My Service
builder.Services.AddTransient<Seed>(); //? inject seed service
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //? add AutoMaper depedency injection
builder.Services.AddScoped<IUserAdminRepository, UserAdminRepository>(); //? inject user admin repository
builder.Services.AddScoped<IUserClientRepository, UserClientRepository>();
builder.Services.AddScoped<AuthHelper>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


// DbContext Services
builder.Services.AddDbContext<DataContext>(options =>
{
    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "db", "LoginDatabase.db");
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
    app.UseCors("AllowAll");
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
