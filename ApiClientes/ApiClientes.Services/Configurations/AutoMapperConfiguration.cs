namespace ApiClientes.Services.Configurations
{
    /// <summary>
    /// Classe para adicionar a configuração do AutoMapper
    /// </summary>
    public class AutoMapperConfiguration
    {
        public static void AddAutoMapper(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
