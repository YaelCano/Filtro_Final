using Application.Repository;
using Domain.Interfaces;
using Persistence.Data;
namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly FinalContext _context;
    public UnitOfWork(FinalContext context)
    {
        _context = context;
    }
    public void Dispose()
    {
        _context.Dispose();
    }
    private ICliente _clientes;
    private IDetallePedido _detallepedidos;
    private IEmpleado _empleados;
    private IGamaProducto _gamaproductos;
    private IOficina _oficinas;
    private IPago _pagos;
    private IPedido _pedidos;
    private IProducto _productos;
    public ICliente Clientes {
        get
        {
            if(_clientes == null) 
            {
                _clientes = new ClienteRepository(_context);
            }
            return _clientes;
        }
    }
    public IDetallePedido DetallePedidos {
        get
        {
            if(_detallepedidos == null) 
            {
                _detallepedidos = new Detalle_PedidoRepository(_context);
            }
            return _detallepedidos;
        }
    }
    public IEmpleado Empleados {
        get
        {
            if(_empleados == null) 
            {
                _empleados = new EmpleadoRepository(_context);
            }
            return _empleados;
        }
    }
    public IGamaProducto GamaProductos {
        get
        {
            if(_gamaproductos == null) 
            {
                _gamaproductos = new GamaProductoRepository(_context);
            }
            return _gamaproductos;
        }
    }
    public IOficina Oficinas {
        get
        {
            if(_oficinas == null) 
            {
                _oficinas = new OficinaRepository(_context);
            }
            return _oficinas;
        }
    }
    public IPago Pagos {
        get
        {
            if(_pagos == null) 
            {
                _pagos = new PagoRepository(_context);
            }
            return _pagos;
        }
    }
    public IPedido Pedidos {
        get
        {
            if(_pedidos == null) 
            {
                _pedidos = new PedidoRepository(_context);
            }
            return _pedidos;
        }
    }
    public IProducto Productos {
        get
        {
            if(_productos == null) 
            {
                _productos = new ProductoRepository(_context);
            }
            return _productos;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
