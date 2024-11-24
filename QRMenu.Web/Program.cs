using Microsoft.AspNetCore.Authentication.Cookies;
using QRMenu.Infrastructure.Persistence;
using System.Text.Json.Serialization;
using QRMenu.Application;
using QRMenu.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Authentication ayarlarý
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
    options.Cookie.Name = "QRMenu.Auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// Add Application Layer
builder.Services.AddApplication();

// Add Infrastructure Layer
builder.Services.AddInfrastructure(builder.Configuration);

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy =>
        policy.RequireAuthenticatedUser());

    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("RequireDealerAdminRole", policy =>
        policy.RequireRole("DealerAdmin"));

    options.AddPolicy("RequireCompanyAdminRole", policy =>
        policy.RequireRole("CompanyAdmin"));
});

// Session configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Memory Cache
builder.Services.AddMemoryCache();

var app = builder.Build();

// Database migration and seeding
// Database migration and seeding kýsmýný güncelleyelim
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        // Veritabaný mevcut deðilse oluþtur
        if (context.Database.IsSqlServer())
        {
            logger.LogInformation("Migrating database...");
            // MigrateAsync yerine EnsureCreated kullanýyoruz
            context.Database.EnsureCreated();
            logger.LogInformation("Database created successfully.");

            logger.LogInformation("Seeding database...");
            // Seed iþlemini çaðýr
            if (!context.Users.Any()) // Eðer hiç kullanýcý yoksa
            {
                await ApplicationDbContextSeed.SeedDefaultUserAsync(context);
                logger.LogInformation("Database seeded successfully.");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while creating the database.");
        throw;
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Global error handling
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/html"; // JSON yerine HTML

            var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
            if (error != null)
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(error.Error, "An unhandled exception occurred.");

                // Geliþtirme ortamýnda hata detaylarýný göster
                if (app.Environment.IsDevelopment())
                {
                    await context.Response.WriteAsync($"<h1>Error</h1><p>{error.Error.Message}</p><p>{error.Error.StackTrace}</p>");
                }
                else
                {
                    await context.Response.WriteAsync("An internal server error occurred.");
                }
            }
        });
    });
    app.UseHsts();
}

// Middleware pipeline sýralamasý önemli
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Custom error handling middleware
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(error.Error, "An unhandled exception occurred.");

            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = 500,
                Message = "An internal server error occurred."
            });
        }
    });
});

// Route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { controller = "Account", action = "Login" });

app.Run();

//using Microsoft.AspNetCore.Authentication.Cookies;
          //using QRMenu.Application;
          //using QRMenu.Infrastructure;
          //using QRMenu.Infrastructure.Persistence;
          //using Microsoft.EntityFrameworkCore;
          //using System.Text.Json.Serialization;

//var builder = WebApplication.CreateBuilder(args);

//// Add logging
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();

//// Add services to the container.
//builder.Services.AddControllersWithViews()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//        options.JsonSerializerOptions.WriteIndented = true;
//    });

//// CORS Policy
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        builder =>
//        {
//            builder.AllowAnyOrigin()
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});

//// Authentication ayarlarý
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//.AddCookie(options =>
//{
//    options.LoginPath = "/Account/Login";
//    options.LogoutPath = "/Account/Logout";
//    options.AccessDeniedPath = "/Account/AccessDenied";
//    options.ExpireTimeSpan = TimeSpan.FromDays(30);
//    options.SlidingExpiration = true;
//    options.Cookie.Name = "QRMenu.Auth";
//    options.Cookie.HttpOnly = true;
//    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//});

//// Add Application Layer
//builder.Services.AddApplication();

//// Add Infrastructure Layer
//builder.Services.AddInfrastructure(builder.Configuration);

//// Add HttpContextAccessor
//builder.Services.AddHttpContextAccessor();

//// Add Authorization
//builder.Services.AddAuthorization(options =>
//{
//    // Default policy
//    options.AddPolicy("RequireAuthenticatedUser", policy =>
//        policy.RequireAuthenticatedUser());

//    // Admin policy
//    options.AddPolicy("RequireAdminRole", policy =>
//        policy.RequireRole("Admin"));

//    // DealerAdmin policy
//    options.AddPolicy("RequireDealerAdminRole", policy =>
//        policy.RequireRole("DealerAdmin"));

//    // CompanyAdmin policy
//    options.AddPolicy("RequireCompanyAdminRole", policy =>
//        policy.RequireRole("CompanyAdmin"));
//});

//// Session configuration
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

//// Memory Cache
//builder.Services.AddMemoryCache();

//var app = builder.Build();

//// Seed Database
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<ApplicationDbContext>();
//        var logger = services.GetRequiredService<ILogger<Program>>();

//        if (context.Database.IsSqlServer())
//        {
//            logger.LogInformation("Migrating database...");
//            await context.Database.MigrateAsync();
//            logger.LogInformation("Database migrated successfully.");

//            logger.LogInformation("Seeding database...");
//            await ApplicationDbContextSeed.SeedDefaultUserAsync(context);
//            logger.LogInformation("Database seeded successfully.");
//        }
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
//        throw;
//    }
//}

//// Configure the HTTP request pipeline.
//// Middleware pipeline kýsmýný þu þekilde düzenleyin
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseStaticFiles(); // Statik dosyalar için


//app.UseCors("AllowAll");
//app.UseSession();
//app.UseAuthentication();
//app.UseAuthorization();



//// Global error handling
//app.UseExceptionHandler(errorApp =>
//{
//    errorApp.Run(async context =>
//    {
//        context.Response.StatusCode = 500;
//        context.Response.ContentType = "application/json";

//        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
//        if (error != null)
//        {
//            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//            logger.LogError(error.Error, "An unhandled exception occurred.");

//            await context.Response.WriteAsJsonAsync(new
//            {
//                StatusCode = 500,
//                Message = "An internal server error occurred."
//            });
//        }
//    });
//});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Account}/{action=Login}/{id?}");



//app.Run();