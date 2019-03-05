using MongoDB.Driver;

namespace CommonAdapters.MongoDbEventStore
{
    public sealed class DatabaseContext
    {
        private readonly IMongoDatabase _database;

        public DatabaseContext(string connectionString, string databaseName)
        {
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollectionFor<T>()
            => _database.GetCollection<T>(typeof(T).Name);
    }
}