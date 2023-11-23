using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 

namespace API.Dtos; 
    public class Gama_ProductoDto : BaseStringDto
    { 
    public string Gama { get; set; } = null!;

    public string DescripcionTexto { get; set; }

    public string DescripcionHtml { get; set; }

    public string Imagen { get; set; }
    } 
