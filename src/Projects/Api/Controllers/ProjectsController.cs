using KanbanFlow.Projects.Application.Commands.CreateProject;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KanbanFlow.Projects.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : Controller
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        var projectId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProjectById), new { id = projectId }, null);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetProjectById(Guid id)
    {
        return Ok();
    }
}