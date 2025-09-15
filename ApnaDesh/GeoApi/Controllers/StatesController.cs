using AutoMapper;
using GeoApi.Data;
using GeoApi.Dtos;
using GeoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatesController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public StatesController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StateReadDto>>> GetAll()
    {
        try
        {
            var items = await _db.States.AsNoTracking().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<StateReadDto>>(items));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpGet("by-country/{countryId:int}")]
    public async Task<ActionResult<IEnumerable<StateReadDto>>> GetByCountry(int countryId)
    {
        try
        {
            var items = await _db.States.AsNoTracking()
                .Where(s => s.CountryId == countryId)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<StateReadDto>>(items));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StateReadDto>> GetById(int id)
    {
        try
        {
            var entity = await _db.States.FindAsync(id);
            if (entity is null) return NotFound();
            return Ok(_mapper.Map<StateReadDto>(entity));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpPost]
    public async Task<ActionResult<StateReadDto>> Create([FromBody] StateCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            // Ensure Country exists
            var countryExists = await _db.Countries.AnyAsync(c => c.Id == dto.CountryId);
            if (!countryExists) return BadRequest(new { message = "Invalid CountryId" });

            var entity = _mapper.Map<State>(dto);
            _db.States.Add(entity);
            await _db.SaveChangesAsync();

            var read = _mapper.Map<StateReadDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, read);
        }
        catch (DbUpdateException dbEx)
        {
            return Conflict(new { message = "State with same name already exists in this country.", error = dbEx.Message });
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] StateUpdateDto dto)
    {
        try
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = await _db.States.FindAsync(id);
            if (entity is null) return NotFound();

            // Ensure Country exists
            var countryExists = await _db.Countries.AnyAsync(c => c.Id == dto.CountryId);
            if (!countryExists) return BadRequest(new { message = "Invalid CountryId" });

            _mapper.Map(dto, entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateException dbEx)
        {
            return Conflict(new { message = "Conflict while updating state.", error = dbEx.Message });
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
            var entity = await _db.States.FindAsync(id);
            if (entity is null) return NotFound();

            _db.States.Remove(entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }
}
