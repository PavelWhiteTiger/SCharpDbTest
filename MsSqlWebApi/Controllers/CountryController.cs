using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Core.DbModels;

namespace MsSqlWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CountryController : Controller
{
    private readonly ApplicationContext _db;

    public CountryController(ApplicationContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = _db.Countries.ToList();
        return Ok(countries);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int countryId)
    {
        var country = _db.Countries.FirstOrDefault(x => x.Id == countryId);
        if (country is null)
        {
            return BadRequest("No BRO");
        }

        return Ok(country);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] Country country)
    {
        _db.Countries.Add(country);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] Country country)
    {
        _db.Countries.Update(country);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromForm] int countryId)
    {
        var country = _db.Countries.FirstOrDefault(x => x.Id == countryId);
        if (country is null)
        {
            return BadRequest("No BRO");
        }

        _db.Countries.Remove(country);
        await _db.SaveChangesAsync();
        return Ok();
    }
}