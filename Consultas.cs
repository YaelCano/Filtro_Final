//Consultas Requeridas
//1. Devuelve un listado con el código de oficina y la ciudad donde hay oficinas
public async Task<IEnumerable<object>> Query1Requerid()
{
    var result = from o in context.Oficina
                    select new { o.codigo_oficina, o.ciudad };
    return await result.ToListAsync();
}
//2. Devuelve un listado con la ciudad y el teléfono de las oficinas de España.
public async Task<IEnumerable<object>> Query2Requerid()
{
    var result = from o in context.Oficina
                    where o.pais == "España"
                    select new { o.ciudad, o.telefono };
    return await result.ToListAsync();
}
//3. Devuelve un listado con el nombre, apellidos y email de los empleados cuyo jefe tiene un código de jefe igual a 7.
public async Task<IEnumerable<object>> Query3Requerid()
{
    var result = from e in context.Empleado
                    where e.codigo_jefe == 7
                    select new { e.nombre, e.apellido1, e.apellido2, e.email };
    return await result.ToListAsync();
}
//4. Devuelve el nombre del puesto, nombre, apellidos y email del jefe de la empresa.
public async Task<IEnumerable<object>> Query4Requerid()
{
    var result = from e in context.Empleado
                    where e.codigo_jefe == null
                    select new { e.puesto, e.nombre, e.apellido1, e.apellido2, e.email };
    return await result.ToListAsync();
}
//5. Devuelve un listado con el nombre, apellidos y puesto de aquellos empleados que no sean representantes de ventas.
public async Task<IEnumerable<object>> Query5Requerid()
{
    var result = from e in context.Empleado
                    where e.puesto != "Representante Ventas"
                    select new { e.nombre, e.apellido1, e.apellido2, e.puesto };
    return await result.ToListAsync();
}
//6. Devuelve un listado con el nombre de los todos los clientes españoles.
public async Task<IEnumerable<object>> Query6Requerid()
{
    var result = from c in context.Cliente
                    where c.pais == "Spain"
                    select c.nombre_cliente;
    return await result.ToListAsync();
}
//7. Devuelve un listado con los distintos estados por los que puede pasar un pedido.
public async Task<IEnumerable<object>> Query7Requerid()
{
    var result = (from p in context.Pedido
                    select p.estado).Distinct();
    return await result.ToListAsync();
}
//8. Devuelve un listado con el código de cliente de aquellos clientes que realizaron algún pago en 2008. Tenga en cuenta que deberá eliminar aquellos códigos de cliente que aparezcan repetidos. Resuelva la consulta:
public async Task<IEnumerable<object>> Query8RequeridWithYearFunction()
{
    var result = (from p in context.Pago
                    where p.fecha_pago.Year == 2008
                    select p.codigo_cliente).Distinct();
    return await result.ToListAsync();
}
public async Task<IEnumerable<object>> Query8RequeridWithDateFormatFunction()
{
    var result = (from p in context.Pago
                    where p.fecha_pago.ToString("yyyy") == "2008"
                    select p.codigo_cliente).Distinct();
    return await result.ToListAsync();
}
public async Task<IEnumerable<object>> Query8RequeridWithoutFunctions()
{
    var result = (from p in context.Pago
                    where p.fecha_pago >= new DateTime(2008, 1, 1) && p.fecha_pago <= new DateTime(2008, 12, 31)
                    select p.codigo_cliente).Distinct();
    return await result.ToListAsync();
}
//9. Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos que no han sido entregados a tiempo.

//10. Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al menos dos días antes de la fecha esperada.
public async Task<IEnumerable<object>> Query10RequeridWithAddDateFunction()
{
    var result = from p in context.Pedido
                    where p.fecha_entrega.AddDays(2) <= p.fecha_esperada
                    select new { p.codigo_pedido, p.codigo_cliente, p.fecha_esperada, p.fecha_entrega };
    return await result.ToListAsync();
}
public async Task<IEnumerable<object>> Query10RequeridWithDateDiffFunction()
{
    var result = from p in context.Pedido
                    where (p.fecha_esperada - p.fecha_entrega).TotalDays >= 2
                    select new { p.codigo_pedido, p.codigo_cliente, p.fecha_esperada, p.fecha_entrega };
    return await result.ToListAsync();
}
public async Task<IEnumerable<object>> Query10RequeridWithOperators()
{
    var result = from p in context.Pedido
                    where p.fecha_entrega.AddDays(2) <= p.fecha_esperada
                    select new { p.codigo_pedido, p.codigo_cliente, p.fecha_esperada, p.fecha_entrega };
    return await result.ToListAsync();
}
//11. Devuelve un listado de todos los pedidos que fueron rechazados en 2009.
public async Task<IEnumerable<object>> Query11Requerid()
{
    var result = from p in context.Pedido
                    where p.estado == "Rechazado" && p.fecha_pedido.Year == 2009
                    select p;
    return await result.ToListAsync();
}
//12. Devuelve un listado de todos los pedidos que han sido entregados en el mes de enero de cualquier año.
public async Task<IEnumerable<object>> Query12Requerid()
{
    var result = from p in context.Pedido
                    where p.fecha_entrega.Month == 1
                    select p;
    return await result.ToListAsync();
}
//13. Devuelve un listado con todos los pagos que se realizaron en el año 2008 mediante Paypal. Ordene el resultado de mayor a menor.
public async Task<IEnumerable<object>> Query13Requerid()
{
    var result = from p in context.Pago
                    where p.fecha_pago.Year == 2008 && p.forma_pago == "Paypal"
                    orderby p.monto_pago descending
                    select p;
    return await result.ToListAsync();
}
//14. Devuelve un listado con todas las formas de pago que aparecen en la tabla pago. Tenga en cuenta que no deben aparecer formas de pago repetidas.
public async Task<IEnumerable<object>> Query14Requerid()
{
    var result = (from p in context.Pago
                    select p.forma_pago).Distinct();
    return await result.ToListAsync();
}
//15. Devuelve un listado con todos los productos que pertenecen a la gama Ornamentales y que tienen más de 100 unidades en stock. El listado deberá estar ordenado por su precio de venta, mostrando en primer lugar los de mayor precio.
public async Task<IEnumerable<object>> Query15Requerid()
{
    var result = from p in context.Producto
                    where p.gama == "Ornamentales" && p.cantidad_en_stock > 100
                    orderby p.precio_venta descending
                    select p;
    return await result.ToListAsync();
}
//16. Devuelve un listado con todos los clientes que sean de la ciudad de Madrid y cuyo representante de ventas tenga el código de empleado 11 o 30
public async Task<IEnumerable<object>> Query16Requerid()
{
    var result = from c in context.Cliente
                    where c.ciudad == "Madrid" && (c.codigo_empleado_rep_ventas == 11 || c.codigo_empleado_rep_ventas == 30)
                    select c;
    return await result.ToListAsync();
}


