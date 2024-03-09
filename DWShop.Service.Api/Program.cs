
using DWShop.Application.Extensions;
using DWShop.Infrastructure.Context;
using DWShop.Infrastructure.Extensions;
using DWShop.Service.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DWShop.Service.Api

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAplicationLayer();
            builder.Services.AddRepositories();

            builder.Services.AddDbContext<AuditableContext>(
                options => options.UseSqlServer
                (builder.Configuration.GetConnectionString("DefaultConnection"))
                );

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Identity:Key")!);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            })
                .AddRoles<IdentityRole>()
                .AddSignInManager<SignInManager<IdentityUser>>()
                .AddRoleValidator<RoleValidator<IdentityRole>>()
                .AddEntityFrameworkStores<AuditableContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
#if DEBUG


            app.UseSwagger();
            app.UseSwaggerUI();
#endif
            app.UseAuthentication();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
