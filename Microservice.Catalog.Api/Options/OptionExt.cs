namespace Microservice.Catalog.Api.Options
{
    public static class OptionExt
    {
        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
        {
            services.AddOptions<MongoOption>()
                .BindConfiguration("MongoOption")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }
    }
}