//Consulting MultiIntabla
// 1.Obtén un listado con el nombre de cada cliente y el nombre y apellido de su representante de ventas.
public async Task<IEnumerable<object>> Query1MultiInt()
{
    var result = from c in context.Cliente
                    join e in context.Empleado on c.codigo_empleado_rep_ventas equals e.codigo_empleado
                    select new { c.nombre_cliente, e.nombre, e.apellido1, e.apellido2 };
    return await result.ToListAsync();
}
// 2. Muestra el nombre de los clientes que hayan realizado pagos junto con el nombre de sus representantes de ventas.
public async Task<IEnumerable<object>> Query2MultiInt()
{
    var result = from c in context.Cliente
                    join p in context.Pago on c.codigo_cliente equals p.codigo_cliente
                    join e in context.Empleado on c.codigo_empleado_rep_ventas equals e.codigo_empleado
                    select new { c.nombre_cliente, e.nombre, e.apellido1, e.apellido2 };
    return await result.ToListAsync();
}
// 3. Muestra el nombre de los clientes que no hayan realizado pagos junto con el nombre de sus representantes de ventas.
public async Task<IEnumerable<object>> Query3MultiInt()
{
    var result = from c in context.Cliente
                    join p in context.Pago on c.codigo_cliente equals p.codigo_cliente into gj
                    from subp in gj.DefaultIfEmpty()
                    join e in context.Empleado on c.codigo_empleado_rep_ventas equals e.codigo_empleado
                    where subp == null
                    select new { c.nombre_cliente, e.nombre, e.apellido1, e.apellido2 };
    return await result.ToListAsync();
}
// 4. Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.
public async Task<IEnumerable<object>> Query4MultiInt()
{
    var result = from c in context.Cliente
                    join p in context.Pago on c.codigo_cliente equals p.codigo_cliente
                    join e in context.Empleado on c.codigo_empleado_rep_ventas equals e.codigo_empleado
                    join o in context.Oficina on e.codigo_oficina equals o.codigo_oficina
                    select new { c.nombre_cliente, e.nombre, e.apellido1, e.apellido2, o.ciudad };
    return await result.ToListAsync();
}
// 5. Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.
public async Task<IEnumerable<object>> Query5MultiInt()
{
    var result = from c in context.Cliente
                    join p in context.Pago on c.codigo_cliente equals p.codigo_cliente into gj
                    from subp in gj.DefaultIfEmpty()
                    join e in context.Empleado on c.codigo_empleado_rep_ventas equals e.codigo_empleado
                    join o in context.Oficina on e.codigo_oficina equals o.codigo_oficina
                    where subp == null
                    select new { c.codigo_cliente, c.nombre_cliente, e.nombre, e.apellido1, e.apellido2, o.ciudad };
    return await result.ToListAsync();
}
// 6. Lista la dirección de las oficinas que tengan clientes en Fuenlabrada.
public async Task<IEnumerable<object>> Query6MultiInt()
{
    var result = from c in context.Cliente
                    join p in context.Pago on c.codigo_cliente equals p.codigo_cliente
                    join e in context.Empleado on c.codigo_empleado_rep_ventas equals e.codigo_empleado
                    join o in context.Oficina on e.codigo_oficina equals o.codigo_oficina
                    where c.ciudad == "Fuenlabrada"
                    select new { o.linea_direccion1, o.linea_direccion2 };
    return await result.Distinct().ToListAsync();
}
// 7. Devuelve el nombre de los clientes y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.
public async Task<IEnumerable<object>> Query7MultiInt()
{
    var result = from c in context.Cliente
                    join e in context.Empleado on c.codigo_empleado_rep_ventas equals e.codigo_empleado
                    join o in context.Oficina on e.codigo_oficina equals o.codigo_oficina
                    select new { c.nombre_cliente, Representante = e.nombre + " " + e.apellido1 + " " + e.apellido2, o.ciudad };
    return await result.ToListAsync();
}
// 8. Devuelve un listado con el nombre de los empleados junto con el nombre de sus jefes.
public async Task<IEnumerable<object>> Query8MultiInt()
{
    var result = from e in context.Empleado
                    join j in context.Empleado on e.codigo_jefe equals j.codigo_empleado
                    select new { Empleado = e.nombre + " " + e.apellido1 + " " + e.apellido2, Jefe = j.nombre + " " + j.apellido1 + " " + j.apellido2 };
        return await result.ToListAsync();
    }

