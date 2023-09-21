using ApiClientes.Data.Contexts;
using ApiClientes.Data.Entities;
using ApiClientes.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientes.Data.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public override void Delete(Cliente entity)
        {
            using (var context = new DataContext())
            {
                entity.Ativo = 0;

                context.Update(entity);
                context.SaveChanges();
            }
        }

        public override List<Cliente> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Clientes.Where(c => c.Ativo == 1).ToList();
            }
        }

        public override Cliente GetById(Guid id)
        {
            using (var context = new DataContext())
            {
                return context.Clientes.FirstOrDefault
                    (c => c.Id == id && c.Ativo == 1);
            }
        }

        public void Reactivate(Guid id)
        {
            using (var context = new DataContext())
            {
                var cliente = context.Clientes.Find(id);
                cliente.Ativo = 1;

                context.Update(cliente);
                context.SaveChanges();
            }
        }

        public Cliente GetByCpf(string cpf)
        {
            using (var context = new DataContext())
            {
                return context.Clientes.FirstOrDefault(c => c.Cpf == cpf);
            }
        }
    }
}
