using Yarp.ReverseProxy.Configuration;

namespace Gateway.Api.Extensions;

internal static class ReverseProxyExtension
{
    private const string USER_CLUSTER = "userCluster";
    private const string FINANCE_CLUSTER = "financeCluster";
    private const string AUTHENTICATED = "default";

    internal static void AddReverseProxyConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddReverseProxy().LoadFromMemory(routes: BuildRoutes(), clusters: BuildClusters(configuration: configuration));
    }

    private static IReadOnlyList<RouteConfig> BuildRoutes() =>
    [
        new()
        {
            RouteId = "user-register",
            ClusterId = USER_CLUSTER,
            Match = new RouteMatch { Path = "/User", Methods = [HttpMethods.Post] },
            RateLimiterPolicy = RateLimitingPolicyNames.REGISTER_USER
        },
        new()
        {
            RouteId = "user-login",
            ClusterId = USER_CLUSTER,
            Match = new RouteMatch { Path = "/User/login", Methods = [HttpMethods.Post] },
            RateLimiterPolicy = RateLimitingPolicyNames.LOGIN
        },
        new()
        {
            RouteId = "user-login-google",
            ClusterId = USER_CLUSTER,
            Match = new RouteMatch { Path = "/User/login-google", Methods = [HttpMethods.Post] },
            RateLimiterPolicy = RateLimitingPolicyNames.LOGIN
        },
        new()
        {
            RouteId = "user-forgot-password",
            ClusterId = USER_CLUSTER,
            Match = new RouteMatch { Path = "/User/forgot-password", Methods = [HttpMethods.Post] },
            RateLimiterPolicy = RateLimitingPolicyNames.FORGOT_PASSWORD
        },
        new()
        {
            RouteId = "user-validate-reset-code",
            ClusterId = USER_CLUSTER,
            Match = new RouteMatch { Path = "/User/validate-reset-code", Methods = [HttpMethods.Post] }
        },
        new()
        {
            RouteId = "user-reset-password",
            ClusterId = USER_CLUSTER,
            Match = new RouteMatch { Path = "/User/reset-password", Methods = [HttpMethods.Put] }
        },
        new()
        {
            RouteId = "user-authenticated",
            ClusterId = USER_CLUSTER,
            Match = new RouteMatch { Path = "/User", Methods = [HttpMethods.Put, HttpMethods.Get, HttpMethods.Delete] },
            AuthorizationPolicy = AUTHENTICATED
        },
        new()
        {
            RouteId = "user-password",
            ClusterId = USER_CLUSTER,
            Match = new RouteMatch { Path = "/User/password", Methods = [HttpMethods.Put] },
            AuthorizationPolicy = AUTHENTICATED
        },
        new()
        {
            RouteId = "finance-catchall",
            ClusterId = FINANCE_CLUSTER,
            Match = new RouteMatch { Path = "/{**catch-all}" },
            AuthorizationPolicy = AUTHENTICATED
        }
    ];

    private static IReadOnlyList<ClusterConfig> BuildClusters(IConfiguration configuration) =>
    [
        BuildCluster(clusterId: USER_CLUSTER, address: configuration.ServiceAddress(serviceName: "UserService")),
        BuildCluster(clusterId: FINANCE_CLUSTER, address: configuration.ServiceAddress(serviceName: "FinanceService"))
    ];

    private static ClusterConfig BuildCluster(string clusterId, string address) =>
        new()
        {
            ClusterId = clusterId,
            Destinations = new Dictionary<string, DestinationConfig>
            {
                [key: "destination1"] = new() { Address = address }
            }
        };

    private static string ServiceAddress(this IConfiguration configuration, string serviceName) =>
        configuration[key: $"Services:{serviceName}"]
        ?? throw new InvalidOperationException(message: $"Missing configuration 'Services:{serviceName}'.");
}
