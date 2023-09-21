using ApiClientes.Data.Entities;
using ApiClientes.Services.Models;
using AutoMapper;

namespace ApiClientes.Services.Mappings
{
    public class EntityToModelMap : Profile
    {
        public EntityToModelMap()
        {
            CreateMap<Cliente, ClientesViewModel>();
        }
    }
}
