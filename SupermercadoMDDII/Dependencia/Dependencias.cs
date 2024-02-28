using Microsoft.EntityFrameworkCore;
using SupermercadoMDDII.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SupermercadoMDDII.Dependencia
{
    public static class Dependencias
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<SupermercadoContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("CadenaSQL"));
                options.UseSqlServer("Server=demoappdb,8002;Database=BaseSupermercado;User Id=sa;Password=Alexander2198;");
            });
        }
        //services.AddScoped<CorreoService>();
    }
}