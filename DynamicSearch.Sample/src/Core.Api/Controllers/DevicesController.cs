namespace Core.Api.Controller;

[Route("dev/[controller]")]
[ApiController]
public class DevicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DevicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchDevicesAsync([FromBody] SearchDevicesCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}