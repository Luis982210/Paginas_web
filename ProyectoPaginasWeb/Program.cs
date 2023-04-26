using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProyectoModels.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(s =>

{

    s.IdleTimeout = TimeSpan.FromMinutes(20);

    s.Cookie.Name = ".aWWW.Session"; /*You can change this variable*/

    s.Cookie.Expiration = TimeSpan.FromMinutes(20);

});



builder.Services.AddCors(options =>

{

    options.AddPolicy("AllOrigins",

        builder =>

        {

            builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();

        });

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

               .AddCookie(options =>

               {

                   options.Cookie.SameSite = SameSiteMode.None;

                   options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                   options.LoginPath = "/Login"; /*this option indicates where is the login page*/

               });
var connectionString = builder.Configuration.GetConnectionString("conexion");
builder.Services.AddDbContext<ProyectoPaW2Context>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<ForwardedHeadersOptions>(option => option.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllOrigins",
        builder =>
        {
            builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        });
});
/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!))
    };
});
*/
var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
