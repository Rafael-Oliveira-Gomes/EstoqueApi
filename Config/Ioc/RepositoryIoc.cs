using EstoqueApi.Interface.Repository;
using EstoqueApi.Repository;

namespace EstoqueApi.Config.Ioc;

public static class RepositoryIoc
{
    public static void ConfigRepositoryIoc(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
    }
}