using CodeCampWebapiClass.Interfaces;
using CodeCampWebapiClass.Models;
using CodeCampWebapiClass.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampWebapiClass.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize] // jwt-todo: 5  [AllowAnonymous]
public class BetterCodeCampController : ControllerBase
{
    private readonly ILogger<BetterCodeCampController> _logger;
    //private readonly CodeCampStudentService _codeCampService;
    private readonly ICodeCampStudentService _iCodeCampStudentService;

    

    public BetterCodeCampController(ILogger<BetterCodeCampController> logger, 
     ICodeCampStudentService iCodeCampStudentService)
    {
        _logger = logger;
        //_codeCampService = codeCampService;
        _iCodeCampStudentService = iCodeCampStudentService;
    }


    [HttpGet]
    public ActionResult<IEnumerable<CodeCamperModel>> Get()
    {
        var items = _iCodeCampStudentService.GetAll();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public ActionResult<CodeCamperModel> Get(int id)
    {
        var item = _iCodeCampStudentService.GetById(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CodeCamperModel newItem)
    {
        _iCodeCampStudentService.Add(newItem);
        return CreatedAtAction(nameof(Get), new { id = newItem.CamperId }, newItem);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] CodeCamperModel updatedItem)
    {
        var success = _iCodeCampStudentService.Update(id, updatedItem);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _iCodeCampStudentService.Delete(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

}
