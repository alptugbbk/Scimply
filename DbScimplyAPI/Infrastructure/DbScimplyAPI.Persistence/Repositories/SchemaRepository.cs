using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Domain.Entities.User;
using DbScimplyAPI.Persistence.Context;


namespace DbScimplyAPI.Persistence.Repositories
{
    public class SchemaRepository : GenericRepository<Schema>, ISchemaRepository
    {
        public SchemaRepository(MyContext context) : base(context)
        {
        }
    }
}
