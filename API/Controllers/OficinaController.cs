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
public class OficinaController: BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OficinaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Oficina>>> Get()
        {
            var entidades = await _unitOfWork.Oficinas.GetAllAsync();
            return _mapper.Map<List<Oficina>>(entidades);
        }

        // [HttpGet("{id}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public async Task<ActionResult<OficinaDto>> Get(int id)
        // {
        //     var entidad = await _unitOfWork.Oficinas.GetByIdAsync(id);
        //     if(entidad == null)
        //     {
        //         return NotFound();
        //     }
        //     return _mapper.Map<OficinaDto>(entidad);
        // }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Oficina>> Post(OficinaDto OficinaDto)
        {
            var entidad = _mapper.Map<Oficina>(OficinaDto);
            this._unitOfWork.Oficinas.Add(entidad);
            await _unitOfWork.SaveAsync();
            if(entidad == null)
            {
                return BadRequest();
            }
            OficinaDto.Id = entidad.Id;
            return CreatedAtAction(nameof(Post), new {id = OficinaDto.Id}, OficinaDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OficinaDto>> Put(int id, [FromBody] OficinaDto OficinaDto)
        {
            if(OficinaDto == null)
            {
                return NotFound();
            }
            var entidades = _mapper.Map<Oficina>(OficinaDto);
            _unitOfWork.Oficinas.Update(entidades);
            await _unitOfWork.SaveAsync();
            return OficinaDto;
        }



    }
