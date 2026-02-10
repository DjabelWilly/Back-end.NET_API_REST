using System.Text;
using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using P7CreateRestApi.Application.Mapping;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Middleware;
using P7CreateRestApi.Services;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


// -------------------- Serilog --------------------

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

// -------------------- DB Context --------------------
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------- AutoMapper --------------------
builder.Services.AddAutoMapper(typeof(MappingProfile));

// -------------------- Repositories & Services --------------------
builder.Services.AddScoped<IBidListRepository, BidListRepository>();
builder.Services.AddScoped<IBidListService, BidListService>();
builder.Services.AddScoped<ICurvePointRepository, CurvePointRepository>();
builder.Services.AddScoped<ICurvePointService, CurvePointService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRuleRepository, RuleRepository>();
builder.Services.AddScoped<IRuleService,  RuleService>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<ITradeService, TradeService>();

// -------------------- Identity --------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddEntityFrameworkStores<LocalDbContext>()
.AddDefaultTokenProviders();

// -------------------- JWT Authentication --------------------
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = ctx =>
        {
            Console.WriteLine("JWT FAILED: " + ctx.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

// -------------------- Authorization --------------------
builder.Services.AddAuthorization(options =>
{
    // User : User OU Admin
    options.AddPolicy("UserAccess", policy =>
        policy.RequireRole("User", "Admin"));

    // Admin : Admin uniquement
    options.AddPolicy("AdminAccess", policy =>
        policy.RequireRole("Admin"));
});



// -------------------- Controllers & Swagger --------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT token"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// -------------------- Pipeline --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseMiddleware<LoggerMiddleware>();
app.UseAuthorization();


app.MapControllers();

// -------------------- Seed --------------------
using (var scope = app.Services.CreateScope())
{
    // Roles
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    foreach (var role in new[] { "Admin", "User" })
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
    // Admin → accès à toutes les routes
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var adminEmail = "admin@admin.com";
    var admin = await userManager.FindByEmailAsync(adminEmail);
    if (admin == null)
    {
        admin = new ApplicationUser
        {
            UserName = "admin",
            Email = adminEmail,
            EmailConfirmed = true,
            FullName = "Administrator"
        };
        await userManager.CreateAsync(admin, "Admin123!");
        await userManager.AddToRoleAsync(admin, "Admin");
    }

    // User → accès uniquement aux routes "User"
    var userEmail = "user@user.com";
    var user = await userManager.FindByEmailAsync(userEmail);
    if (user == null)
    {
        user = new ApplicationUser
        {
            UserName = "user",
            Email = userEmail,
            EmailConfirmed = true,
            FullName = "AuthenticatedUser"
        };
        await userManager.CreateAsync(user, "User123!");
        await userManager.AddToRoleAsync(user, "User");
    }

}

app.Run();
