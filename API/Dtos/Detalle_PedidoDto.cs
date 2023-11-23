using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 

namespace API.Dtos; 
    public class Detalle_PedidoDto : BaseDto
    { 
    public int CodigoPedido { get; set; }

    public string CodigoProducto { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioUnidad { get; set; }

    public short NumeroLinea { get; set; }
    } 
