using ApiClientes.Data.Entities;
using ApiClientes.Services.Models.Requests;
using AutoMapper;

namespace ApiClientes.Services.Mappings
{
    public class ModelToEntityMap : Profile
    {
        public ModelToEntityMap()
        {
            CreateMap<ClientesCreateRequest, Cliente>()
                .AfterMap((request, entity) => 
                {
                    entity.Id = Guid.NewGuid();
                    entity.DataHoraCriacao = DateTime.Now;
                    entity.DataHoraUltimaAlteracao = DateTime.Now;
                    entity.Ativo = 1;
                });
        }
    }
}