// 9. Devuelve un listado que muestre el nombre de cada empleados, el nombre de su jefe y el nombre del jefe de sus jefe.
public async Task<IEnumerable<object>> Query9MultiInt()
{
    var result = from e in context.Empleado
                join j in context.Empleado on e.codigo_jefe equals j.codigo_empleado
                join m in context.Empleado on j.codigo_jefe equals m.codigo_empleado
                select new { Empleado = e.nombre + " " + e.apellido1 + " " + e.apellido2, Jefe = j.nombre + " " + j.apellido1 + " " + j.apellido2, JefeDelJefe = m.nombre + " " + m.apellido1 + " " + m.apellido2 };
    return await result.ToListAsync();
}
// 10. Devuelve el nombre de los clientes a los que no se les ha entregado a tiempo un pedido.
public async Task<IEnumerable<object>> Query10MultiInt()
{
    var result = from c in context.Cliente
                    join p in context.Pedido on c.codigo_cliente equals p.codigo_cliente
                    where p.fecha_entrega > p.fecha_esperada
                    select c.nombre_cliente;
    return await result.Distinct().ToListAsync();
}
// 11. Devuelve un listado de las diferentes gamas de producto que ha comprado cada cliente.
public async Task<IEnumerable<object>> Query11MultiInt()
{
    var result = from c in context.Cliente
                    join pe in context.Pedido on c.codigo_cliente equals pe.codigo_cliente
                    join dp in context.DetallePedido on pe.codigo_pedido equals dp.codigo_pedido
                    join pr in context.Producto on dp.codigo_producto equals pr.codigo_producto
                    select new { c.nombre_cliente, pr.gama };
    return await result.Distinct().ToListAsync();
}


//Consulting MultiExtabla
// 1.Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago.
public async Task<IEnumerable<object>> Query1MultiEx()
{
        var result = from c in context.Cliente
                    join p in context.Pago on c.codigo_cliente equals p.codigo_cliente into gj
                    from subp in gj.DefaultIfEmpty()
                    where subp == null
                    select c.nombre_cliente;
        return await result.ToListAsync();
    }
// 2. Devuelve un listado que muestre solamente los clientes que no han realizado ningún pedido.
public async Task<IEnumerable<object>> Query2MultiEx()
{
        var result = from c in context.Cliente
                    join pe in context.Pedido on c.codigo_cliente equals pe.codigo_cliente into gj
                    from subpe in gj.DefaultIfEmpty()
                    where subpe == null
                    select c.nombre_cliente;
        return await result.ToListAsync();
    }
// 3. Devuelve un listado que muestre los clientes que no han realizado ningún pago y los que no han realizado ningún pedido.
public async Task<IEnumerable<object>> Query3MultiEx()
{
        var result = from c in context.Cliente
                    join pe in context.Pedido on c.codigo_cliente equals pe.codigo_cliente into gj1
                    from subpe in gj1.DefaultIfEmpty()
                    join pa in context.Pago on c.codigo_cliente equals pa.codigo_cliente into gj2
                    from subpa in gj2.DefaultIfEmpty()
                    where subpe == null && subpa == null
                    select c.nombre_cliente;
        return await result.ToListAsync();
    }
// 4. Devuelve un listado que muestre solamente los empleados que no tienen una oficina asociada.
public async Task<IEnumerable<object>> Query4MultiEx()
{
        var result = from e in context.Empleado
                    join o in context.Oficina on e.codigo_oficina equals o.codigo_oficina into gj
                    from subo in gj.DefaultIfEmpty()
                    where subo == null
                    select new { Empleado = $"{e.nombre} {e.apellido1} {e.apellido2}" };
        return await result.ToListAsync();
    }
// 5. Devuelve un listado que muestre solamente los empleados que no tienen un cliente asociado.
public async Task<IEnumerable<object>> Query5MultiEx()
{
        var result = from e in context.Empleado
                    join c in context.Cliente on e.codigo_empleado equals c.codigo_empleado_rep_ventas into gj
                    from subc in gj.DefaultIfEmpty()
                    where subc == null
                    select new { Empleado = $"{e.nombre} {e.apellido1} {e.apellido2}" };
        return await result.ToListAsync();
    }
