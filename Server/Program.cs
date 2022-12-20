using FSDOCS.Server.Repositories.Contracts;
using FSDOCS.Server.Repositories;
using FSDOCS.Server.Data;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using FSDOCS.Server.Helpers;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
});


builder.Services.AddRazorPages();

// configure strongly types settings objects
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register the dbcontext class for dependency injection
var connectionString = builder.Configuration.GetConnectionString("SecuredFSDOCSSQLServerDBConnection");
builder.Services.AddDbContext<FsdocsDbContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddScoped<IPreinscriptionService, PreinscriptionService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IDossierService, DossierService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IGroupeService, GroupeService>();

builder.Services.AddScoped<TokenAuthenticationFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
}

// global cors policy
app.UseCors("DevCorsPolicy");

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

//
app.UseSession();

app.Run();
