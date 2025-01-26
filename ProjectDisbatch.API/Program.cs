using Microsoft.EntityFrameworkCore;
using ProjectDisbatch.API.Data;
using ProjectDisbatch.API.Mappings;
using ProjectDisbatch.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inject DbContext class
builder.Services.AddDbContext<ProjectDisbatchDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("ProjectDisbatchConnectionString"))); //TODO: Should get this from .env file (don't store credentials in appsettings.json

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//Scoped is during lifetime
//If using a different database, then change out the concrete implementation class here
builder.Services.AddScoped<IDepartmentRepository, NpgSqlDepartmentRepository>(); 
builder.Services.AddScoped<IProjectRepository, NpgSqlProjectRepository>();

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
