using SmartStore.DellyManLogistics.Domain;

namespace SmartStore.DellyManLogistics.Services
{
    public partial interface IDellyManLogisticsService
    {
        DellyManLogisticsRecord GetDellyManLogisticsRecord(int entityId, string entityName);
        DellyManLogisticsRecord GetDellyManLogisticsRecordById(int id);
        void InsertDellyManLogisticsRecord(DellyManLogisticsRecord record);
        void UpdateDellyManLogisticsRecord(DellyManLogisticsRecord record);
        void DeleteDellyManLogisticsRecord(DellyManLogisticsRecord record);
    }
}
