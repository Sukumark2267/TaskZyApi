using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Core.Services;

namespace Infrastructure.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkPostRepository, WorkPostRepository>();
            services.AddScoped<IWorkTypeRepository, WorkTypeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IConfirmRepository, ConfirmRepository>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