// 6. Devuelve un listado que muestre solamente los empleados que no tienen un cliente asociado junto con los datos de la oficina donde trabajan.
public async Task<IEnumerable<object>> Query6MultiEx()
{
        var result = from e in context.Empleado
                    join c in context.Cliente on e.codigo_empleado equals c.codigo_empleado_rep_ventas into gj1
                    from subc in gj1.DefaultIfEmpty()
                    join o in context.Oficina on e.codigo_oficina equals o.codigo_oficina
                    where subc == null
                    select new { Empleado = $"{e.nombre} {e.apellido1} {e.apellido2}", o.linea_direccion1, o.linea_direccion2, o.ciudad, o.region, o.pais };
        return await result.Distinct().ToListAsync();
    }
// 7. Devuelve un listado que muestre los empleados que no tienen una oficina asociada y los que no tienen un cliente asociado.
public async Task<IEnumerable<object>> Query7MultiEx()
{
        var result = from e in context.Empleado
                    join c in context.Cliente on e.codigo_empleado equals c.codigo_empleado_rep_ventas into gj1
                    from subc in gj1.DefaultIfEmpty()
                    join o in context.Oficina on e.codigo_oficina equals o.codigo_oficina into gj2
                    from subo in gj2.DefaultIfEmpty()
                    where subo == null || subc == null
                    select new { Empleado = $"{e.nombre} {e.apellido1} {e.apellido2}" };
        return await result.Distinct().ToListAsync();
    }
// 8. Devuelve un listado de los productos que nunca han aparecido en un pedido.
public async Task<IEnumerable<object>> Query8MultiEx()
{
        var result = from p in context.Producto
                    join dp in context.DetallePedido on p.codigo_producto equals dp.codigo_producto into gj
                    from subdp in gj.DefaultIfEmpty()
                    where subdp == null
                    select p.nombre;
        return await result.ToListAsync();
    }
// 9. Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto.
public async Task<IEnumerable<object>> Query9MultiEx()
{
        var result = from p in context.Producto
                    join dp in context.DetallePedido on p.codigo_producto equals dp.codigo_producto into gj1
                    from subdp in gj1.DefaultIfEmpty()
                    join pe in context.Pedido on subdp.codigo_pedido equals pe.codigo_pedido into gj2
                    from subpe in gj2.DefaultIfEmpty()
                    where subdp == null
                    select new { p.nombre, p.descripcion, subpe.imagen };
        return await result.ToListAsync();
    }
// 10. Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.
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
// 11. Devuelve un listado con los clientes que han realizado algún pedido pero no han realizado ningún pago.
public async Task<IEnumerable<object>> Query11MultiEx()
{
        var result = from c in context.Cliente
                    join pe in context.Pedido on c.codigo_cliente equals pe.codigo_cliente into gj1
                    from subpe in gj1.DefaultIfEmpty()
                    join pa in context.Pago on c.codigo_cliente equals pa.codigo_cliente into gj2
                    from subpa in gj2.DefaultIfEmpty()
                    where subpe != null && subpa == null
                    select new { c.codigo_cliente, c.nombre_cliente, c.nombre_contacto, c.apellido_contacto, c.telefono };
        return await result.Distinct().ToListAsync();
    }
// 12. Devuelve un listado con los datos de los empleados que no tienen clientes asociados y el nombre de su jefe asociado
public async Task<IEnumerable<object>> Query12()
{
    var result = from e in context.Empleado
                    join c in context.Cliente on e.codigo_empleado equals c.codigo_empleado_rep_ventas into gj
                    from subc in gj.DefaultIfEmpty()
                    join j in context.Empleado on e.codigo_jefe equals j.codigo_empleado
                    where subc == null
                    select new { Empleado = $"{e.nombre} {e.apellido1} {e.apellido2}", e.Email, e.Puesto, Jefe = $"{j.nombre} {j.apellido1} {j.apellido2}" };
    return await result.ToListAsync();
}

