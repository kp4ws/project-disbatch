using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ProjectDisbatch.API.Data;
using ProjectDisbatch.API.Mappings;
using ProjectDisbatch.API.Repositories;
using ProjectDisbatch.API.Middlewares;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add logging to application
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/ProjectDisbatch_Log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information() //Change this as needed depending on what levels you want to be logged
    .CreateLogger();

//Clear out any existing providers
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddControllers();

//TODO Add versioning to API
//builder.Services.AddApiVersioning();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    //Adds Authorization to Swagger so you can login and make API calls
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Project Disbatch API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }

    });
});

//Inject DbContext class
builder.Services.AddDbContext<ProjectDisbatchDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("ProjectDisbatchConnectionString"))); //TODO: Should get this from .env file (don't store credentials in appsettings.json

//Inject Auth DbContext Class
builder.Services.AddDbContext<ProjectDisbatchAuthDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("ProjectDisbatchAuthConnectionString")));

//Scoped is during lifetime
//If using a different database, then change out the concrete implementation class here
builder.Services.AddScoped<IDepartmentRepository, NpgSqlDepartmentRepository>();
builder.Services.AddScoped<IProjectRepository, NpgSqlProjectRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//TODO revisit
//enable CORS for given client website URL
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowLocalHost",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:5173")
//            .AllowAnyMethod()
//            .AllowAnyHeader()
//            .AllowCredentials();
//        });
//});

builder.Services.AddIdentityCore<IdentityUser>().
    AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("ProjectDisbatch")
    .AddEntityFrameworkStores<ProjectDisbatchAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    //Options for the password we want to configure
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        AuthenticationType = "Jwt",
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidAudiences = new[] { builder.Configuration["Jwt:Audience"] },
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

//TODO revisit
//app.UseCors("AllowLocalHost");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//Allows ASP NET to serve static files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
    //https://localhost:portnumber/Images
});

app.MapControllers();

app.Run();
