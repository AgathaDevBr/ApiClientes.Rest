namespace ApiClientes.Services.Models.Responses
{
    /// <summary>
    /// Modelo de dados para resposta dos processamentos de
    /// CREATE, UPDATE ou DELETE para cliente
    /// </summary>
    public class ClientesResponse
    {
        public int? Status { get; set; }
        public string? Mensagem { get; set; }
        public ClientesViewModel? Cliente { get; set; }
    }
}