//Summary
// 1. ¿Cuántos empleados hay en la compañía?
public async Task<IEnumerable<object>> Query1Summary()
{
    var result = context.Empleado.Count();
    return await Task.FromResult(new List<object> { result });
}
// 2. ¿Cuántos clientes tiene cada país?
public async Task<IEnumerable<object>> Query2Summary()
{
    var result = from c in context.Cliente
                    group c by c.pais into g
                    select new { Pais = g.Key, CantidadClientes = g.Count() };
    return await result.ToListAsync();
}
// 3. ¿Cuál fue el pago medio en 2009?
public async Task<IEnumerable<object>> Query3Summary()
{
    var result = (from p in context.Pago
                    where p.fecha_pago.Year == 2009
                    select p.total).Average();
    return await Task.FromResult(new List<object> { result });
}
// 4. ¿Cuántos pedidos hay en cada estado? Ordena el resultado de forma descendente por el número de pedidos.
public async Task<IEnumerable<object>> Query4Summary()
{
    var result = from p in context.Pedido
                    group p by p.estado into g
                    select new { Estado = g.Key, CantidadPedidos = g.Count() };
    return await result.OrderByDescending(r => r.CantidadPedidos).ToListAsync();
}
// 5. Calcula el precio de venta del producto más caro y más barato en una misma consulta.
public async Task<IEnumerable<object>> Query5Summary()
{
    var result = from p in context.Producto
                    select new { p.precio_venta };
    return await Task.FromResult(new List<object> { result });
}
// 6. Calcula el número de clientes que tiene la empresa.
public async Task<IEnumerable<object>> Query6Summary()
{
    var result = context.Cliente.Count();
    return await Task.FromResult(new List<object> { result });
}
// 7. ¿Cuántos clientes existen con domicilio en la ciudad de Madrid?
public async Task<IEnumerable<object>> Query7Summary()
{
    var result = context.Cliente.Count(c => c.ciudad == "Madrid");
    return await Task.FromResult(new List<object> { result });
}
// 8. ¿Calcula cuántos clientes tiene cada una de las ciudades que empiezan por M?
public async Task<IEnumerable<object>> Query8Summary()
{
    var result = from c in context.Cliente
                    where c.ciudad.StartsWith("M")
                    group c by c.ciudad into g
                    select new { Ciudad = g.Key, CantidadClientes = g.Count() };
    return await result.ToListAsync();
}
// 9. Devuelve el nombre de los representantes de ventas y el número de clientes al que atiende cada uno.
public async Task<IEnumerable<object>> Query9Summary()
{
    var result = from c in context.Cliente
                    group c by c.codigo_empleado_rep_ventas into g
                    select new { IdEmpleado = g.Key, CantidadClientes = g.Count() };
    return await result.ToListAsync();
}
// 10. Calcula el número de clientes que no tiene asignado representante de ventas.
public async Task<IEnumerable<object>> Query10Summary()
{
    var result = context.Cliente.Count(c => c.codigo_empleado_rep_ventas == null);
    return await Task.FromResult(new List<object> { result });
}
// 11. Calcula la fecha del primer y último pago realizado por cada uno de los clientes. El listado deberá mostrar el nombre y los apellidos de cada cliente.
public async Task<IEnumerable<object>> Query11Summary()
{
    var result = from p in context.Pago
                    group p by p.codigo_cliente into g
                    select new
                    {
                        CodigoCliente = g.Key,
                        PrimerPago = g.Min(p => p.fecha_pago),
                        UltimoPago = g.Max(p => p.fecha_pago)
                    };
    return await result.Select(r => new { Nombre = $"{r.PrimerPago} - {r.UltimoPago}" }).ToListAsync();
}
// 12. Calcula el número de productos diferentes que hay en cada uno de los pedidos.
public async Task<IEnumerable<object>> Query12Summary()
{
    var result = from dp in context.DetallePedido
                    group dp by dp.codigo_pedido into g
                    select new { CodigoPedido = g.Key, CantidadProductosDiferentes = g.Select(dp => dp.codigo_producto).Distinct().Count() };
    return await result.ToListAsync();
}
// 13. Calcula la suma de la cantidad total de todos los productos que aparecen en cada uno de los pedidos.
public async Task<IEnumerable<object>> Query13Summary()
{
    var result = from dp in context.DetallePedido
                    group dp by dp.codigo_pedido into g
                    select new { CodigoPedido = g.Key, SumaCantidadTotal = g.Sum(dp => dp.cantidad) };
    return await result.ToListAsync();
}
// 14. Devuelve un listado de los 20 productos más vendidos y el número total de unidades que se han vendido de cada uno. El listado deberá estar ordenado por el número total de unidades vendidas.
public async Task<IEnumerable<object>> Query14Summary()
{
    var result = (from dp in context.DetallePedido
                    group dp by dp.codigo_producto into g
                    orderby g.Sum(dp => dp.cantidad) descending
                    select new { CodigoProducto = g.Key, UnidadesVendidas = g.Sum(dp => dp.cantidad) }).Take(20);
    return await result.ToListAsync();
}
// 15. La facturación que ha tenido la empresa en toda la historia, indicando la base imponible, el IVA y el total facturado. La base imponible se calcula sumando el coste del producto por el número de unidades vendidas de la tabla detalle_pedido. El IVA es el 21 % de la base imponible, y el total la suma de los dos campos anteriores.
public async Task<IEnumerable<object>> Query15Summary()
{
    var result = (from dp in context.DetallePedido
                    select dp.precio_unidad * dp.cantidad).Sum();
    var iva = result * 0.21;
    return await Task.FromResult(new List<object>
    {
        new
        {
            BaseImponible = result,
            IVA = iva,
            Total = result + iva
        }
    });
}
// 16. La misma información que en la pregunta anterior, pero agrupada por código de producto.
public async Task<IEnumerable<object>> Query16Summary()
{
    var result = from dp in context.DetallePedido
                    group dp by dp.codigo_producto into g
                    let baseImponible = g.Sum(dp => dp.precio_unidad * dp.cantidad)
                    let iva = baseImponible * 0.21
                    let total = baseImponible + iva
                    select new
                    {
                        CodigoProducto = g.Key,
                        BaseImponible = baseImponible,
                        IVA = iva,
                        Total = total
                    };
    return await result.ToListAsync();
}
// 17. La misma información que en la pregunta anterior, pero agrupada por código de producto filtrada por los códigos que empiecen por OR.
public async Task<IEnumerable<object>> Query17Summary()
{
    var result = (from dp in context.DetallePedido
                    where dp.codigo_producto.StartsWith("OR")
                    group dp by dp.codigo_producto into g
                    let baseImponible = g.Sum(dp => dp.precio_unidad * dp.cantidad)
                    let iva = baseImponible * 0.21
                    let total = baseImponible + iva
                    select new
                    {
                        CodigoProducto = g.Key,
                        BaseImponible = baseImponible,
                        IVA = iva,
                        Total = total
                    });
    return await result.ToListAsync();
}
// 18. Lista las ventas totales de los productos que hayan facturado más de 3000 euros. Se mostrará el nombre, unidades vendidas, total facturado y total facturado con impuestos (21% IVA).
public async Task<IEnumerable<object>> Query18Summary()
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
// 19. Muestre la suma total de todos los pagos que se realizaron para cada uno de los años que aparecen en la tabla pagos.
public async Task<IEnumerable<object>> Query19Summary()
{
    var result = from p in context.Pago
                    group p by p.fecha_pago.Year into g
                    select new { Año = g.Key, SumaTotal = g.Sum(p => p.total) };
    return await result.ToListAsync();
}

