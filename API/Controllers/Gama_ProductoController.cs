using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class Gama_ProductoController: BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Gama_ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GamaProducto>>> Get()
        {
            var entidades = await _unitOfWork.GamaProductos.GetAllAsync();
            return _mapper.Map<List<GamaProducto>>(entidades);
        }

   

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GamaProducto>> Post(Gama_ProductoDto Gama_ProductoDto)
        {
            var entidad = _mapper.Map<GamaProducto>(Gama_ProductoDto);
            this._unitOfWork.GamaProductos.Add(entidad);
            await _unitOfWork.SaveAsync();
            if(entidad == null)
            {
                return BadRequest();
            }
            Gama_ProductoDto.Id = entidad.Id;
            return CreatedAtAction(nameof(Post), new {id = Gama_ProductoDto.Id}, Gama_ProductoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Gama_ProductoDto>> Put(int id, [FromBody] Gama_ProductoDto Gama_ProductoDto)
        {
            if(Gama_ProductoDto == null)
            {
                return NotFound();
            }
            var entidades = _mapper.Map<GamaProducto>(Gama_ProductoDto);
            _unitOfWork.GamaProductos.Update(entidades);
            await _unitOfWork.SaveAsync();
            return Gama_ProductoDto;
        }

        // [HttpDelete("{id}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var entidad = await _unitOfWork.GamaProductos.GetByIdAsync(id);
        //     if(entidad == null)
        //     {
        //         return NotFound();
        //     }
        //     _unitOfWork.GamaProductos.Delete(entidad);
        //     await _unitOfWork.SaveAsync();
        //     return NoContent();
        // }
    }
