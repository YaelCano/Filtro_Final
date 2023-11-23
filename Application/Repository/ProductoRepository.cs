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
    public class ProductoRepository : GenericStringRepository<Producto>, IProducto
    {
        public FinalContext _context { get; set; }
        public ProductoRepository(FinalContext context) : base(context)
        {
            _context = context;
        }
        async Task<IEnumerable<object>> IProducto.ProductNotExist()
        {
            var productosSinPedido = await _context.Productos
                .Where(p => !_context.DetallePedidos.Any(dp => dp.CodigoProducto == p.Id))
                .ToListAsync();

            return productosSinPedido.Cast<object>().ToList();
        }
        public async Task<IEnumerable<object>> ListFrutis()
        {
            var result = from dp in context.DetallePedido
                         group dp by dp.codigo_producto into g
                         let totalFacturado = g.Sum(dp => dp.precio_unidad * dp.cantidad)
                         where totalFacturado > 3000
                         select new
                         {
                             CodigoProducto = g.Key,
                             UnidadesVendidas = g.Sum(dp => dp.cantidad),
                             TotalFacturado = totalFacturado,
                             TotalFacturadoConImpuestos = totalFacturado * 1.21
                         };
            return await result.ToListAsync();
        }
    }
}