//Consulting With Operator basic
// 1. Devuelve el nombre del cliente con mayor límite de crédito.
    public async Task<List<object>> Query1WithOperatorBasic()
    {
        var nombreCliente = await _context.Clientes
            .Where(c => c.LimiteCredito == _context.Clientes.Max(cl => cl.LimiteCredito))
            .Select(c => c.NombreCliente)
            .ToListAsync();

        return nombreCliente;
    }
// 2. Devuelve el nombre del producto que tenga el precio de venta más caro.
public async Task<List<object>> Query2WithOperatorBasic()
{
    var nombreProducto = await _context.Productos
        .Where(p => p.PrecioVenta == _context.Productos.Max(pr => pr.PrecioVenta))
        .Select(p => p.Nombre)
        .ToListAsync();

    return nombreProducto;
}

// 3. Devuelve el nombre del producto del que se han vendido más unidades.
public async Task<List<object>> Query3WithOperatorBasic()
{
    var nombreProductoMasVendido = await _context.Productos
        .Join(_context.DetallePedidos, p => p.CodigoProducto, dp => dp.CodigoProducto, (p, dp) => new { Producto = p, DetallePedido = dp })
        .GroupBy(x => x.Producto.Nombre)
        .OrderByDescending(g => g.Sum(x => x.DetallePedido.Cantidad))
        .Select(g => g.Key)
        .FirstOrDefaultAsync();

    return nombreProductoMasVendido;
}
// 4. Los clientes cuyo límite de crédito sea mayor que los pagos que hayan realizado.
public async Task<List<object>> Query4WithOperatorBasic()
{
    var clientesLimiteCreditoMayorPagos = await _context.Clientes
        .Where(c => c.LimiteCredito > _context.Pagos.Max(p => p.Total))
        .Select(c => new { c.NombreCliente, c.LimiteCredito })
        .ToListAsync();

    return clientesLimiteCreditoMayorPagos.Cast<object>().ToList();
}

// 5. Devuelve el producto que más unidades tiene en stock.
public async Task<List<object>> Query5WithOperatorBasic()
{
    var productoMasStock = await _context.Productos
        .Where(p => p.CantidadEnStock == _context.Productos.Max(pr => pr.CantidadEnStock))
        .Select(p => p.Nombre)
        .ToListAsync();

    return productoMasStock.Cast<object>().ToList();
}

// 6. Devuelve el producto que menos unidades tiene en stock.
public async Task<List<object>> Query6WithOperatorBasic()
{
    var productoMenosStock = await _context.Productos
        .Where(p => p.CantidadEnStock == _context.Productos.Min(pr => pr.CantidadEnStock))
        .Select(p => p.Nombre)
        .ToListAsync();

    return productoMenosStock.Cast<object>().ToList();
}

// 7. Devuelve el nombre, los apellidos y el email de los empleados que están a cargo de Alberto Soria.
public async Task<List<object>> Query7WithOperatorBasic()
{
    var empleadosACargoAlbertoSoria = await _context.Empleados
        .Where(e => e.CodigoJefe == _context.Empleados
            .Where(e => e.Nombre == "Alberto" && e.Apellido1 == "Soria")
            .Select(e => e.CodigoEmpleado)
            .FirstOrDefault())
        .Select(e => new { Empleado = $"{e.Nombre} {e.Apellido1} {e.Apellido2}", e.Email })
        .ToListAsync();

    return empleadosACargoAlbertoSoria.Cast<object>().ToList();
}

//Consulting with all any
// 1. Devuelve el nombre del cliente con mayor límite de crédito.
public async Task<List<object>> Query1WithAllAny()
{
    var clienteMayorLimiteCredito = await _context.Clientes
        .Where(c => c.LimiteCredito >= _context.Clientes.Max(cli => cli.LimiteCredito))
        .Select(c => c.NombreCliente)
        .ToListAsync();

    return clienteMayorLimiteCredito.Cast<object>().ToList();
}

// 2. Devuelve el nombre del producto que tenga el precio de venta más caro.
public async Task<List<object>> Query2WithAllAny()
{
    var productoMasCaro = await _context.Productos
        .Where(p => p.PrecioVenta >= _context.Productos.Max(pr => pr.PrecioVenta))
        .Select(p => p.Nombre)
        .ToListAsync();

    return productoMasCaro.Cast<object>().ToList();
}

