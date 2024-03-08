using DWShop.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DWShop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddAplicationLayer(this IServiceCollection services) 
        { 
            services.AddTransient(typeof(IPipelineBehavior<,>), 
                typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(x=> x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
             
        }
    }
}
