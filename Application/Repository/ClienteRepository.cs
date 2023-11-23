using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class ClienteRepository : GenericRepository<Cliente>, ICliente
    {
        public FinalContext _context { get; set; }
        public ClienteRepository(FinalContext context) : base(context)
        {
            _context = context;
        }

        async Task<IEnumerable<object>> ICliente.lisTClients()
        {
            var query1 = await _context.Clientes
    .GroupJoin(_context.Pedidos, c => c.CodigoCliente, p => p.CodigoCliente, (cliente, pedidos) => new
    {
        cliente.NombreCliente,
        CantidadPedidos = pedidos.Count()
    })
    .ToListAsync<object>();

            return query1;
        }
    }
}
