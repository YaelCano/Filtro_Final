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
    public class PagoRepository : GenericRepository<Pago> , IPago 
    { 
        public FinalContext _context { get; set; } 
        public PagoRepository(FinalContext context) : base(context) 
        { 
            _context = context; 
        }

        public Task<Pago> GetByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }
    } 
} 
