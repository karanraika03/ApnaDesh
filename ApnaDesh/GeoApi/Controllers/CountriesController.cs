using AutoMapper;
using GeoApi.Data;
using GeoApi.Dtos;
using GeoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CountriesController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryReadDto>>> GetAll()
    {
        try
        {
            var items = await _db.Countries.AsNoTracking().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CountryReadDto>>(items));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CountryReadDto>> GetById(int id)
    {
        try
        {
            var entity = await _db.Countries.FindAsync(id);
            if (entity is null) return NotFound();
            return Ok(_mapper.Map<CountryReadDto>(entity));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CountryReadDto>> Create([FromBody] CountryCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = _mapper.Map<Country>(dto);
            _db.Countries.Add(entity);
            await _db.SaveChangesAsync();

            var read = _mapper.Map<CountryReadDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, read);
        }
        catch (DbUpdateException dbEx)
        {
            return Conflict(new { message = "Country with same name already exists.", error = dbEx.Message });
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CountryUpdateDto dto)
    {
        try
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = await _db.Countries.FindAsync(id);
            if (entity is null) return NotFound();

            _mapper.Map(dto, entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateException dbEx)
        {
            return Conflict(new { message = "Conflict while updating country.", error = dbEx.Message });
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
            var entity = await _db.Countries.FindAsync(id);
            if (entity is null) return NotFound();

            _db.Countries.Remove(entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }
}
