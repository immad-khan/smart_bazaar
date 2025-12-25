using SmartBazaar.API.Services;
using SmartBazaar.API.Data;
var builder = WebApplication.CreateBuilder(args);
// Register the Database Context
builder.Services.AddSingleton<DatabaseContext>();

// Register the Services your partner wrote
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<StoreService>();
// Add services to the container.
builder.Services.AddScoped<ScraperService>();
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", builder => 
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// 1. Get configuration
var supabaseUrl = builder.Configuration["Supabase:Url"]!;
var supabaseKey = builder.Configuration["Supabase:Key"]!;
// 2. Register Supabase Client as a Singleton (exists for the life of the app)
builder.Services.AddSingleton(provider => 
    new Supabase.Client(supabaseUrl, supabaseKey, new Supabase.SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    }));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Use CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
