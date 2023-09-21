namespace ApiClientes.Services.Models
{
    /// <summary>
    /// Modelo de dados para a entidade Cliente na API
    /// </summary>
    public class ClientesViewModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string? Telefone { get; set; }
        public DateTime? DataHoraCriacao { get; set; }
        public DateTime? DataHoraUltimaAlteracao { get; set; }
    }
}
