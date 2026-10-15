using System;
using Microsoft.Extensions.DependencyInjection;

namespace Neptune.API.Services.AI;

/// <summary>
/// NPT-1133: picks how WQMP extraction reaches Claude. Shared by Neptune.API's Startup and
/// Neptune.Eval so both resolve <see cref="NeptuneConfiguration.ClaudeCliEnabled"/> the same way.
/// </summary>
public static class WqmpExtractionClientRegistration
{
    /// <param name="cliAllowed">True only for local development; an unset flag defaults to this.</param>
    /// <param name="environmentName">Named in the startup error when the flag is on where it isn't allowed.</param>
    public static void Add(IServiceCollection services, NeptuneConfiguration configuration, bool cliAllowed, string environmentName)
    {
        var useCli = configuration.ClaudeCliEnabled ?? cliAllowed;
        if (useCli && !cliAllowed)
        {
            throw new InvalidOperationException(
                $"ClaudeCliEnabled is true, but the Claude CLI extraction path is only for local development (ASPNETCORE_ENVIRONMENT=Development; this is '{environmentName}'). Remove ClaudeCliEnabled from this environment's configuration.");
        }

        // Resolve the default once so everything reading the options sees the same answer.
        configuration.ClaudeCliEnabled = useCli;
        services.PostConfigure<NeptuneConfiguration>(c => c.ClaudeCliEnabled = useCli);

        if (useCli)
        {
            services.AddScoped<IWqmpExtractionClient, ClaudeCliWqmpExtractionClient>();
        }
        else
        {
            services.AddScoped<IWqmpExtractionClient, AnthropicApiWqmpExtractionClient>();
        }
    }
}
