using CodeCampWebapiClass.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampWebapiClass.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CodeCampController : ControllerBase
{
    private readonly ILogger<CodeCampController> _logger;

    public CodeCampController(ILogger<CodeCampController> logger)
    {
        _logger = logger;
    }

    //Run-time
    private static List<CodeCamperModel> items = new List<CodeCamperModel>
    {
        new CodeCamperModel {  CamperId = 1, CamperName = "Name 1", Track = CodeCampTrack.FrontEnd },
        new CodeCamperModel {  CamperId = 2, CamperName = "Name 2", Track = CodeCampTrack.Backend },
        new CodeCamperModel {  CamperId = 3, CamperName = "Name 3", Track = CodeCampTrack.Mobile },
    };

    [HttpGet]
    public ActionResult<IEnumerable<CodeCamperModel>> Get()
    {
        return Ok(items);
    }

    [HttpGet("{id}")]
    public ActionResult<CodeCamperModel> Get(int id)
    {
        var item = items.Find(i => i.CamperId == id);

        _logger.LogInformation("We called GET Endpoint");

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CodeCamperModel newItem)
    {
        items.Add(newItem);
        return CreatedAtAction(nameof(Get), new { id = newItem.CamperId }, newItem);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] CodeCamperModel updatedItem)
    {
        var existingItem = items.Find(i => i.CamperId == id);

        if (existingItem == null)
        {
            return NotFound();
        }

        existingItem.CamperName = updatedItem.CamperName;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var itemToRemove = items.Find(i => i.CamperId == id);

        if (itemToRemove == null)
        {
            return NotFound();
        }

        items.Remove(itemToRemove);
        return NoContent();
    }

}
