using Autofac;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Common;
using SmartStore.Core.Events;
using SmartStore.Plugin1.Domain;
using SmartStore.Services.Catalog;
using System;
using System.Linq;

namespace SmartStore.Plugin1.Services
{
    public partial class Plugin1Service : IPlugin1Service
    {
        private readonly IRepository<Plugin1Record> _repository;
        private readonly IDbContext _dbContext;
        private readonly AdminAreaSettings _adminAreaSettings;
        private readonly IEventPublisher _eventPublisher;

        public Plugin1Service(
            IRepository<Plugin1Record> repository,
            IDbContext dbContext,
            AdminAreaSettings adminAreaSettings,
            IEventPublisher eventPublisher,
            IComponentContext ctx)
        {
            _repository = repository;
            _dbContext = dbContext;
            _adminAreaSettings = adminAreaSettings;
            _eventPublisher = eventPublisher;
        }

        public Plugin1Record GetPlugin1Record(int entityId, string entityName)
        {
            if (entityId == 0)
                return null;

            var record = new Plugin1Record();

            var query =
                from x in _repository.Table
                    //where x.EntityId == entityId && x.EntityName == entityName
                select x;

            record = query.FirstOrDefault();

            return record;
        }

        public Plugin1Record GetPlugin1RecordById(int id)
        {
            if (id == 0)
                return null;

            var record = new Plugin1Record();

            var query =
                from x in _repository.Table
                where x.Id == id
                select x;

            record = query.FirstOrDefault();

            return record;
        }

        public void InsertPlugin1Record(Plugin1Record record)
        {
            Guard.NotNull(record, nameof(record));

            var utcNow = DateTime.UtcNow;
            record.CreatedOnUtc = utcNow;

            _repository.Insert(record);
        }

        public void UpdatePlugin1Record(Plugin1Record record)
        {
            Guard.NotNull(record, nameof(record));

            var utcNow = DateTime.UtcNow;
            record.UpdatedOnUtc = utcNow;

            _repository.Update(record);
        }

        public void DeletePlugin1Record(Plugin1Record record)
        {
            Guard.NotNull(record, nameof(record));

            _repository.Delete(record);
        }
    }
}
