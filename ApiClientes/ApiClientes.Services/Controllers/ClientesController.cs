using ApiClientes.Data.Entities;
using ApiClientes.Data.Interfaces;
using ApiClientes.Services.Models;
using ApiClientes.Services.Models.Requests;
using ApiClientes.Services.Models.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiClientes.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        //atributos
        private readonly IClienteRepository? _clienteRepository;
        private readonly IMapper? _mapper;

        //construtor com entrada de argumentos (inicialização dos atributos)
        public ClientesController(IClienteRepository? clienteRepository, IMapper? mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Serviço para cadastro de clientes.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ClientesResponse), StatusCodes.Status201Created)]
        public IActionResult Post(ClientesCreateRequest request)
        {
            try
            {
                var response = new ClientesResponse();

                var cliente = _mapper?.Map<Cliente>(request);
                _clienteRepository?.Add(cliente);

                response.Status = 201;
                response.Mensagem = "Cliente cadastrado com sucesso.";
                response.Cliente = _mapper?.Map<ClientesViewModel>(cliente);

                return StatusCode(response.Status.Value, response);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Serviço para edição de clientes.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ClientesResponse), StatusCodes.Status200OK)]
        public IActionResult Put(ClientesUpdateRequest request)
        {
            try
            {
                var response = new ClientesResponse();

                //buscar o cliente no banco de dados através do ID
                var cliente = _clienteRepository?.GetById(request.Id.Value);

                //verificando se o cliente foi encontrado
                if(cliente != null)
                {
                    cliente.Nome = request.Nome;
                    cliente.Email = request.Email;
                    cliente.Telefone = request.Telefone;

                    _clienteRepository.Update(cliente);

                    response.Status = 200;
                    response.Mensagem = "Cliente atualizado com sucesso.";
                    response.Cliente = _mapper.Map<ClientesViewModel>(cliente);                    
                }
                else
                {
                    response.Status = 404;
                    response.Mensagem = "Cliente não encontrado, verifique o ID informado.";
                }

                return StatusCode(response.Status.Value, response);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Serviço para exclusão de clientes.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ClientesResponse), StatusCodes.Status200OK)]
        public IActionResult Delete(Guid? id)
        {
            try
            {
                var response = new ClientesResponse();

                //buscar 1 cliente no banco de dados através do ID
                var cliente = _clienteRepository?.GetById(id.Value); 

                //verificar se o cliente foi encontrado
                if(cliente != null)
                {
                    //excluindo o cliente
                    _clienteRepository?.Delete(cliente);

                    response.Status = 200;
                    response.Mensagem = "Cliente excluído com sucesso.";
                    response.Cliente = _mapper?.Map<ClientesViewModel>(cliente);
                }
                else
                {
                    response.Status = 404;
                    response.Mensagem = "Cliente não encontrado, verifique o ID informado.";
                }

                return StatusCode(response.Status.Value, response);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Serviço para consultar todos os clientes cadastrados.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ClientesViewModel>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                //consultar todos os clientes cadastrados na base de dados
                var clientes = _clienteRepository?.GetAll();

                //copiar os dados para uma lista de ClientesViewModel
                var response = _mapper?.Map<List<ClientesViewModel>>(clientes);
                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Serviço para consultar 1 cliente através do id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientesViewModel), StatusCodes.Status200OK)]
        public IActionResult GetById(Guid? id)
        {
            try
            {
                //consultar 1 cliente através do ID
                var cliente = _clienteRepository?.GetById(id.Value);

                //verificar se o cliente foi encontrado
                if(cliente != null)
                {
                    //copiar os dados do cliente encontrado para um objeto ClientesViewModel
                    var response = _mapper?.Map<ClientesViewModel>(cliente);
                    return StatusCode(200, response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
