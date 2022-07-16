using SmartStore.WhatsApp.Domain;

namespace SmartStore.WhatsApp.Services
{
    public partial interface IWhatsAppService
    {
        WhatsAppRecord GetWhatsAppRecord(int entityId, string entityName);
        WhatsAppRecord GetWhatsAppRecordById(int id);
        void InsertWhatsAppRecord(WhatsAppRecord record);
        void UpdateWhatsAppRecord(WhatsAppRecord record);
        void DeleteWhatsAppRecord(WhatsAppRecord record);
    }
}
