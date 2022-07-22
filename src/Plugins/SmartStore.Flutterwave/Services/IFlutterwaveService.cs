using SmartStore.Flutterwave.Domain;

namespace SmartStore.Flutterwave.Services
{
    public partial interface IFlutterwaveService
    {
        FlutterwaveRecord GetFlutterwaveRecord(int entityId, string entityName);
        FlutterwaveRecord GetFlutterwaveRecordById(int id);
        void InsertFlutterwaveRecord(FlutterwaveRecord record);
        void UpdateFlutterwaveRecord(FlutterwaveRecord record);
        void DeleteFlutterwaveRecord(FlutterwaveRecord record);
    }
}
