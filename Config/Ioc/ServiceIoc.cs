using EstoqueApi.Interface.Service;
using EstoqueApi.Service;

namespace EstoqueApi.Config.Ioc;

public static class ServiceIoc
{
    public static void ConfigServiceIoc(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProdutoService, ProdutoService>();
    }
}