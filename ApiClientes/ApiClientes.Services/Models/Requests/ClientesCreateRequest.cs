using System.ComponentModel.DataAnnotations;

namespace ApiClientes.Services.Models.Requests
{
    /// <summary>
    /// Modelo de dados para o serviço de cadastro de cliente
    /// </summary>
    public class ClientesCreateRequest
    {
        [Required(ErrorMessage = "Nome do cliente é obrigatório.")]
        [RegularExpression(pattern: "^[A-Za-zÀ-Üà-ü\\s]{6,150}$", 
            ErrorMessage = "Informe um nome válido de 6 a 150 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Email do cliente é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Cpf do cliente é obrigatório.")]
        [RegularExpression(pattern: "^[0-9]{11}$", 
            ErrorMessage = "Informe um cpf com exatamente 11 dígitos.")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "Telefone do cliente é obrigatório.")]
        [RegularExpression(pattern: "[() 0-9]{5,20}$", 
            ErrorMessage = "Informe um telefone no formato '(00)000000000'.")]
        public string? Telefone { get; set; }
    }
}
