using Microsoft.EntityFrameworkCore;
using Server.Date;
using Server.Mapping;
using Server.Models.Entities;
using Server.Repo.interfaces;
using Server.Repo.repositories;
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

#region Db Connection  
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

#region Identity settings
//builder.Services.AddIdentity<User, IdentityRole>(options =>
//{
//    options.Password.RequireDigit = false;
//options.Password.RequireLowercase = false;
//options.Password.RequireNonAlphanumeric = false;
//options.Password.RequireUppercase = false;
//options.Password.RequiredLength = 6;
//})
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();
#endregion


#region  Scoped Services

builder.Services.AddScoped<IGenericRepository<House>, GenericRepository<House>>();
builder.Services.AddScoped<IGenericRepository<Device>, GenericRepository<Device>>();
builder.Services.AddScoped<IGenericRepository<History>, GenericRepository<History>>();
builder.Services.AddScoped<IGenericRepository<Notification>, GenericRepository<Notification>>();
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IHistoriesRepository, HistoryRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

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
#endregion

#region Authorization settings
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
//});
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

#region Create Admin User
//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

//    string[] roles = { "Admin", "User" };
//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }

//    // Create Admin User
//    var adminEmail = "admin@example.com";
//    var adminUser = await userManager.FindByEmailAsync(adminEmail);
//    if (adminUser == null)
//    {
//        var newAdmin = new User
//        {
//            UserName = adminEmail,
//            Email = adminEmail,
//            Address = "Default Address" // Set a default value for Address
//        };
//        await userManager.CreateAsync(newAdmin, "Admin@1234");
//        await userManager.AddToRoleAsync(newAdmin, "Admin");
//    }
//}
#endregion

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

