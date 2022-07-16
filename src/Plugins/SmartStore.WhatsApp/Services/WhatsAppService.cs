using Autofac;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Common;
using SmartStore.Core.Events;
using SmartStore.Services.Catalog;
using SmartStore.WhatsApp.Domain;
using System;
using System.Linq;

namespace SmartStore.WhatsApp.Services
{
    public partial class WhatsAppService : IWhatsAppService
    {
        private readonly IRepository<WhatsAppRecord> _repository;
        private readonly IDbContext _dbContext;
        private readonly AdminAreaSettings _adminAreaSettings;
        private readonly IEventPublisher _eventPublisher;

        public WhatsAppService(
            IRepository<WhatsAppRecord> repository,
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

        public WhatsAppRecord GetWhatsAppRecord(int entityId, string entityName)
        {
            if (entityId == 0)
                return null;

            var record = new WhatsAppRecord();

            var query =
                from x in _repository.Table
                    //where x.EntityId == entityId && x.EntityName == entityName
                select x;

            record = query.FirstOrDefault();

            return record;
        }

        public WhatsAppRecord GetWhatsAppRecordById(int id)
        {
            if (id == 0)
                return null;

            var record = new WhatsAppRecord();

            var query =
                from x in _repository.Table
                where x.Id == id
                select x;

            record = query.FirstOrDefault();

            return record;
        }

        public void InsertWhatsAppRecord(WhatsAppRecord record)
        {
            Guard.NotNull(record, nameof(record));

            var utcNow = DateTime.UtcNow;
            record.CreatedOnUtc = utcNow;

            _repository.Insert(record);
        }

        public void UpdateWhatsAppRecord(WhatsAppRecord record)
        {
            Guard.NotNull(record, nameof(record));

            var utcNow = DateTime.UtcNow;
            record.UpdatedOnUtc = utcNow;

            _repository.Update(record);
        }

        public void DeleteWhatsAppRecord(WhatsAppRecord record)
        {
            Guard.NotNull(record, nameof(record));

            _repository.Delete(record);
        }
    }
}
