using Autofac;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Common;
using SmartStore.Core.Events;
using SmartStore.DellyManLogistics.Domain;
using SmartStore.Services.Catalog;
using System;
using System.Linq;

namespace SmartStore.DellyManLogistics.Services
{
    public partial class DellyManLogisticsService : IDellyManLogisticsService
    {
        private readonly IRepository<DellyManLogisticsRecord> _repository;
        private readonly IDbContext _dbContext;
        private readonly AdminAreaSettings _adminAreaSettings;
        private readonly IEventPublisher _eventPublisher;

        public DellyManLogisticsService(
            IRepository<DellyManLogisticsRecord> repository,
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

        public DellyManLogisticsRecord GetDellyManLogisticsRecord(int entityId, string entityName)
        {
            if (entityId == 0)
                return null;

            var record = new DellyManLogisticsRecord();

            var query =
                from x in _repository.Table
                    //where x.EntityId == entityId && x.EntityName == entityName
                select x;

            record = query.FirstOrDefault();

            return record;
        }

        public DellyManLogisticsRecord GetDellyManLogisticsRecordById(int id)
        {
            if (id == 0)
                return null;

            var record = new DellyManLogisticsRecord();

            var query =
                from x in _repository.Table
                where x.Id == id
                select x;

            record = query.FirstOrDefault();

            return record;
        }

        public void InsertDellyManLogisticsRecord(DellyManLogisticsRecord record)
        {
            Guard.NotNull(record, nameof(record));

            var utcNow = DateTime.UtcNow;
            record.CreatedOnUtc = utcNow;

            _repository.Insert(record);
        }

        public void UpdateDellyManLogisticsRecord(DellyManLogisticsRecord record)
        {
            Guard.NotNull(record, nameof(record));

            var utcNow = DateTime.UtcNow;
            record.UpdatedOnUtc = utcNow;

            _repository.Update(record);
        }

        public void DeleteDellyManLogisticsRecord(DellyManLogisticsRecord record)
        {
            Guard.NotNull(record, nameof(record));

            _repository.Delete(record);
        }
    }
}
