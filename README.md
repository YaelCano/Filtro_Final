

Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no han realizado ningún pedido.
        public async Task<List<object>> lisTClients()
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

Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos que no han sido entregados a tiempo.

Devuelve un listado de los productos que nunca han aparecido en unpedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto. Solucion:
        async Task<IEnumerable<object>> IProducto.ProductNotExist()
        {
            var productosSinPedido = await _context.Productos
                .Where(p => !_context.DetallePedidos.Any(dp => dp.CodigoProducto == p.Id))
                .ToListAsync();

            return productosSinPedido.Cast<object>().ToList();
        }

Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.
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


Lista las ventas totales de los productos que hayan facturado más de 3000 euros. Se mostrará el nombre, unidades vendidas, total facturado y total facturado con impuestos (21% IVA).

Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos empleados que no sean representante de ventas de ningún cliente.

Devuelve el nombre del producto del que se han vendido más unidades. (Tenga en cuenta que tendrá que calcular cuál es el número total de unidades que se han vendido de cada producto a partir de los datos de la tabla detalle_pedido)