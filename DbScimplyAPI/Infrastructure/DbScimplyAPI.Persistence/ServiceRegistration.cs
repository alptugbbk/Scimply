using DbScimplyAPI.Application.Abstractions.Cryptographies;
using DbScimplyAPI.Application.Abstractions.Services;
using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Persistence.Concretes.Cryptographies;
using DbScimplyAPI.Persistence.Concretes.Services;
using DbScimplyAPI.Persistence.Configurations;
using DbScimplyAPI.Persistence.Context;
using DbScimplyAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Persistence
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<MyContext>(options => options.UseNpgsql(DbConfiguration.ConnectionString));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ISchemaRepository, SchemaRepository>();
            services.AddScoped<IAESEncryption, AESEncryption>();
            services.AddScoped<IPasswordGenerator, PasswordGenerator>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IMailSenderService, MailSenderService>();
        }


    }
}
