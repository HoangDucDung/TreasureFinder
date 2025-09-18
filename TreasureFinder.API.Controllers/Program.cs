using TreasureFinder.Service;
using TreasureFinder.Service.Mapping;
using TreasureHunt.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddAutoMapper(typeof(MappingProfile));
DependencyInjectionService.RegisterServices(builder.Services);
DependencyInjectionRepo.RegisterServices(builder.Services);

var app = builder.Build();
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();