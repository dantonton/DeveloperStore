using System.Reflection;
using DeveloperStore.Domain.Infra;
using DeveloperStore.Infra;
using DeveloperStore.Infra.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssemblies(Assembly.Load("DeveloperStore.Domain"));
});
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddHttpClient();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

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
