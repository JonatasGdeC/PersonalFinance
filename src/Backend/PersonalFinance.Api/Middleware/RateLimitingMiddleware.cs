using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace PersonalFinance.Api.Middleware;

public static class RateLimitingMiddleware
{
    public static void RateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(configureOptions: options =>
        {
            options.AddFixedWindowLimiter(policyName: RateLimitingPolicyNames.REGISTER_USER, configureOptions: opt =>
            {
                opt.PermitLimit = 1;
                opt.Window = TimeSpan.FromMinutes(minutes: 5);
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.QueueLimit = 0;
            });
            
            options.AddFixedWindowLimiter(policyName: RateLimitingPolicyNames.LOGIN, configureOptions: opt =>
            {
                opt.PermitLimit = 10;
                opt.Window = TimeSpan.FromMinutes(minutes: 1);
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.QueueLimit = 0;
            });

            options.AddFixedWindowLimiter(policyName: RateLimitingPolicyNames.FORGOT_PASSWORD, configureOptions: opt =>
            {
                opt.PermitLimit = 5;
                opt.Window = TimeSpan.FromMinutes(minutes: 15);
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.QueueLimit = 0;
            });

            
            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.Headers.RetryAfter = "60";
                await context.HttpContext.Response.WriteAsJsonAsync(
                    value: new { errorMessages = new[] { "Too many requests. Try again later." } },
                    cancellationToken: cancellationToken);
            };
        });
    }
}

public static class RateLimitingPolicyNames
{
    public const string REGISTER_USER = "register-user";
    public const string LOGIN = "login";
    public const string FORGOT_PASSWORD = "forgot-password";
}