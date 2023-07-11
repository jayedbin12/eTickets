using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//For seed
var _connectionString = $@"Server=.;Database=eTickets;User Id=LAPTOP-35V804DV/Jayed;Password=;Trusted_Connection=True;TrustServerCertificate=true;";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_connectionString));
//

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register services
builder.Services.AddScoped<IActorService, ActorService>();

var app = builder.Build();

// Run the seed code and resolve services
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    var appDbInitializer = new AppDbInitializer(dbContext);
    appDbInitializer.Seed();

    // Resolve and use your services
    var actorService = services.GetRequiredService<IActorService>();
    var actorsData = await actorService.GetAllAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
