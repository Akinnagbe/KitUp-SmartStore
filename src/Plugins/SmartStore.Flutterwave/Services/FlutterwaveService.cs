using Autofac;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Common;
using SmartStore.Core.Events;
using SmartStore.Flutterwave.Domain;
using SmartStore.Services.Catalog;
using System;
using System.Linq;

namespace SmartStore.Flutterwave.Services
{
    public partial class FlutterwaveService : IFlutterwaveService
    {
        private readonly IRepository<FlutterwaveRecord> _repository;
        private readonly IDbContext _dbContext;
        private readonly AdminAreaSettings _adminAreaSettings;
        private readonly IEventPublisher _eventPublisher;

        public FlutterwaveService(
            IRepository<FlutterwaveRecord> repository,
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

        public FlutterwaveRecord GetFlutterwaveRecord(int entityId, string entityName)
        {
            if (entityId == 0)
                return null;

            var record = new FlutterwaveRecord();

            var query =
                from x in _repository.Table
                    //where x.EntityId == entityId && x.EntityName == entityName
                select x;

            record = query.FirstOrDefault();

            return record;
        }

        public FlutterwaveRecord GetFlutterwaveRecordById(int id)
        {
            if (id == 0)
                return null;

            var record = new FlutterwaveRecord();

            var query =
                from x in _repository.Table
                where x.Id == id
                select x;

            record = query.FirstOrDefault();

            return record;
        }

        public void InsertFlutterwaveRecord(FlutterwaveRecord record)
        {
            Guard.NotNull(record, nameof(record));

            var utcNow = DateTime.UtcNow;
            record.CreatedOnUtc = utcNow;

            _repository.Insert(record);
        }

        public void UpdateFlutterwaveRecord(FlutterwaveRecord record)
        {
            Guard.NotNull(record, nameof(record));

            var utcNow = DateTime.UtcNow;
            record.UpdatedOnUtc = utcNow;

            _repository.Update(record);
        }

        public void DeleteFlutterwaveRecord(FlutterwaveRecord record)
        {
            Guard.NotNull(record, nameof(record));

            _repository.Delete(record);
        }
    }
}
