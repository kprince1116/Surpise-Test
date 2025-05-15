using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using Repository.Models;
using Repository.Repository;
using service.Interfaces;
using service.Services;
using Service.Interfaces;
using Service.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthservice, AuthService>();
builder.Services.AddScoped<ILoginRepo, LoginRepo>();
builder.Services.AddScoped<IBlogRepo, BlogRepo>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllersWithViews();

var conn = builder.Configuration.GetConnectionString("defaults");
builder.Services.AddDbContext<TestingContext>(options => options.UseNpgsql(conn));
builder.Services.AddMvc().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["jwtToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers["Authorization"] = "Bearer " + token;
                }
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
                {
                    context.NoResult();
                    context.Response.Cookies.Delete("jwtToken");
                    context.Response.Redirect("/Login/Login");
                    return Task.CompletedTask;
                },

            OnChallenge = context =>
            {
                var path = context.Request.Path.Value;
                if (path != "/UserLogin/Login" && !context.Response.HasStarted)
                {
                    context.Response.Cookies.Delete("jwtToken");
                    context.Response.Redirect("/Login/Login");
                    context.HandleResponse();
                }
                return Task.CompletedTask;
            },
            OnForbidden = context =>
             {
                 context.Response.StatusCode = 403;
                 context.Response.Redirect("/Auth/AccessDenied");
                 context.Response.ContentType = "application/json";
                 return Task.CompletedTask;
            }


        };
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
