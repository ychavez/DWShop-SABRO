
using DWShop.Application.Extensions;
using DWShop.Infrastructure.Context;
using DWShop.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
#if DEBUG


            app.UseSwagger();
            app.UseSwaggerUI();
#endif
            

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
