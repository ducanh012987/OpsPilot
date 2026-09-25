using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsPilot.OpsPilot.Domain.Entities.Projects;
using OpsPilot.OpsPilot.Infrastructure.Persistence;

namespace OpsPilot.Api.Controllers.Projects
{
    [ApiController]
    [Route("api/v1/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly OpsPilotDbContext _dbContext;

        public ProjectsController(OpsPilotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
        {
            var projects = await _dbContext.Projects
                .AsNoTracking()
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync(cancellationToken);

            return Ok(projects);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);

            if (project is null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        public record CreateProjectRequest(
            string Name,
            string Code,
            string? Description);

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProjectRequest request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.Code))
            {
                return BadRequest(new
                {
                    message = "Name và Code là bắt buộc."
                });
            }

            var code = request.Code.Trim().ToUpperInvariant();

            var exists = await _dbContext.Projects
                .AnyAsync(x => x.Code == code, cancellationToken);

            if (exists)
            {
                return Conflict(new
                {
                    message = "Mã project đã tồn tại."
                });
            }

            var project = new Project
            {
                Name = request.Name.Trim(),
                Code = code,
                Description = request.Description?.Trim(),
                CreateDate = DateTime.UtcNow
            };

            _dbContext.Projects.Add(project);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = project.Id },
                project);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);

            if (project is null)
            {
                return NotFound();
            }

            _dbContext.Projects.Remove(project);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
