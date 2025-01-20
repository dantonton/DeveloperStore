using DeveloperStore.Domain.Infra;
using DeveloperStore.Domain.Models;
using MongoDB.Driver;

namespace DeveloperStore.Infra.Repositories
{
    public class BaseRepository<TEntity>  : IBaseRepository<TEntity> where TEntity : BaseModel
    {

        private readonly IMongoCollection<TEntity> collection;
        private readonly MongoDbContext mongoDbContext;

        public BaseRepository(MongoDbContext mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
            this.collection = mongoDbContext.Database.GetCollection<TEntity>(typeof(TEntity).FullName);
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id.ToString()), cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetByAllAsync(CancellationToken cancellationToken)
        {
            return await collection.Find(Builders<TEntity>.Filter.Empty).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {

            return await collection.Find(Builders<TEntity>.Filter.Eq("_id", id.ToString())).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id), entity, cancellationToken: cancellationToken);
        }
    }
}
