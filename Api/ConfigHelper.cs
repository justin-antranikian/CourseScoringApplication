namespace Api;

public class ConfigHelper(IServiceCollection services, IConfiguration configuration)
{
    public void RegisterConfig<T>() where T : class, IConfigSectionName
    {
        var configurationSection = configuration.GetSection(T.SectionName);
        services.Configure<T>(configurationSection);
    }

    public T GetConfig<T>() where T : class, IConfigSectionName
    {
        return configuration.GetSection(T.SectionName).Get<T>()!;
    }
}