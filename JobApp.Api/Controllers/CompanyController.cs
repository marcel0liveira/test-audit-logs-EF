using JobApp.Application.DTOs.Requests;
using JobApp.Domain.Models;
using JobApp.Infrastructure.Persistences;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobAppApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly JobAppDbContext _context;

    public CompanyController(JobAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<Company>>> GetAll() => await _context.Companies.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Company>> Create(CompanyDtoInp input)
    {
        var company = new Company()
        {
            Industry = input.Industry,
            User = new User()
            {
                Email = input.Email,
                Name = input.Name,
                Password = input.Password,
            }
        };
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created);
    }
}