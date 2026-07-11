using System.Threading.RateLimiting;

namespace PersonalFinance.Api.Middleware;

public static class RateLimitingExtensions
{
    public static void RateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(configureOptions: options =>
        {
            options.AddPolicy(policyName: RateLimitingPolicyNames.REGISTER_USER, partitioner: httpContext =>
                CreateIpPartition(httpContext: httpContext, permitLimit: 5, window: TimeSpan.FromMinutes(minutes: 20)));

            options.AddPolicy(policyName: RateLimitingPolicyNames.LOGIN, partitioner: httpContext =>
                CreateIpPartition(httpContext: httpContext, permitLimit: 10, window: TimeSpan.FromMinutes(minutes: 1)));

            options.AddPolicy(policyName: RateLimitingPolicyNames.FORGOT_PASSWORD, partitioner: httpContext =>
                CreateIpPartition(httpContext: httpContext, permitLimit: 5, window: TimeSpan.FromMinutes(minutes: 15)));

            options.OnRejected = async (context, cancellationToken) =>
            {
                int retryAfterSeconds = context.Lease.TryGetMetadata(metadataName: MetadataName.RetryAfter, metadata: out TimeSpan retryAfter)
                    ? (int)retryAfter.TotalSeconds
                    : 60;

                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.Headers.RetryAfter = retryAfterSeconds.ToString();

                await context.HttpContext.Response.WriteAsJsonAsync(
                    value: new { errorMessages = new[] { "Too many requests. Try again later." } },
                    cancellationToken: cancellationToken);
            };
        });
    }

    private static RateLimitPartition<string> CreateIpPartition(HttpContext httpContext, int permitLimit, TimeSpan window)
    {
        string ipAddress = GetClientIpAddress(httpContext: httpContext);

        return RateLimitPartition.GetFixedWindowLimiter(partitionKey: ipAddress,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = permitLimit,
                Window = window,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            });
    }

    private static string GetClientIpAddress(HttpContext httpContext)
    {
        string? forwardedFor = httpContext.Request.Headers[key: "X-Forwarded-For"].FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(value: forwardedFor))
        {
            return forwardedFor.Split(separator: ',').First().Trim();
        }

        return httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
}

public static class RateLimitingPolicyNames
{
    public const string REGISTER_USER = "register-user";
    public const string LOGIN = "login";
    public const string FORGOT_PASSWORD = "forgot-password";
}