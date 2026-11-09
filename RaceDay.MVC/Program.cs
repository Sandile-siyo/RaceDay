using RaceDay.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MVC controller and view support
builder.Services.AddControllersWithViews();

// Add session support for storing logged-in user information
builder.Services.AddSession();

// Allow access to HttpContext inside controllers and services
builder.Services.AddHttpContextAccessor();

// Register ApiService for dependency injection
builder.Services.AddScoped<ApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Display custom error page
    app.UseExceptionHandler("/Home/Error");

    // Enable HTTP Strict Transport Security (HSTS)
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enable static files such as CSS, JavaScript and images
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Enable session middleware
app.UseSession();

// Enable authorization middleware
app.UseAuthorization();

// Configure the default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the application
app.Run();