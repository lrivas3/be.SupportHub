using HelpPoint.Common.DependencyInjection;
using HelpPoint.Common.RequestPipeline;
using HelpPoint.Features.Support;
using HelpPoint.Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFeaturesDependencyInjection()
    .AddEmailConfiguration(configuration: builder.Configuration)
    .AddRepositories()
    .AddApplicationDbContext(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
    .AddOpenApiDocumentation();

builder.Services.AddVersioning();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddControllers();

// builder.Services.AddSingleton<ProblemDetailsFactory, HelpPointProblemDetailsFactory>();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddHttpClient<IRecaptchaService, RecaptchaService>();

builder.Services.AddCors(options =>
 {
     options.AddPolicy("AllowConfiguredOrigins", policy =>
     {
         policy.WithOrigins(allowedOrigins ?? throw new InvalidOperationException("Cors no configurado"))
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
     });
 });

var app = builder.Build();

app.UseGlobalErrorHandling();
app.UseVersionSet();

app.UseCustomOpenApiViewer();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowConfiguredOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
