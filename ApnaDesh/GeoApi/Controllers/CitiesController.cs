using AutoMapper;
using GeoApi.Data;
using GeoApi.Dtos;
using GeoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CitiesController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityReadDto>>> GetAll()
    {
        try
        {
            var items = await _db.Cities.AsNoTracking().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CityReadDto>>(items));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpGet("by-state/{stateId:int}")]
    public async Task<ActionResult<IEnumerable<CityReadDto>>> GetByState(int stateId)
    {
        try
        {
            var items = await _db.Cities.AsNoTracking()
                .Where(c => c.StateId == stateId)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CityReadDto>>(items));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CityReadDto>> GetById(int id)
    {
        try
        {
            var entity = await _db.Cities.FindAsync(id);
            if (entity is null) return NotFound();
            return Ok(_mapper.Map<CityReadDto>(entity));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CityReadDto>> Create([FromBody] CityCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            // Ensure State exists
            var stateExists = await _db.States.AnyAsync(s => s.Id == dto.StateId);
            if (!stateExists) return BadRequest(new { message = "Invalid StateId" });

            var entity = _mapper.Map<City>(dto);
            _db.Cities.Add(entity);
            await _db.SaveChangesAsync();

            var read = _mapper.Map<CityReadDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, read);
        }
        catch (DbUpdateException dbEx)
        {
            return Conflict(new { message = "City with same name already exists in this state.", error = dbEx.Message });
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CityUpdateDto dto)
    {
        try
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = await _db.Cities.FindAsync(id);
            if (entity is null) return NotFound();

            // Ensure State exists
            var stateExists = await _db.States.AnyAsync(s => s.Id == dto.StateId);
            if (!stateExists) return BadRequest(new { message = "Invalid StateId" });

            _mapper.Map(dto, entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateException dbEx)
        {
            return Conflict(new { message = "Conflict while updating city.", error = dbEx.Message });
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var entity = await _db.Cities.FindAsync(id);
            if (entity is null) return NotFound();

            _db.Cities.Remove(entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }
}
