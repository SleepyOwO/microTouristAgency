namespace TouristAgencyAPI.Models
{
    public class TourAgencyDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ToursCollectionName { get; set; } = null!;

    }
}
