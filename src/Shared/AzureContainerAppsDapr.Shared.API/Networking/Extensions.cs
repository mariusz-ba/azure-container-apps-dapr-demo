using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;

namespace AzureContainerAppsDapr.Shared.API.Networking;

public static class Extensions
{
    public static IApplicationBuilder UseHeadersForwarding(this IApplicationBuilder app)
    {
        app.Use((context, next) =>
        {
            if (context.Request.Headers.TryGetValue("X-Forwarded-Prefix", out var prefix))
            {
                context.Request.PathBase = new PathString($"/{prefix}");
            }

            return next(context);
        });

        app.UseForwardedHeaders(CreateForwardedHeadersOptions());
        
        return app;
    }

    private static ForwardedHeadersOptions CreateForwardedHeadersOptions()
    {
        var forwardedHeadersOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        };

        return forwardedHeadersOptions;
    }
}