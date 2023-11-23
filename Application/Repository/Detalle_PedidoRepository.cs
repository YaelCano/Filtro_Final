using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository
{
    public class Detalle_PedidoRepository: GenericRepository<DetallePedido> , IDetallePedido 
    { 
        public FinalContext _context { get; set; } 
        public Detalle_PedidoRepository(FinalContext context) : base(context) 
        { 
            _context = context; 
        } 
    } 
} 