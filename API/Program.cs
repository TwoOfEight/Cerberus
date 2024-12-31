using System.Text;
using API.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository = API.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None);

builder.Services.AddControllers();

builder.Services.AddDbContext<Repository>(options =>
{
    var connection = builder.Configuration.GetConnectionString("SQLServer") ??
                     throw new InvalidOperationException("Database connection string not configured");
    options.UseSqlServer(connection);
});

builder.Services
    .AddIdentity<UserModel, IdentityRole>(options => options.User.AllowedUserNameCharacters += " ")
    .AddEntityFrameworkStores<Repository>()
    .AddDefaultTokenProviders();

var secret = builder.Configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"] ??
                          throw new InvalidOperationException("ValidIssuer not configured"),
            ValidAudience = builder.Configuration["JWT:ValidAudience"] ??
                            throw new InvalidOperationException("ValidAudience not configured"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ClockSkew = new TimeSpan(0, 0, 5)
        };
    });

// Register Application Services
//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("Manager", policy => policy.RequireRole("Admin", "Manager"));
//});

const string developmentPolicy = "developmentPolicy";
const string productionPolicy = "productionPolicy";
string[] allowedOrigins = ["http://192.168.1.35", "http://192.168.1.36"];

builder.Services.AddCors(options =>
{
    options.AddPolicy(developmentPolicy, p =>
    {
        p.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
    options.AddPolicy(productionPolicy, p =>
    {
        p.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors("developmentPolicy");
}
else
{
    app.UseCors("productionPolicy");
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();