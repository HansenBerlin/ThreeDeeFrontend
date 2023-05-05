
using Microsoft.AspNetCore.Authentication.OAuth;

namespace ThreeDeeFrontend.Services;

public static class ServiceCollectionExtensions
{
    public static void AddOAuthProviders(this IServiceCollection services, string appSettingsFilePath)
    {
        var configurationRoot = new ConfigurationBuilder().AddJsonFile(appSettingsFilePath).Build();
        
        services.AddAuthentication().AddGoogle(googleOptions =>
        {
            parseAuthenticationInformationFromConfigurationFile(configurationRoot, "Google", googleOptions);

        });

        services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
        { 
            parseAuthenticationInformationFromConfigurationFile(configurationRoot, "Microsoft", microsoftOptions);
        });
    }

    private static void parseAuthenticationInformationFromConfigurationFile(IConfigurationRoot configurationRoot, string service, OAuthOptions options)
    {
        string clientIdEnv = Environment.GetEnvironmentVariable($"{service.ToUpper()}_OAUTH_CLIENT_ID");
        options.ClientId = string.IsNullOrEmpty(clientIdEnv)
            ? configurationRoot.GetSection($"Secrets:{service.ToUpper()}")["ClientId"]
              ?? throw new InvalidOperationException("Client ID not found.")
            : clientIdEnv;

        string clientSecretEnv = Environment.GetEnvironmentVariable($"{service.ToUpper()}_OAUTH_CLIENT_SECRET");
        options.ClientSecret = string.IsNullOrEmpty(clientSecretEnv)
            ? configurationRoot.GetSection($"Secrets:{service.ToUpper()}")["ClientSecret"]
              ?? throw new InvalidOperationException("Client secret not found.")
            : clientSecretEnv;
       
    }
}