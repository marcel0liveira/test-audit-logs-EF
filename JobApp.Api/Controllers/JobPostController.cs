using JobApp.Application.DTOs.Requests;
using JobApp.Domain.Models;
using JobApp.Infrastructure.Persistences;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobAppApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobPostController : ControllerBase
{
    private readonly JobAppDbContext _context;

    public JobPostController(JobAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<JobPost>>> GetAll() => await _context.JobPosts.Include(j => j.Company).ToListAsync();

    [HttpPost]
    public async Task<ActionResult<JobPost>> Create(JobPostDtoInp jobPost)
    {
        var company = await _context.Companies.FindAsync(jobPost.CompanyId);
        if (company == null)
        {
            return BadRequest("Invalid CompanyId");
        }

        _context.JobPosts.Add(
                new JobPost()
                {
                    Title = jobPost.Title,
                    Description = jobPost.Description,
                    CompanyId = jobPost.CompanyId,
                }
            );

        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status201Created);
    }
}
