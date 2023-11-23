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
    public class OficinaRepository : GenericStringRepository<Oficina> , IOficina 
    { 
        public FinalContext _context { get; set; } 
        public OficinaRepository(FinalContext context) : base(context) 
        { 
            _context = context; 
        } 
        public async Task<IEnumerable<object>> Query10MultiEx()
{
        var result = from o in context.Oficina
                    where !(
                        from ofi in context.Oficina
                        join e in context.Empleado on ofi.codigo_oficina equals e.codigo_oficina
                        join c in context.Cliente on e.codigo_empleado equals c.codigo_empleado_rep_ventas
                        join pe in context.Pedido on c.codigo_cliente equals pe.codigo_cliente
                        join dp in context.DetallePedido on pe.codigo_pedido equals dp.codigo_pedido
                        join pr in context.Producto on dp.codigo_producto equals pr.codigo_producto
                        where pr.gama == "Frutales"
                        select ofi.codigo_oficina
                    ).Contains(o.codigo_oficina)
                    select o;
        return await result.ToListAsync();
    }
    } 
} 
