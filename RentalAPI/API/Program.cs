using API.DataAccess;
using API.Implementation;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnect")));
//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "localhost",
        ValidAudience = "localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"])),
        ClockSkew = TimeSpan.Zero

    };
});

//Dependency InjectionA
builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddScoped<IVehiclesDisplay, VehicleDisplay>();
builder.Services.AddScoped<IOrder, Order>();
builder.Services.AddScoped<IOrderDisplay, OrderDisplay>();
builder.Services.AddScoped<IReturnVehicle, ReturnVehicle>();
builder.Services.AddScoped<IUser, Users>();

//builder.Services.AddSingleton<IDataAccess, DataAccess>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
