using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Common.Settings;

namespace Play.Common.MongoDB;

public static class Extensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        // Serialize UUID and Datetime when save on MongoDB
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        services.AddSingleton(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();

            var serviceSettings = configuration!.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            var mongoClient = new MongoClient(mongoDbSettings!.ConnectionString);
            return mongoClient.GetDatabase(serviceSettings!.ServiceName);
        });

        return services;
    }

    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(serviceProvider =>
        {
            var database = serviceProvider.GetService<IMongoDatabase>();
            return new MongoRepository<T>(database!, collectionName);
        });

        return services;
    }

    public static IServiceCollection AddIndex<T>(this IServiceCollection services, string collectionName, string index) where T : IEntity
    {
        services.AddSingleton(serviceProvider =>
        {
            var database = serviceProvider.GetService<IMongoDatabase>();
            var dbCollection = database!.GetCollection<T>(collectionName);

            var key = Builders<T>.IndexKeys.Ascending(index);

            var options = new CreateIndexOptions()
            {
                Unique = true,
                Background = false
            };

            var indexDefinition = new CreateIndexModel<T>(key, options);
            dbCollection.Indexes.CreateOne(indexDefinition);

            return dbCollection;
        });

        return services;
    }
}