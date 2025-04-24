using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Date;
using Server.Mapping;
using Server.Models.DTOs;
using Server.Models.Entities;
using Server.Repo.interfaces;
using Server.Repo.repositories;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;

});
#region Db Connection  
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

#region Identity settings
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    options.Password.RequireDigit = false;
//options.Password.RequireLowercase = false;
//options.Password.RequireNonAlphanumeric = false;
//options.Password.RequireUppercase = false;
//options.Password.RequiredLength = 6;
//})
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();// AddDefaultTokenProviders is
                                                                                                                             // to make GeneratePasswordResetTokenAsync Work

// Configuration of Password when Register
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
});
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>(); // object that holds data in Jwt Section
builder.Services.AddSingleton(jwtOptions); // register while dependency injection in UserController in primary Constructor
builder.Services.AddScoped<JwtRepository>();

#endregion


#region  Scoped Services

builder.Services.AddScoped<IGenericRepository<House>, GenericRepository<House>>();
builder.Services.AddScoped<IGenericRepository<Device>, GenericRepository<Device>>();
builder.Services.AddScoped<IGenericRepository<History>, GenericRepository<History>>();
builder.Services.AddScoped<IGenericRepository<Notification>, GenericRepository<Notification>>();
builder.Services.AddScoped<IGenericRepository<Alarm>, GenericRepository<Alarm>>();
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IHistoriesRepository, HistoryRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IAlarmRepository, AlarmRepository>();

#endregion


#region Authentication settings
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        var jwtKey = builder.Configuration["Jwt:Key"];
//        if (string.IsNullOrEmpty(jwtKey))
//        {
//            throw new InvalidOperationException("JWT Key is not configured.");
//        }

//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//        };
//    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // The Default Authentication is JWT not Cookies
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // if unauthorized return to him "unauthorized" not "Not Found"
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // for any other Schema
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true; // save Token String in AuthenticationProperties in case you needed it 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)) // transform SingingKey
                                                                                                   // from string to Byte
    };

});

#endregion

#region Authorization settings

#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig).Assembly);
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

