using codecampapi.Models;
using codecampapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace codecampapi.Controllers;

//[EnableCors("AllowSpecificOrigin")]
[ApiController]
[Route("[controller]")]
//[Authorize] // Add this attribute to secure the entire controller, // jwt-todo: 6
public class BetterCodeCampController : ControllerBase
{
    private readonly ILogger<BetterCodeCampController> _logger;
    private readonly CodeCampService _codeCampService;

    public BetterCodeCampController(ILogger<BetterCodeCampController> logger, CodeCampService codeCampService)
    {
        _logger = logger;
        _codeCampService = codeCampService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CodeCamperModel>> Get()
    {
        var items = _codeCampService.GetAll();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public ActionResult<CodeCamperModel> Get(int id)
    {
        var item = _codeCampService.GetById(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CodeCamperModel newItem)
    {
        _codeCampService.Add(newItem);
        return CreatedAtAction(nameof(Get), new { id = newItem.CamperId }, newItem);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] CodeCamperModel updatedItem)
    {
        var success = _codeCampService.Update(id, updatedItem);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _codeCampService.Delete(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

}
