using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientes.Tests.Helpers
{
    /// <summary>
    /// Métodos auxiliares para desenvolvimento das rotinas de teste
    /// </summary>
    public class TestHelper
    {
        /// <summary>
        /// Método para retornar uma instancia de HttpClient
        /// já conectado na API de clientes
        /// </summary>
        public static HttpClient CreateClient
            => new WebApplicationFactory<Program>().CreateClient();

        /// <summary>
        /// Método para receber o objeto de uma requisição
        /// e serializa-lo para JSON para fazer o envio
        /// </summary>
        public static StringContent CreateContent<T>(T request)
            => new StringContent(JsonConvert.SerializeObject(request),
                Encoding.UTF8, "application/json");

        /// <summary>
        /// Método para deserializar um objeto JSON obtido
        /// de uma requisiçaõ feita para a API
        /// </summary>
        public static T ReadContent<T>(HttpResponseMessage response)
            => JsonConvert.DeserializeObject<T>
                (response.Content.ReadAsStringAsync().Result);
    }
}
