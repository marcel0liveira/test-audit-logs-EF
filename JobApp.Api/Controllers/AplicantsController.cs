using JobApp.Application.DTOs.Requests;
using JobApp.Domain.Models;
using JobApp.Infrastructure.Persistences;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobAppApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicantsController : ControllerBase
{
    private readonly JobAppDbContext _context;

    public ApplicantsController(JobAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<Applicant>>> GetAll() => await _context.Applicants.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Applicant>> Create(ApplicantDtoInp input)
    {
        var applicant = new Applicant()
        {
            Skill = input.Skill,
            User = new User()
            {
                Email = input.Email,
                Name = input.Name,
                Password = input.Password,
            }
        };
        _context.Applicants.Add(applicant);
        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status201Created);
    }
}
