namespace TouristAgencyAPI.Models;

public class TourDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationDays { get; set; }
}