// 3. Devuelve el producto que menos unidades tiene en stock.
public async Task<List<object>> Query3WithAllAny()
{
    var productoMenosStock = await _context.Productos
        .Where(p => p.CantidadEnStock <= _context.Productos.Min(pr => pr.CantidadEnStock))
        .Select(p => p.Nombre)
        .ToListAsync();

    return productoMenosStock.Cast<object>().ToList();
}

//Consulting With In And Not In
// 1. Devuelve el nombre, apellido1 y cargo de los empleados que no representen a ningún cliente.
public async Task<List<object>> Query1WithInNotIn()
{
    var empleadosSinClientes = await _context.Empleados
        .Where(e => !_context.Clientes.Any(c => c.CodigoEmpleadoRepVentas == e.CodigoEmpleado))
        .Select(e => new { e.Nombre, e.Apellido1, e.Apellido2, e.Cargo })
        .ToListAsync();

    return empleadosSinClientes.Cast<object>().ToList();
}

// 2. Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago.
public async Task<List<object>> Query2WithInNotIn()
{
    var clientesSinPago = await _context.Clientes
        .Where(c => !_context.Pagos.Any(p => p.CodigoCliente == c.CodigoCliente))
        .Select(c => c.NombreCliente)
        .ToListAsync();

    return clientesSinPago.Cast<object>().ToList();
}

// 3. Devuelve un listado que muestre solamente los clientes que sí han realizado algún pago.
public async Task<List<object>> Query3WithInNotIn()
{
    var clientesConPago = await _context.Clientes
        .Where(c => _context.Pagos.Any(p => p.CodigoCliente == c.CodigoCliente))
        .Select(c => c.NombreCliente)
        .ToListAsync();

    return clientesConPago.Cast<object>().ToList();
}

// 4. Devuelve un listado de los productos que nunca han aparecido en un pedido.
public async Task<List<object>> Query4WithInNotIn()
{
    var productosSinPedidos = await _context.Productos
        .Where(p => !_context.DetallePedidos.Any(dp => dp.CodigoProducto == p.CodigoProducto))
        .Select(p => p.Nombre)
        .ToListAsync();

    return productosSinPedidos.Cast<object>().ToList();
}

// 5. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos empleados que no sean representante de ventas de ningún cliente.
public async Task<List<object>> Query5WithInNotIn()
{
    var empleadosOficinaSinClientes = await _context.Empleados
        .Where(e => !_context.Clientes.Any(c => c.CodigoEmpleadoRepVentas == e.CodigoEmpleado))
        .Join(_context.Oficinas, e => e.CodigoOficina, o => o.CodigoOficina, (e, o) => new { e, o })
        .Select(x => new { x.e.Nombre, x.e.Apellido1, x.e.Apellido2, x.e.Puesto, x.o.Telefono })
        .ToListAsync();

    return empleadosOficinaSinClientes.Cast<object>().ToList();
}

// 6. Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.
public async Task<List<object>> Query6WithInNotIn()
{
    var oficinasSinRepresentantesFrutales = await _context.Oficinas
        .Where(o => !_context.Empleados
            .Where(e => _context.Clientes
                .Where(c => _context.Pedidos
                    .Where(p => _context.DetallePedidos
                        .Where(dp => _context.Productos
                            .Any(pr => pr.CodigoProducto == dp.CodigoProducto && pr.Gama == "Frutales")
                        )
                        .Any(dp => dp.CodigoPedido == p.CodigoPedido)
                    )
                    .Any(p => p.CodigoCliente == c.CodigoCliente)
                )
                .Any(c => c.CodigoEmpleadoRepVentas == e.CodigoEmpleado)
            )
            .Any(e => e.CodigoOficina == o.CodigoOficina)
        )
        .Select(o => o)
        .ToListAsync();

    return oficinasSinRepresentantesFrutales.Cast<object>().ToList();
}

// 7. Devuelve un listado con los clientes que han realizado algún pedido pero no han realizado ningún pago.
public async Task<List<object>> Query7WithInNotIn()
{
    var clientesConPedidoSinPago = await _context.Clientes
        .Where(c => _context.Pedidos.Any(p => p.CodigoCliente == c.CodigoCliente))
        .Where(c => !_context.Pagos.Any(p => p.CodigoCliente == c.CodigoCliente))
        .Select(c => c.NombreCliente)
        .ToListAsync();

    return clientesConPedidoSinPago.Cast<object>().ToList();
}

//Consulting Exist And Not Exist
// 1. Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago.
public async Task<List<object>> Query1ExistNotExist()
{
    var clientesSinPago = await _context.Clientes
        .Where(c => !_context.Pagos.Any(p => p.CodigoCliente == c.CodigoCliente))
        .ToListAsync();

    return clientesSinPago.Cast<object>().ToList();
}

// 2. Devuelve un listado que muestre solamente los clientes que sí han realizado algún pago.
public async Task<List<object>> Query2ExistNotExist()
{
    var clientesConPago = await _context.Clientes
        .Where(c => _context.Pagos.Any(p => p.CodigoCliente == c.CodigoCliente))
        .ToListAsync();

    return clientesConPago.Cast<object>().ToList();
}

