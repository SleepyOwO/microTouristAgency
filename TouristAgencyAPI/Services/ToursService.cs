using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TouristAgencyAPI.Models;

namespace TouristAgencyAPI.Services
{
    public class ToursService
    {

        private readonly IMongoCollection<Tour> _toursCollection;

        public ToursService(IOptions<TourAgencyDatabaseSettings> tourDbSettings)
        {
            var mongoClient = new MongoClient(
                tourDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                tourDbSettings.Value.DatabaseName);

            _toursCollection = mongoDatabase.GetCollection<Tour>(
                tourDbSettings.Value.ToursCollectionName);
        }

        public async Task<List<Tour>> GetToursAsync() =>
            await _toursCollection.Find(_ => true).ToListAsync();

        public async Task<Tour?> GetTourAsync(string id) =>
            await _toursCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateTourAsync(Tour newTour) =>
            await _toursCollection.InsertOneAsync(newTour);

        public async Task UpdateTourAsync(string id, Tour updatedTour) =>
            await _toursCollection.ReplaceOneAsync(x => x.Id == id, updatedTour);

        public async Task RemoveTourAsync(string id) =>
            await _toursCollection.DeleteOneAsync(x => x.Id == id);

    }
}
