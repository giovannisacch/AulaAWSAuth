using Amazon.Rekognition;
using Amazon.Runtime;
using Amazon.S3;
using AulaAWS.Application.Services;
using AulaAWS.Lib.Data;
using AulaAWS.Lib.Data.Repositorios;
using AulaAWS.Lib.Data.Repositorios.Interfaces;
using AulaAWS.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AulaAWS.Di
{
    public static class MyConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AWSLoginContext>(
                conn => conn.UseNpgsql(config.GetConnectionString("AWSLoginDB"))
                .UseSnakeCaseNamingConvention());

            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IUsuarioApplication, UsuarioApplication>();
            services.AddScoped<IImagensServices, ImagensServices>();
            services.AddAWSService<IAmazonS3>();
            services.AddScoped<AmazonRekognitionClient>();

            var awsOptions = config.GetAWSOptions();
            awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();
            services.AddDefaultAWSOptions(awsOptions);

            return services;
        }
    }
}