// 3. Devuelve un listado de los productos que nunca han aparecido en un pedido.
public async Task<List<object>> Query3ExistNotExist()
{
    var productosSinPedido = await _context.Productos
        .Where(p => !_context.DetallePedidos.Any(dp => dp.CodigoProducto == p.CodigoProducto))
        .ToListAsync();

    return productosSinPedido.Cast<object>().ToList();
}

// 4. Devuelve un listado de los productos que han aparecido en un pedido alguna vez.
public async Task<List<object>> Query4ExistNotExist()
{
    var productosConPedido = await _context.Productos
        .Where(p => _context.DetallePedidos.Any(dp => dp.CodigoProducto == p.CodigoProducto))
        .ToListAsync();

    return productosConPedido.Cast<object>().ToList();
}

//Consulting Variate

// 1. Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado 
public async Task<List<object>> lisTClient()
{
    var query1 = await _context.Cliente
        .GroupJoin(_context.Pedido, c => c.CodigoCliente, p => p.CodigoCliente, (cliente, pedidos) => new
        {
            cliente.NombreCliente,
            CantidadPedidos = pedidos.Count()
        })
        .ToListAsync<object>();

    return query1;
}

// 2. Devuelve un listado con los nombres de los clientes y el total pagado por cada uno de ellos
public async Task<List<object>> Query1Variate2()
{
    var query2 = await _context.Cliente
        .GroupJoin(_context.Pago, c => c.CodigoCliente, p => p.CodigoCliente, (cliente, pagos) => new
        {
            cliente.NombreCliente,
            TotalPagado = pagos.Sum(p => p.Total)
        })
        .ToListAsync<object>();

    return query2;
}

// 3. Devuelve el nombre de los clientes que hayan hecho pedidos en 2008 ordenados alfabéticamente de menor a mayor.
public async Task<List<string>> Query1Variate3()
{
    var query3 = await _context.Cliente
        .Join(_context.Pedido, c => c.CodigoCliente, p => p.CodigoCliente, (cliente, pedido) => new
        {
            cliente.NombreCliente,
            pedido.FechaPedido
        })
        .Where(p => p.FechaPedido.Year == 2008)
        .OrderBy(p => p.NombreCliente)
        .Select(p => p.NombreCliente)
        .Distinct()
        .ToListAsync();

    return query3;
}

// 4. Devuelve el nombre del cliente, el nombre y primer apellido de su representante de ventas y el número de teléfono de la oficina del representante de ventas,de aquellos clientes que no hayan realizado ningún pago.
public async Task<List<object>> Query1Variate4()
{
    var query4 = await _context.Cliente
        .GroupJoin(_context.Pago, c => c.CodigoCliente, p => p.CodigoCliente, (cliente, pagos) => new
        {
            cliente.CodigoCliente,
            cliente.NombreCliente,
            pagos.FirstOrDefault()?.Empleado.Nombre,
            pagos.FirstOrDefault()?.Empleado.Apellido1,
            pagos.FirstOrDefault()?.Empleado.Apellido2,
            pagos.FirstOrDefault()?.Empleado.Oficina.Telefono
        })
        .Where(c => c.CodigoCliente != null && c.Nombre == null)
        .Distinct()
        .ToListAsync<object>();

    return query4;
}

// 5. Devuelve el listado de clientes donde aparezca el nombre del cliente, el nombre y primer apellido de su representante de ventas y la ciudad donde está su oficina.
public async Task<List<object>> Query1Variate5()
{
    var query5 = await _context.Cliente
        .Join(_context.Empleado, c => c.CodigoEmpleadoRepVentas, e => e.CodigoEmpleado, (cliente, empleado) => new
        {
            cliente.NombreCliente,
            Empleado = $"{empleado.Nombre} {empleado.Apellido1}",
            CiudadOficina = empleado.Oficina.Ciudad
        })
        .ToListAsync<object>();

    return query5;
}

// 6. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos empleados que no sean representante de ventas de ningún cliente.
public async Task<List<object>> Query1Variate6()
{
    var query6 = await _context.Empleado
        .Join(_context.Oficina, e => e.CodigoOficina, o => o.CodigoOficina, (empleado, oficina) => new
        {
            empleado.Nombre,
            empleado.Apellido1,
            empleado.Apellido2,
            empleado.Puesto,
            oficina.Telefono
        })
        .Where(e => !_context.Cliente.Any(c => c.CodigoEmpleadoRepVentas == e.CodigoEmpleado))
        .ToListAsync<object>();

    return query6;
}

// 7. Devuelve un listado indicando todas las ciudades donde hay oficinas y el número de empleados que tiene.
public async Task<List<object>> Query1Variate7()
{
    var query7 = await _context.Oficina
        .Join(_context.Empleado, o => o.CodigoOficina, e => e.CodigoOficina, (oficina, empleado) => new
        {
            oficina.Ciudad,
            Empleado = empleado.CodigoOficina
        })
        .GroupBy(e => e.Ciudad)
        .Select(g => new
        {
            Ciudad = g.Key,
            CantidadEmpleados = g.Count()
        })
        .ToListAsync<object>();

    return query7;
}
dotnet ef dbcontext scaffold "server=localhost;user=root;password=123456;database=proyecto" Pomelo.EntityFrameworkCore.MySql -s WebApi -p Dominio --context WebApiContext --context-dir Data --output-dir Entities