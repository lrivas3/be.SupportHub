using Asp.Versioning;
using Scalar.AspNetCore;

namespace HelpPoint.Common.RequestPipeline;

public static class WebApplicationExtensions
{
    public static void UseGlobalErrorHandling(this WebApplication app) => app.UseExceptionHandler("/error");

    public static void UseCustomOpenApiViewer(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("SupportHub API")
                .WithTheme(ScalarTheme.DeepSpace)
                .WithDefaultHttpClient(ScalarTarget.Shell, ScalarClient.Curl);
        });
    }

    public static void UseVersionSet(this WebApplication app)
    {
        var versionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1))
                .ReportApiVersions()
                .Build();
    }
}
