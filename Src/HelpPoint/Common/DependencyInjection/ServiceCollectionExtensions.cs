using System.Text;
using Asp.Versioning;
using HelpPoint.Common.Http;
using HelpPoint.Config;
using HelpPoint.Features.Auth;
using HelpPoint.Features.Catalogo;
using HelpPoint.Features.Emails;
using HelpPoint.Features.Employees;
using HelpPoint.Features.Support;
using HelpPoint.Features.Tickets;
using HelpPoint.Features.Users;
using HelpPoint.Infrastructure.Authentication;
using HelpPoint.Infrastructure.DataBase;
using HelpPoint.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HelpPoint.Common.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                var env = context.HttpContext.RequestServices.GetRequiredService<IHostEnvironment>();
                context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                context.ProblemDetails.Extensions["environment"] = env.EnvironmentName;
            };
        });

        return services;
    }


    public static IServiceCollection AddEmailConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        _ = services.Configure<EmailSettings>(options =>
        {
            options.Host = configuration["Email:Host"] ??
                           throw new InvalidOperationException("Configuracion de correo fallida: Host no configurado");
            options.Port = int.Parse(configuration["Email:Port"] ??
                                     throw new InvalidOperationException(
                                         "Configuracion de correo fallida: Port no configurado"));
            options.UserName = configuration["Email:UserName"] ??
                               throw new InvalidOperationException(
                                   "Configuracion de correo fallida: User no configurado");
            options.PassWord = configuration["Email:Password"] ??
                               throw new InvalidOperationException(
                                   "Configuracion de correo fallida: Pass no configurado");
            options.TimeOut = int.Parse(configuration["Email:TimeOut"] ?? "5000");
        });
        return services;
    }

    public static IServiceCollection AddFeaturesDependencyInjection(this IServiceCollection services)
    {
        _ = services.AddScoped<IAuth, AuthService>()
            .AddScoped<LoginValidator>()
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<ITokenGenerator, TokenGenerator>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ISupport, SupportRequestService>()
            .AddScoped<ITicket, TicketService>()
            .AddScoped<ICatalogoService, CatalogoService>()
            .AddScoped<IEmailService, EmailService>();

        services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
        services.AddHttpContextAccessor();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        _ = services.AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserRolesRepository, UserRolesRepository>()
            .AddScoped<ISupportRequestRepository, SupportRequestRepository>()
            .AddScoped<IEmployeeRepository, EmployeeRepository>()
            .AddScoped<ITicketRepository, TicketRepository>()
            .AddScoped<IEstadoRepository, EstadoRepository>();
        return services;
    }

    /// <summary>
    /// Adds and configures the database context (using Npgsql in this example).
    /// </summary>
    public static IServiceCollection AddApplicationDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<HelpPointDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    /// <summary>
    /// Adds JWT authentication using configuration from appsettings.json (Jwt section).
    /// </summary>
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("Jwt");
        var secretKey = jwtConfig["SecretKey"];
        var issuer = jwtConfig["Issuer"];
        var audience = jwtConfig["Audience"];

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Convert.FromBase64String(secretKey!))
                };
            });

        return services;
    }

    /// <summary>
    /// Adds OpenAPI/Swagger services (assuming you want to move that too).
    /// </summary>
    public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }

    /// <summary>
    /// Add Api Versioning
    /// </summary>
    public static void AddVersioning(this IServiceCollection services) =>
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion                   = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions                   = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddMvc().AddApiExplorer(options =>
        {
            options.GroupNameFormat           = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
}
