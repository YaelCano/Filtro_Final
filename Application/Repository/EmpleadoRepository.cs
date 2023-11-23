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
    public class EmpleadoRepository : GenericRepository<Empleado> , IEmpleado 
    { 
        public FinalContext _context { get; set; } 
        public EmpleadoRepository(FinalContext context) : base(context) 
        { 
            _context = context; 
        } 
    } 
} 
