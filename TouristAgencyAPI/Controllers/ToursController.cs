using Microsoft.AspNetCore.Mvc;
using TouristAgencyAPI.Models;
using TouristAgencyAPI.Services;

namespace TouristAgencyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToursController : ControllerBase
{
    private readonly ToursService _toursService;

    public ToursController(ToursService toursService) =>
        _toursService = toursService;

    [HttpGet]
    public async Task<List<Tour>> Get() =>
        await _toursService.GetToursAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Tour>> Get(string id)
    {
        var tour = await _toursService.GetTourAsync(id);

        if (tour is null)
        {
            return NotFound();
        }

        return tour;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tour newTour)
    {
        await _toursService.CreateTourAsync(newTour);

        return CreatedAtAction(nameof(Get), new { id = newTour.Id }, newTour);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Tour updatedTour) {
        var tour = await _toursService.GetTourAsync(id);

        if (tour is null)
        {
            return NotFound();
        }

        updatedTour.Id = tour.Id;

        await _toursService.UpdateTourAsync(id, updatedTour);

        return NoContent();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveTourAsync(string id)
    {
        var tour = await _toursService.GetTourAsync(id);

        if (tour is null)
        {
            return NotFound();
        }

        await _toursService.RemoveTourAsync(id);

        return NoContent();

    }

    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<Tour>>> GetTours()
    //{
    //    var tours = await _toursCollection.Find(_ => true).ToListAsync();
    //    return Ok(tours);
    //}

    //[HttpGet("{id}")]
    //public async Task<ActionResult<Tour>> GetTour(string id)
    //{
    //    if (!ObjectId.TryParse(id, out var objectId))
    //        return BadRequest("Invalid ID format");

    //    var tour = await _toursCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
    //    if (tour == null)
    //        return NotFound();
    //    return Ok(tour);
    //}

    //[HttpPost]
    //public async Task<ActionResult<Tour>> CreateTour(TourDTO request)
    //{
    //    var tour = new Tour
    //    {
    //        Name = request.Name,
    //        Description = request.Description,
    //        Country = request.Country,
    //        Price = request.Price,
    //        DurationDays = request.DurationDays
    //    };

    //    await _toursCollection.InsertOneAsync(tour);
    //    return CreatedAtAction(nameof(GetTour), new { id = tour.Id }, tour);
    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateTour(string id, TourDTO request)
    //{
    //    if (!ObjectId.TryParse(id, out var objectId))
    //        return BadRequest("Invalid ID format");

    //    var tour = await _toursCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
    //    if (tour == null)
    //        return NotFound();

    //    tour.Name = request.Name;
    //    tour.Description = request.Description;
    //    tour.Country = request.Country;
    //    tour.Price = request.Price;
    //    tour.DurationDays = request.DurationDays;

    //    await _toursCollection.ReplaceOneAsync(t => t.Id == id, tour);
    //    return NoContent();
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteTour(string id)
    //{
    //    if (!ObjectId.TryParse(id, out var objectId))
    //        return BadRequest("Invalid ID format");

    //    var result = await _toursCollection.DeleteOneAsync(t => t.Id == id);
    //    if (result.DeletedCount == 0)
    //        return NotFound();
    //    return NoContent();
    //}
}