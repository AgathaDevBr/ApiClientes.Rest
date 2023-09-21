using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ApiClientes.Services.Configurations
{
    /// <summary>
    /// Classe para customizar a documentação gerada pelo Swagger
    /// </summary>
    public class SwaggerConfiguration
    {
        public static void AddSwagger(WebApplicationBuilder builder)
        {
            //habilitando a geração da documentação da API
            builder.Services.AddEndpointsApiExplorer();

            //customizando a documentação
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API de controle de clientes - COTI Informática",
                    Description = "API REST desenvolvida em AspNet 7 com EntityFramework",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "COTI Informática",
                        Email = "contato@cotiinformatica.com.br",
                        Url = new Uri("http://www.cotiinformatica.com.br")
                    }
                });

                //gerando um arquivo XML dentro da pasta de compilação do projeto
                //contendo os comentários XML feitos no código fonte
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //configurando o swagger para incluir os comentarios XML do código
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
