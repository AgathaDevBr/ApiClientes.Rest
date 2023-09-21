using ApiClientes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientes.Data.Interfaces
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        void Reactivate(Guid id);
        Cliente GetByCpf(string cpf);
    }
}
